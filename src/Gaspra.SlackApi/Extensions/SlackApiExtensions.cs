using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using Gaspra.SlackApi.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Extensions
{
    public static class SlackApiExtensions
    {
        public static async Task<SlackChannel> GetSlackChannelWithName(
            this ISlackApi slackApi,
            string token,
            string channelName,
            int searchLimit = 100)
        {
            var channelResponse = await slackApi.GetChannels(token, searchLimit);

            var channels = await RecurseChannelPagesUntilChannelExists(slackApi, token, channelResponse, channelName, searchLimit);

            var channelWithName = channels
                .Where(c => c.Name.Equals(channelName))
                .FirstOrDefault();

            return channelWithName;
        }

        private static async Task<IEnumerable<SlackChannel>> RecurseChannelPagesUntilChannelExists(ISlackApi slackApi, string token, SlackChannelsResponse slackChannelsResponse, string channelName, int searchLimit)
        {
            var slackChannels = new List<SlackChannel>();

            slackChannels.AddRange(slackChannelsResponse.Channels);

            if(slackChannels.Any(c => c.Name.Equals(channelName)))
            {
                return slackChannels;
            }

            if (slackChannelsResponse.NextCursor != null && !string.IsNullOrWhiteSpace(slackChannelsResponse.NextCursor.Cursor))
            {
                var nextPageResponse = await slackApi.GetChannels(token, searchLimit, slackChannelsResponse.NextCursor.Cursor);

                slackChannels.AddRange(await RecurseChannelPagesUntilChannelExists(slackApi, token, nextPageResponse, channelName, searchLimit));
            }

            return slackChannels;
        }
    }
}
