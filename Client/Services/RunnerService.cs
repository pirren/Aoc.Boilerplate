using Aoc.Lib;
using Aoc.Lib.Utils;
using Aoc.Lib.Workers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
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

            var solutionUtils = scope.ServiceProvider.GetRequiredService<SolutionUtils>();
            var config = scope.ServiceProvider.GetRequiredService<SystemConfig>();

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Aoc{0}", config.AocVersion);
                var solutions = solutionUtils.GetSolutions();

                object solution = Activator.CreateInstance(solutions.First());

                //int day = 2;
                //var result = solutionUtils.BuildTemplate(day);
                //if (result.IsSuccess) Console.WriteLine("Solution for day {0} successfully created", day);
                //else Console.WriteLine("Could not create solution: {0}", result.Error);

                Console.ReadLine();
            }
        }

        protected override async Task RunningAsync(CancellationToken cancellationToken)
        {
            Thread.Sleep(50);
            await Task.Factory.StartNew(async () => await RunAsync(cancellationToken));
            //await RunAsync(cancellationToken);
        }
    }
}
