using Gaspra.SlackApi.Extensions;
using Gaspra.SlackApi.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Example
{
    public class SlackApiService : IHostedService
    {
        private readonly string token = "**INSERT APP TOKEN HERE**";
        private readonly string channelName = "**INSERT CHANNEL NAME HERE**";

        private readonly ISlackApi slackApi;

        public SlackApiService(
            ISlackApiFactory slackApiFactory)
        {
            this.slackApi = slackApiFactory.CreateSlackApi();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var channel = await slackApi.GetSlackChannelWithName(token, channelName);

            if (channel == null) throw new Exception($"Unable to find channel with name: {channelName}");

            var message = await slackApi.PostMessage(token, channel.Id, $":smiley: I'm posting to the `{channelName}` channel");

            for (var i = 0; i < 5; i++)
            {
                await slackApi.PostInThread(
                    token,
                    channel.Id,
                    message.ThreadId,
                    $"Posting in thread: `{i + 1} / 5`");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}