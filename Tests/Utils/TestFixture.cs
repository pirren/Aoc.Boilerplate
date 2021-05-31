using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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
