using Gaspra.SlackApi.Interfaces;
using Gaspra.SlackApi.Models;
using Gaspra.SlackApi.Models.Enums;
using Gaspra.SlackApi.Models.Responses;
using Newtonsoft.Json;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gaspra.SlackApi
{
    public class SlackUserExtensions : ISlackUserExtensions
    {
        private readonly ISlackApi _slackApi;

        public SlackUserExtensions(ISlackApi slackApi)
        {
            _slackApi = slackApi;
        }

        public async Task<IList<SlackUser>> GetAllSlackUsers(string token, int searchLimit = 50)
        {
            var retryPolicy = Policy
                .Handle<ApiException>(e =>
                {
                    var slackFailedResponse = JsonConvert.DeserializeObject<SlackFailedResponse>(e.Content);

                    return slackFailedResponse.Error.Equals(ErrorTypes.RateLimited);
                })
                .WaitAndRetryAsync(10, (retry) => TimeSpan.FromMilliseconds(retry * 200));

            var endSearch = false;

            var apiCursor = "";

            var slackUsers = new List<SlackUser>();

            while (!endSearch)
            {
                var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
                {
                    var userResponse = await _slackApi.GetUsers(token, searchLimit, apiCursor);

                    if (userResponse.Ok)
                    {
                        slackUsers.AddRange(userResponse
                            .Users);

                        apiCursor = userResponse
                            .NextCursor
                            .Cursor;

                        if (string.IsNullOrWhiteSpace(apiCursor))
                        {
                            // we've got to the end of the search results
                            endSearch = true;
                        }
                    }
                    else
                    {
                        // something when wrong when communicating with
                        // the slack, end early and return null
                        endSearch = true;
                    }
                });

                if (policyResult.Outcome.Equals(OutcomeType.Failure))
                {
                    // we failed all the retry attempts or something else
                    // went wrong throw the exception
                    throw policyResult.FinalException;
                }
            }

            return slackUsers;
        }

        public async Task<SlackUser> GetSlackUserWithName(string token, string name, int searchLimit = 50)
        {
            var retryPolicy = Policy
                .Handle<ApiException>(e =>
                {
                    var slackFailedResponse = JsonConvert.DeserializeObject<SlackFailedResponse>(e.Content);

                    return slackFailedResponse.Error.Equals(ErrorTypes.RateLimited);
                })
                .WaitAndRetryAsync(10, (retry) => TimeSpan.FromMilliseconds(retry * 200));

            var endSearch = false;

            SlackUser userWithName = null;

            var apiCursor = "";

            while (!endSearch && userWithName == null)
            {
                var policyResult = await retryPolicy.ExecuteAndCaptureAsync(async () =>
                {
                    var userResponse = await _slackApi.GetUsers(token, searchLimit, apiCursor);

                    if (userResponse.Ok)
                    {
                        userWithName = userResponse
                            .Users
                            .Where(u => u.Name.Equals(name))
                            .FirstOrDefault();

                        apiCursor = userResponse
                            .NextCursor
                            .Cursor;

                        if (string.IsNullOrWhiteSpace(apiCursor))
                        {
                            // we've got to the end of the search results
                            endSearch = true;
                        }
                    }
                    else
                    {
                        // something when wrong when communicating with
                        // the slack, end early and return null
                        endSearch = true;
                    }
                });

                if (policyResult.Outcome.Equals(OutcomeType.Failure))
                {
                    // we failed all the retry attempts or something else
                    // went wrong throw the exception
                    throw policyResult.FinalException;
                }
            }

            return userWithName;
        }
    }
}
