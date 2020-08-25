using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polly;
using Refit;
using Newtonsoft.Json;
using Gaspra.SlackApi.Models.Enums;
using Gaspra.SlackApi.Models.MessageBlocks;

namespace Gaspra.SlackApi
{
    public class SlackPostMessageExtensions : ISlackPostMessageExtensions
    {
        private readonly ISlackApi _slackApi;

        public SlackPostMessageExtensions(ISlackApi slackApi)
        {
            _slackApi = slackApi;
        }

        public async Task<SlackPostMessageResponse> PostMessageWithRetryOnErrors(string token, string channel, string message, int retryCount, Func<int, TimeSpan> waitTime, IList<ErrorTypes> retryErrorTypes)
        {
            var retryPolicy = Policy
                .Handle<ApiException>(e =>
                {
                    var slackFailedResponse = JsonConvert.DeserializeObject<SlackFailedResponse>(e.Content);

                    return retryErrorTypes.Contains(slackFailedResponse.Error);
                })
                .WaitAndRetryAsync(retryCount, waitTime);

            SlackPostMessageResponse response = null;

            var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                response = await _slackApi.PostMessage(token, channel, message);
            });

            if (policyResult.Outcome.Equals(OutcomeType.Failure))
            {
                throw policyResult.FinalException;
            }

            return response;
        }

        public async Task<SlackPostMessageResponse> PostMessageWithRateLimitRetry(string token, string channel, string message, int retryCount = 5)
        {
            var retryPolicy = Policy
                .Handle<ApiException>(e =>
                {
                    var slackFailedResponse = JsonConvert.DeserializeObject<SlackFailedResponse>(e.Content);

                    return slackFailedResponse.Error.Equals(ErrorTypes.RateLimited);
                })
                .WaitAndRetryAsync(retryCount, (retry) => TimeSpan.FromMilliseconds(100 * retry));

            SlackPostMessageResponse response = null;

            var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                response = await _slackApi.PostMessage(token, channel, message);
            });

            if (policyResult.Outcome.Equals(OutcomeType.Failure))
            {
                throw policyResult.FinalException;
            }

            return response;
        }

        public async Task<SlackPostMessageResponse> PostBlockMessageWithRateLimitRetry(string token, string channel, string backupMessage, IList<ISlackMessageBlock> slackMessageBlocks, int retryCount = 5)
        {
            var retryPolicy = Policy
                .Handle<ApiException>(e =>
                {
                    var slackFailedResponse = JsonConvert.DeserializeObject<SlackFailedResponse>(e.Content);

                    return slackFailedResponse.Error.Equals(ErrorTypes.RateLimited);
                })
                .WaitAndRetryAsync(retryCount, (retry) => TimeSpan.FromMilliseconds(100 * retry));

            SlackPostMessageResponse response = null;

            var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                var blocks = JsonConvert.SerializeObject(slackMessageBlocks, Formatting.None);

                response = await _slackApi.PostMessageWithBlocks(token, channel, backupMessage, blocks);
            });

            if (policyResult.Outcome.Equals(OutcomeType.Failure))
            {
                throw policyResult.FinalException;
            }

            return response;
        }
    }
}
