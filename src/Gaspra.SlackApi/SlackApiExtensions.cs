using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polly;
using Refit;
using System.Net;
using Newtonsoft.Json;
using Gaspra.SlackApi.Models.Enums;

namespace Gaspra.SlackApi
{
    public class SlackApiExtensions : ISlackApiExtensions
    {
        private readonly ISlackApi _slackApi;

        public SlackApiExtensions(ISlackApi slackApi)
        {
            _slackApi = slackApi;
        }

        public async Task<SlackPostMessageResponse> PostMessageWithRetry(string token, string channelId, string message, int retryCount = 3, long ticksBetweenRetries = 5000000)
        {
            var response = await PostMessageWithRetry(
                token,
                channelId,
                message,
                retryCount,
                (retry) => TimeSpan.FromTicks(ticksBetweenRetries),
                new List<ErrorTypes> { ErrorTypes.RateLimited });

            return response;
        }

        public async Task<SlackPostMessageResponse> PostMessageWithRetry(string token, string channelId, string message, int retryCount, Func<int, TimeSpan> waitTime, IList<ErrorTypes> retryErrorTypes)
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
                response = await _slackApi.PostMessage(token, channelId, message);
            });

            if (policyResult.Outcome.Equals(OutcomeType.Failure))
            {
                // todo; handle failure
                var finalException = policyResult.FinalException;
            }

            return response;
        }
    }
}
