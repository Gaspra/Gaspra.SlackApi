using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Extensions
{
    public static class SlackApiExtensions
    {
        public static async Task<SlackChannel> GetSlackChannelWithName(
            this ISlackApi slackApi,
            string token,
            string channelName)
        {
            var channels = await slackApi.GetChannels(token);

            var channelWithName = channels
                .Channels
                .Where(c => c.Name.Equals(channelName))
                .FirstOrDefault();

            return channelWithName;
        }
    }
}
