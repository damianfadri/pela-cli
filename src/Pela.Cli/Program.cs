using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pela.Cli;
using Pela.Core;
using Pela.Infrastructure;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddCommandLine(args, new Dictionary<string, string>
                {
                    { "--assistants", $"{nameof(InputOptions)}:{nameof(InputOptions.AssistantFile)}" },
                    { "--areas", $"{nameof(InputOptions)}:{nameof(InputOptions.AreaFile)}" }
                });
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                services.Configure<InputOptions>(
                    configuration.GetSection(nameof(InputOptions)));

                services.AddLogging(builder =>
                {
                    builder.AddFilter("Microsoft", LogLevel.None);
                });

                services.AddTransient<IAreaReader, SimpleAreaReader>();
                services.AddTransient<IAssistantReader, SimpleAssistantReader>();

                services.AddHostedService<Startup>();
            })
            .RunConsoleAsync();
    }
}