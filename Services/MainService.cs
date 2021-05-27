using Aoc2020.Lib.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc2020.Services
{
    public class MainService : WorkerBase
    {
        //private readonly ILogger<MainService> _log;

        public MainService()
        {

        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RunAsync from MainService");
        }

        protected override async Task RunningAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("RunningAsync from MainService");
            await RunAsync(cancellationToken);
        }
    }
}
