using Gaspra.SlackApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Gaspra.SlackApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSlackApi(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<ISlackApiFactory, SlackApiFactory>();

            return serviceCollection;
        }
    }
}
