using Gaspra.SlackApi.Models.Enums;
using Gaspra.SlackApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    public interface ISlackApiExtensions
    {
        Task<SlackPostMessageResponse> PostMessageWithRetry(
            string token,
            string channelId,
            string message,
            int retryCount = 3,
            long ticksBetweenRetries = 5000000
        );

        Task<SlackPostMessageResponse> PostMessageWithRetry(
            string token,
            string channelId,
            string message,
            int retryCount,
            Func<int, TimeSpan> waitTime,
            IList<ErrorTypes> retryErrorTypes);
    }
}
