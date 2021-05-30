using Aoc.Lib;
using Aoc.Lib.Utils;
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
            SolutionUtils utils = new(new SystemConfig(fixture.TestDataFolder));
            Assert.True(utils.GenerateTemplate(1).IsFailure);
        }

        [Fact]
        public void GenerateTemplate_BuiltSolutionReturnsSuccess()
        {
            SolutionUtils utils = new(new SystemConfig(fixture.TestDataFolder));
            int nonExistingDay = 2;
            Assert.True(utils.GenerateTemplate(nonExistingDay).IsSuccess);
            File.Delete(utils.GetTemplateUrl(nonExistingDay));
        }
        #endregion
    }
}
