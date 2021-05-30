using Aoc.Lib;
using Aoc.Lib.Utils;
using Aoc.Lib.Workers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc.Client.Services
{
    public class MainService : WorkerBase
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

        public MainService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();


            var solutionUtils = scope.ServiceProvider.GetRequiredService<SolutionUtils>();
            var config = scope.ServiceProvider.GetRequiredService<SystemConfig>();


            Console.WriteLine("Aoc{0}", config.AocVersion);

            //var result = solutionUtils.BuildSolution(day);
            //if (result.IsSuccess) Console.WriteLine("Solution for day {0} successfully created", day);
            //else Console.WriteLine("Could not create solution: {0}", result.Error);

            //Console.WriteLine("RunAsync from MainService");
        }

        protected override async Task RunningAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RunningAsync from MainService");
            await RunAsync(cancellationToken);
        }
    }
}
