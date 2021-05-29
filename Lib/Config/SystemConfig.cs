using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Lib.Config
{
    public class SystemConfig
    {
        public string AocVersion { get; }
        public SystemConfig(IConfiguration config)
        {
            AocVersion = config.GetValue<string>("AocVersion") ?? String.Empty;
        }
    }
}
