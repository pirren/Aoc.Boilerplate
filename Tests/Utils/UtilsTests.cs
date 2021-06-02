using Aoc.Lib.Config;
using Aoc.Lib.Utils;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using Xunit;

namespace Aoc.Tests.Utils
{
    public class UtilsTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture fixture;
        public UtilsTests(TestFixture fixture)
        {
            this.fixture = fixture;
        }

        #region SolutionUtils
        [Fact]
        public void GenerateTemplate_SolutionExistsReturnsFails()
        {
            SolutionUtils utils = new(new SystemConfig(fixture.SolutionsRootFolder), null);
            Assert.True(utils.GenerateTemplate(1, "Testname").IsFailure);
        }

        [Fact]
        public void GenerateTemplate_BuiltSolutionReturnsSuccess()
        {
            SolutionUtils utils = new(new SystemConfig(fixture.SolutionsRootFolder), new TemplateConfig(new string[] { fixture.SolutionTestTemplate }));
            int nonExistingDay = 2;
            Assert.True(utils.GenerateTemplate(nonExistingDay, "Testday").IsSuccess);
            File.Delete(utils.GetTemplateUrl(nonExistingDay));
        }
        #endregion
    }
}
