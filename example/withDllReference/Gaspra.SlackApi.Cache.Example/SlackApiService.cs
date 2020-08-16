using Gaspra.SlackApi.Extensions;
using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Cache.Example
{
    public class SlackApiService : IHostedService
    {
        private readonly string token = "**INSERT APP TOKEN HERE**";
        private readonly string channelName = "**INSERT CHANNEL NAME HERE**";

        private readonly ISlackApi slackApi;
        private readonly IMemoryCache memoryCache;
        private readonly Random random;

        private SlackChannel slackChannel;

        public SlackApiService(
            ISlackApiFactory slackApiFactory,
            IMemoryCache memoryCache)
        {
            this.slackApi = slackApiFactory.CreateSlackApi();
            this.memoryCache = memoryCache;
            this.random = new Random();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            slackChannel = await slackApi.GetSlackChannelWithName(token, channelName);

            if (slackChannel == null) throw new Exception($"Unable to find channel with name: {channelName}");

            var iteration = 1;

            while (iteration < 31)
            {
                var randomValue = random.Next(1, 4);

                var slackMessage = randomValue switch
                {
                    1 => await GetOrCreateMessageCacheEntry(":bug:"),
                    2 => await GetOrCreateMessageCacheEntry(":poop:"),
                    3 => await GetOrCreateMessageCacheEntry(":tada:")
                };

                var postTask = randomValue switch
                {
                    1 => slackApi.PostInThread(token, slackChannel.Id, slackMessage.ThreadId, $"`{iteration}`"),
                    2 => slackApi.PostInThread(token, slackChannel.Id, slackMessage.ThreadId, $"`{iteration}`"),
                    3 => slackApi.PostInThread(token, slackChannel.Id, slackMessage.ThreadId, $"`{iteration}`")
                };

                await postTask;

                iteration++;

                await Task.Delay(500);
            }
        }

        private async Task<SlackMessage> GetOrCreateMessageCacheEntry(string message)
        {
            var cacheEntry = await memoryCache.GetOrCreateAsync(message, entry =>
            {
                var message = slackApi.PostMessage(token, slackChannel.Id, entry.Key.ToString());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(3))
                    .RegisterPostEvictionCallback(async (key, value, reason, substate) =>
                    {
                        var slackMessage = value as SlackMessage;

                        await slackApi.PostInThread(token, slackChannel.Id, slackMessage.ThreadId, $"Ending thread {key}, removed from cache");
                    });


                entry.SetOptions(cacheEntryOptions);

                return message;
            });

            return cacheEntry;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}