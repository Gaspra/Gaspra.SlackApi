using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Extensions
{
    public static class AuthorisedSlackApiExtensions
    {
        public static async Task<SlackChannel> GetSlackChannelWithName(
            this IAuthorisedSlackApi authorisedSlackApi,
            string channelName)
        {
            var channels = await authorisedSlackApi.GetChannels();

            var channelWithName = channels
                .Channels
                .Where(c => c.Name.Equals(channelName))
                .FirstOrDefault();

            return channelWithName;
        }
    }
}
