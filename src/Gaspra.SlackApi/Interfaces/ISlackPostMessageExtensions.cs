using Gaspra.SlackApi.Models.Enums;
using Gaspra.SlackApi.Models.MessageBlocks;
using Gaspra.SlackApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Interfaces
{
    public interface ISlackPostMessageExtensions
    {
        [Obsolete("Please use the method: [PostMessageWithRateLimitRetry] instead")]
        Task<SlackPostMessageResponse> PostMessageWithRetryOnErrors(
            string token,
            string channel,
            string message,
            int retryCount,
            Func<int, TimeSpan> waitTime,
            IList<ErrorTypes> retryErrorTypes);

        Task<SlackPostMessageResponse> PostMessageWithRateLimitRetry(
            string token,
            string channel,
            string message,
            int retryCount = 5);

        Task<SlackPostMessageResponse> PostBlockMessageWithRateLimitRetry(
            string token,
            string channel,
            string backupMessage,
            IList<ISlackMessageBlock> slackMessageBlocks,
            int retryCount = 5);

        Task<SlackPostMessageResponse> PostMessageInThreadWithRateLimitRetry(
            string token,
            string channel,
            string thread,
            string message,
            int retryCount = 5);

        Task<SlackPostMessageResponse> PostBlockMessageInThreadWithRateLimitRetry(
            string token,
            string channel,
            string thread,
            string backupMessage,
            IList<ISlackMessageBlock> slackMessageBlocks,
            int retryCount = 5);
    }
}
