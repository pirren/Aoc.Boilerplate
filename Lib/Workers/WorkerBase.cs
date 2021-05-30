using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc.Lib.Workers
{
    public abstract class WorkerBase : BackgroundService
    {
        protected Task ExecutingTask { get; set; }
        protected static AutoResetEvent AutoResetEvent = new AutoResetEvent(false);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            ExecutingTask = RunningAsync(cancellationToken);
            await ExecutingTask;
        }

        /// <summary>
        /// Executed task that needs to be overriden
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected abstract Task RunningAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Main Method that needs to be overriden. It runs forever
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task RunAsync(CancellationToken cancellationToken);

        public override async Task StopAsync(CancellationToken cancellationToken)
            => await Task.WhenAny(ExecutingTask, Task.Delay(TimeSpan.FromMilliseconds(3000), cancellationToken));
    }
}
