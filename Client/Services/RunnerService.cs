using Aoc.Client.Core;
using Aoc.Lib;
using Aoc.Lib.Extensions;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Aoc.Lib.Workers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc.Client.Services
{
    public class RunnerService : WorkerBase
    {
        private const string solutionTemplate = @"            using Aoc2020.Client.Services;
            using Microsoft.Extensions.Configuration;
            using Microsoft.Extensions.DependencyInjection;
            using Microsoft.Extensions.Hosting;
            using Serilog;
            using System;
            using System.IO;
            ";

        private readonly IServiceScopeFactory scopeFactory;

        public RunnerService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();

            var core = scope.ServiceProvider.GetRequiredService<IProgramCore>();

            while (!cancellationToken.IsCancellationRequested)
            {
                core.Run();

                //int day = 2;
                //var result = solutionUtils.BuildTemplate(day);
                //if (result.IsSuccess) Console.WriteLine("Solution for day {0} successfully created", day);
                //else Console.WriteLine("Could not create solution: {0}", result.Error);
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
