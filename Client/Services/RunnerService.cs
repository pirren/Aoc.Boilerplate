using Aoc.Lib.Helpers;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Aoc.Lib.Workers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc.Client.Services
{
    public class RunnerService : WorkerBase
    {
        private const string TextStartup = "Found {0} problems!\n\n";
        private const string TextHelp = "Following operations are available:\n" +
            "\n\t- [number] run single" +
            "\n\t- [a] run all" +
            "\n\t- [c] create template" +
            "\n\t- [l] list templates" +
            "\n\t- [ctrl+c] quit\n";

        private readonly IServiceScopeFactory scopeFactory;

        public RunnerService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();
            var core = scope.ServiceProvider.GetRequiredService<ICore>();
            var solutionUtils = scope.ServiceProvider.GetRequiredService<SolutionUtils>();

            VisualHelpers.Print(string.Format(TextStartup, solutionUtils.GetSolvers().Count));
            VisualHelpers.Print(TextHelp);

            while (!cancellationToken.IsCancellationRequested)
            {
                core.Run();
            }
        }

        protected override async Task RunningAsync(CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(async () =>
            {
                Thread.Sleep(300); // wait for host to finish
                Log.Information("Starting RunnerService");
                Console.Write('\n');
                await RunAsync(cancellationToken);
            });
        }
    }
}
