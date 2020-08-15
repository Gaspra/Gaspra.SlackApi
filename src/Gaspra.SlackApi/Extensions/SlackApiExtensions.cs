using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using Gaspra.SlackApi.Models.Enums;
using System;
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
            int searchLimit = 100,
            List<ChannelTypes> channelTypes = null,
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if(channelTypes == null)
            {
                channelTypes = new List<ChannelTypes> { ChannelTypes.PrivateChannel, ChannelTypes.PublicChannel };
            }

            var channelTypesString = string.Join(",", channelTypes.Select(t => t.GetDescription()));

            var endSearch = false;

            SlackChannel channelWithName = null;

            var apiCursor = "";

            while (!endSearch && channelWithName == null)
            {
                var channelResponse = await slackApi.GetChannels(token, channelTypesString, searchLimit, apiCursor);

                if (channelResponse.Ok)
                {
                    channelWithName = channelResponse
                        .Channels
                        .Where(c => c.Name.Equals(channelName, stringComparison))
                        .FirstOrDefault();

                    apiCursor = channelResponse
                        .NextCursor
                        .Cursor;
                }
                else
                {
                    // something when wrong when communicating with
                    // the slack, end early and return null
                    endSearch = true;
                }
            }

            return channelWithName;
        }

        public static async Task<SlackUser> GetSlackUserWithName(
            this ISlackApi slackApi,
            string token,
            string name,
            int searchLimit = 50)
        {
            var endSearch = false;

            SlackUser userWithName = null;

            var apiCursor = "";

            while (!endSearch && userWithName == null)
            {
                var userResponse = await slackApi.GetUsers(token, searchLimit, apiCursor);

                if (userResponse.Ok)
                {
                    userWithName = userResponse
                        .Users
                        .Where(u => u.Name.Equals(name))
                        .FirstOrDefault();

                    apiCursor = userResponse
                        .NextCursor
                        .Cursor;
                }
                else
                {
                    // something when wrong when communicating with
                    // the slack, end early and return null
                    endSearch = true;
                }
            }

            return userWithName;
        }
    }
}
