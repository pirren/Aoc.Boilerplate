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
            SolutionsRootFolder = root["SolutionsRootFolder"];
            TemplateUrl = root["TemplateUrl"];
            if (!Directory.Exists(SolutionsRootFolder))
                throw new Exception(
                    $"Test data folder {SolutionsRootFolder} not found. Current directory: {Directory.GetCurrentDirectory()}");
        }

        public void Dispose()
        {
        }

        public string SolutionsRootFolder { get; }
        public string TemplateUrl { get; }
    }
}
