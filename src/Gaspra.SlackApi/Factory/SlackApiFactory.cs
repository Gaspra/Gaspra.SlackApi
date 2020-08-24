using Gaspra.SlackApi.Extensions;
using Gaspra.SlackApi.Interfaces;
using Refit;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Factory
{
    public class SlackApiFactory : ISlackApiFactory
    {
        public IAuthorisedSlackApi CreateAuthorisedSlackApi(string authorisationToken)
        {
            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(authorisationToken)
            };

            var authorisedSlackApi = RestService.For<IAuthorisedSlackApi>(
                SlackUrls.Slack,
                refitSettings);

            return authorisedSlackApi;
        }

        public ISlackApi CreateSlackApi()
        {
            var slackApi = RestService.For<ISlackApi>(SlackUrls.Slack);

            return slackApi;
        }
    }
}
