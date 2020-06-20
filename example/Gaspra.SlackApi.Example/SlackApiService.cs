using Gaspra.SlackApi.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
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
            var channelResponse = await slackApi.GetChannels(token);

            var channel = channelResponse
                .Channels
                .Where(c => c.Name.Equals(channelName))
                .FirstOrDefault();

            if (channel == null) throw new Exception($"Unable to find channel with name: {channelName}");

            var guidToPost = Guid.NewGuid();

            await slackApi.PostMessage(token, channel.Id, $":smiley: I'm posting to {channelName} [{guidToPost}]");

            var conversationHistory = await slackApi.GetConversationHistory(token, channel.Id);

            var message = conversationHistory
                .Messages
                .Where(m => m.Text.EndsWith($"[{guidToPost}]"))
                .FirstOrDefault();

            if (message == null) throw new Exception($"Unable to find message ending with: [{guidToPost}]");

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
