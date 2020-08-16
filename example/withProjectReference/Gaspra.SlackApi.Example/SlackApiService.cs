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

        private readonly ISlackApi _slackApi;

        public SlackApiService(
            ISlackApi slackApi)
        {
            _slackApi = slackApi;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var postMessageResponse = await _slackApi.PostMessage(token, channelName, $":smiley: I'm posting to the `{channelName}` channel");

            if (postMessageResponse.Ok)
            {
                for (var i = 0; i < 3; i++)
                {
                    await _slackApi.PostInThread(
                        token,
                        channelName,
                        postMessageResponse.Message.ThreadId,
                        $"Posting in thread: `{i + 1} / 3`");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}