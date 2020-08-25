using Gaspra.SlackApi.Factory;
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
                .TryAddSingleton<ISlackApiFactory, SlackApiFactory>();

            return serviceCollection;
        }

        public static IServiceCollection AddSlackApi(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .TryAddSingleton<ISlackApiFactory, SlackApiFactory>();

            serviceCollection
                .TryAddSingleton((services) => services
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
                .TryAddSingleton((services) => services
                    .GetService<ISlackApiFactory>()
                    .CreateAuthorisedSlackApi(slackApiToken));

            return serviceCollection;
        }

        public static IServiceCollection AddSlackApiWithPostMessageExtensions(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSlackApi()
                .TryAddSingleton<ISlackPostMessageExtensions, SlackPostMessageExtensions>();

            return serviceCollection;
        }

        public static IServiceCollection AddSlackApiWithSlackUserExtensions(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSlackApi()
                .TryAddSingleton<ISlackUserExtensions, SlackUserExtensions>();

            return serviceCollection;
        }
    }
}
