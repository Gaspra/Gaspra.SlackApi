using Gaspra.SlackApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Gaspra.SlackApi.Cache.Example
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((host, services) =>
            {
                services
                    .AddSlackApi()
                    .AddMemoryCache()
                    .AddHostedService<SlackApiService>();
            });
    }
}
