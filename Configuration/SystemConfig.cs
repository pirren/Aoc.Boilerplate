using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Aoc.Configuration
{
    public class SystemConfig
    {
        public virtual string SolutionsBasePath { get; }
        public virtual string[] TemplateBase { get; }
        public string AocVersion { get; }
        public string AsciiUrl { get; }

        public SystemConfig(IConfiguration config)
        {
            SolutionsBasePath = config.GetValue<string>("SolutionsRootFolder") ?? String.Empty;
            TemplateBase = File.ReadAllLines(config.GetValue<string>("TemplateBaseUrl"));
            AocVersion = config.GetValue<string>("AocVersion") ?? String.Empty;
            AsciiUrl = config.GetValue<string>("AsciiUrl") ?? String.Empty;
        }

        public SystemConfig() { }
    }
}
