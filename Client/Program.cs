using Aoc.Client.Services;
using Aoc.Lib;
using Aoc.Lib.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Aoc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting Application...");
                CreateHostBuilder(args).Build().Run();
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

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
            .AddJsonFile("serilog.Development.json", optional: true, reloadOnChange: true)
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostCtx, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.Development.json", true);
        })
        .ConfigureServices((hostCtx, services) =>
        {
            services.AddSingleton<SystemConfig>();
            services.AddSingleton<SolutionUtils>();
            services.AddSingleton(_ => hostCtx.Configuration);
            services.AddHostedService<MainService>();
        })
        .UseSerilog();
    }
}
