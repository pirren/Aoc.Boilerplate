using Microsoft.Extensions.Configuration;
using System;

namespace Aoc.Configuration
{
    public class SystemConfig
    {
        public string AocVersion { get; }
        public string SolutionsBasePath { get; }
        public string AsciiUrl { get; }
        public SystemConfig(IConfiguration config)
        {
            AocVersion = config.GetValue<string>("AocVersion") ?? String.Empty;
            SolutionsBasePath = config.GetValue<string>("SolutionsRootFolder") ?? String.Empty;
            AsciiUrl = config.GetValue<string>("AsciiUrl") ?? String.Empty;
        }

        public SystemConfig(string basePath) // todo: get rid of this crap 
        {
            SolutionsBasePath = basePath;
        }
    }
}
