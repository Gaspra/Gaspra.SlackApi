using Gaspra.SlackApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gaspra.SlackApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSlackApiFactory(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<ISlackApiFactory, SlackApiFactory>();

            return serviceCollection;
        }

        public static IServiceCollection AddSlackApi(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .TryAddSingleton<ISlackApiFactory, SlackApiFactory>();

            serviceCollection
                .AddSingleton((services) => services
                    .GetService<ISlackApiFactory>()
                    .CreateSlackApi());

            return serviceCollection;
        }

        public static IServiceCollection AddAuthorisedSlackApi(
            this IServiceCollection serviceCollection,
            string slackApiToken)
        {
            serviceCollection
                .TryAddSingleton<ISlackApiFactory, SlackApiFactory>();

            serviceCollection
                .AddSingleton((services) => services
                    .GetService<ISlackApiFactory>()
                    .CreateAuthorisedSlackApi(slackApiToken));

            return serviceCollection;
        }
    }
}
