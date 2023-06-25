using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pela.Core;

namespace Pela.Cli
{
    public partial class Startup : BackgroundService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IAreaReader _areaReader;
        private readonly IAssistantReader _assistantReader;
        private readonly InputOptions _options;

        public Startup(
            IHostApplicationLifetime lifetime,
            IAreaReader areaReader,
            IAssistantReader assistantReader,
            IOptions<InputOptions> options)
        {
            _lifetime = lifetime;
            _areaReader = areaReader;
            _assistantReader = assistantReader;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            if (!File.Exists(_options.AreaFile))
            {
                throw new InvalidOperationException(
                    $"Area file does not exist. Path: '{_options.AreaFile}'");
            }

            if (!File.Exists(_options.AssistantFile))
            {
                throw new InvalidOperationException(
                    $"Assistant file does not exist. Path: '{_options.AssistantFile}'");
            }

            var areas = 
                await _areaReader
                    .ReadAsync(_options.AreaFile)
                    .ToListAsync();

            var assistants =
                await _assistantReader
                    .ReadAsync(_options.AssistantFile)
                    .ToListAsync();

            foreach (var area in areas
                .OrderByDescending(area => area.Priority)
                .ThenByDescending(area => area.Value))
            {
                var finder = new SolutionFinder(area, assistants);
                var solution = finder.FindSolution();

                foreach (var assistant in solution.Assistants)
                {
                    assistants.Remove(assistant);
                }

                Console.WriteLine(solution);
            }

            _lifetime.StopApplication();
        }
    }
}
