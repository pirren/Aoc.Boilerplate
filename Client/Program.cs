using Aoc.Client.Core;
using Aoc.Client.Services;
using Aoc.Lib.Config;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aoc.Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            SystemUtils.PrintAsciiHeader(AppConfig, ConsoleColor.Green);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(SerilogConfiguration)
                .CreateLogger();

            try
            {
                Log.Information("Starting Application...");
                using var host = CreateHostBuilder(args).UseConsoleLifetime().Build();
                await host.RunAsync();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IConfiguration SerilogConfiguration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
            .AddJsonFile("serilog.Development.json", optional: true, reloadOnChange: true)
            .Build();

        public static IConfiguration AppConfig { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostCtx, config) =>
            {
                config.AddConfiguration(AppConfig);
            })
            .ConfigureServices((hostCtx, services) =>
            {
                services.AddSingleton<IProgramCore, ProgramCore>();
                services.AddSingleton<SystemConfig>();
                services.AddSingleton<TemplateConfig>();
                services.AddSingleton<SolutionUtils>();
                services.AddSingleton(_ => hostCtx.Configuration);
                services.AddHostedService<RunnerService>();
            })
            .UseSerilog();
    }
}
