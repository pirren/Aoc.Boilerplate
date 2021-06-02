using Microsoft.Extensions.Configuration;
using System.IO;

namespace Aoc.Configuration
{
    public class TemplateConfig
    {
        public string[] TemplateBase { get; }
        public TemplateConfig(IConfiguration config)
        {
            TemplateBase = File.ReadAllLines(config.GetValue<string>("TemplateBaseUrl"));
        }

        public TemplateConfig(string[] templateTestingBase) // todo: get rid of this crap 
        {
            TemplateBase = templateTestingBase;
        }
    }
}
