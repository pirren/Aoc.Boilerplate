using Microsoft.Extensions.Configuration;
using System;

namespace Aoc.Lib
{
    public class SystemConfig
    {
        public string AocVersion { get; }
        public string SolutionsBasePath { get; }
        public SystemConfig(IConfiguration config)
        {
            AocVersion = config.GetValue<string>("AocVersion") ?? String.Empty;
            SolutionsBasePath = config.GetValue<string>("SolutionsRootFolder") ?? String.Empty;
        }

        public SystemConfig(string basePath)
        {
            SolutionsBasePath = basePath;
        }
    }
}
