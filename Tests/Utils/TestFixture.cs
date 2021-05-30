using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Tests.Utils
{
    public class TestFixture : IDisposable
    {
        public TestFixture()
        {
            var root = new ConfigurationBuilder().AddJsonFile("utils-test-config.json").Build();
            TestDataFolder = root["SolutionsRootFolder"];
            if (!Directory.Exists(TestDataFolder))
                throw new Exception(
                    $"Test data folder {TestDataFolder} not found. Current directory: {Directory.GetCurrentDirectory()}");
        }

        public void Dispose()
        {
        }

        public string TestDataFolder { get; }

        public string ExcelTestFile(string filename) => Path.Combine(TestDataFolder, filename);
    }
}
