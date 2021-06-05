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
        private const string TextStartup = "Found {0} problems!";
        private const string TextHelp = "Following operations are available:\n" +
            "\n\t[number] run single" +
            "\n\t[a] run all" +
            "\n\t[c] create template" +
            "\n\t[l] list templates" +
            "\n\t[ctrl+c] quit";

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

            VisualHelpers.Print(string.Format(TextStartup, solutionUtils.GetSolvers().Count), newLines: 2);
            VisualHelpers.Print(TextHelp, newLines: 1);

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
                VisualHelpers.Print(newLines: 1);
                await RunAsync(cancellationToken);
            });
        }
    }
}
