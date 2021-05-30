using Microsoft.Extensions.Configuration;
using System;

namespace Aoc.Lib
{
    public class SystemConfig
    {
        public string AocVersion { get; }
        public string SolutionBasePath { get; }
        public SystemConfig(IConfiguration config)
        {
            AocVersion = config.GetValue<string>("AocVersion") ?? String.Empty;
            SolutionBasePath = config.GetValue<string>("SolutionBasePath") ?? String.Empty;
        }

        public SystemConfig(string basePath)
        {
            SolutionBasePath = basePath;
        }
    }
}
