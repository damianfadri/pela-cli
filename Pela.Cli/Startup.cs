using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pela.Core;
using System.Text;

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
            await Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(_options.AreaFile))
            {
                throw new InvalidOperationException(
                    $"Area file does not exist. Path: '{_options.AreaFile}'");
            }

            if (string.IsNullOrWhiteSpace(_options.AssistantFile))
            {
                throw new InvalidOperationException(
                    $"Assistant file does not exist. Path: '{_options.AssistantFile}'");
            }

            var areas = 
                _areaReader
                    .ReadAsync(_options.AreaFile)
                    .ToBlockingEnumerable();

            var assistants =
                _assistantReader
                    .ReadAsync(_options.AssistantFile)
                    .ToBlockingEnumerable();

            var solutions = new Solver(areas, assistants).Solve();

            foreach (var solution in solutions)
            {
                LogSolution(solution);
            }

            _lifetime.StopApplication();
        }

        private void LogSolution(Solution solution)
        {
            var sb = new StringBuilder();

            sb.AppendLine(solution.Area.Name);
            foreach (var assistant in solution.Assistants)
            {
                sb.AppendLine($"  {assistant.Name}");
            }
            
            if (!solution.IsSolved)
            {
                sb.AppendLine();
                sb.AppendLine("  Modifications:");
                if (solution.TourDuration < solution.Area.TourDuration)
                {
                    sb.AppendLine($"    Tour Duration: +{solution.Area.TourDuration - solution.TourDuration}");
                }

                if (solution.EducationalValue < solution.Area.EducationalValue)
                {
                    sb.AppendLine($"    Educational Value: +{solution.Area.EducationalValue - solution.EducationalValue}");
                }

                if (solution.VisitorAppeal < solution.Area.VisitorAppeal)
                {
                    sb.AppendLine($"    Visitor Appeal: +{solution.Area.VisitorAppeal - solution.VisitorAppeal}");
                }
            }

            Console.WriteLine(sb);
        }
    }
}
