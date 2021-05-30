using Aoc.Lib.Utils;
using System;
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
        public void BuildSolution_SolutionExistsReturnsFails()
        {
            SolutionUtils utils = new SolutionUtils(new Lib.SystemConfig(fixture.TestDataFolder));
            Assert.True(utils.BuildSolution(1).IsFailure);
        }

        [Fact]
        public void BuildSolution_BuiltSolutionReturnsSuccess()
        {
            SolutionUtils utils = new SolutionUtils(new Lib.SystemConfig(fixture.TestDataFolder));
            int nonExistingDay = 2;
            Assert.True(utils.BuildSolution(nonExistingDay).IsSuccess);
            File.Delete(utils.GetSolutionUrl(nonExistingDay));
        }
        #endregion
    }
}
