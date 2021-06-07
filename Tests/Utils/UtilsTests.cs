using Aoc.Configuration;
using Aoc.Lib.Extensions;
using Aoc.Lib.Utils;
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

        private SolverUtils GetSolutionUtils()
        {
            var mockConfig = new Mock<SystemConfig>();
            mockConfig.Setup(x => x.SolutionsBasePath).Returns(fixture.SolutionsRootFolder);
            mockConfig.Setup(x => x.TemplateBase).Returns(new string[] { File.ReadAllText(fixture.TemplateUrl) });

            var utils = new SolverUtils(mockConfig.Object)
            {
                SolutionsNamespace = "Aoc.Test.Solutions"
            };
            return utils;
        }

        #region SolutionUtils
        [Fact]
        public void GetTemplateUrl_DayInRangeReturnsUrl()
        {
            var utils = GetSolutionUtils();
            int day = 20;
            var res = utils.GetFullSolverUrl(day);

            Assert.True(utils.GetFullSolverUrl(day).Length > 0);
        }

        [Fact]
        public void GetTemplateUrl_DayOutOfRangeReturnsEmpty()
        {
            var utils = GetSolutionUtils();
            int day = 25;

            Assert.True(utils.GetFullSolverUrl(day) == string.Empty);
        }

        [Fact]
        public void GetTemplateFolderUrl_DayInRangeReturnsUrl()
        {
            var utils = GetSolutionUtils();
            int day = 20;

            Assert.True(utils.GetSolverFolderFullUrl(day).Length > 0);
        }

        [Fact]
        public void GetTemplateFolderUrl_DayOutOfRangeReturnsEmpty()
        {
            var utils = GetSolutionUtils();
            int day = 25;
            Assert.True(utils.GetSolverFolderFullUrl(day) == string.Empty);
        }

        [Fact]
        public void GetTemplateShortName_ExpectedOutput()
        {
            var utils = GetSolutionUtils();

            var actual = utils.GetSolverName(1);
            string expected = "Day01";

            Assert.True(expected == actual);
        }

        [Fact]
        public void TemplateExists_ExistingSolverExists()
        {
            var utils = GetSolutionUtils();
            var result = utils.SolverExists(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void TemplateExists_BadRequestIsFailure()
        {
            var utils = GetSolutionUtils();
            var result = utils.SolverExists(5);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void GetSolvers_ReturnsExisting()
        {
            var utils = GetSolutionUtils();
            var solvers = utils.GetSolvers();

            Assert.True(solvers.Count == 1);
        }

        [Fact]
        public void GetSolvers_AppliedFilterReturnsExpected()
        {
            var utils = GetSolutionUtils();

            string existingSolver = "Day01";
            string nonExistingSolver = "Day02";

            var expected = utils.GetSolvers(existingSolver);
            var expectedEmpty = utils.GetSolvers(nonExistingSolver);

            Assert.True(expected.Count == 1);
            Assert.True(expectedEmpty.Count == 0);
        }

        [Fact]
        public void GenerateTemplate_SolutionExistsReturnsFails()
        {
            SolverUtils utils = GetSolutionUtils();
            Assert.True(utils.GenerateSolver(1, "Testname").IsFailure);
        }

        [Fact]
        public void GenerateTemplate_BuiltSolutionReturnsSuccess()
        {
            SolverUtils utils = GetSolutionUtils();
            int nonExistingDay = 2;
            string indataUrl = Path.Combine(utils.GetSolverFolderFullUrl(nonExistingDay), $"day-{nonExistingDay.SolverNumberToPrint()}.in");

            Assert.True(utils.GenerateSolver(nonExistingDay, "Testday").IsSuccess);
            Assert.True(File.Exists(utils.GetFullSolverUrl(nonExistingDay)));
            Assert.True(File.Exists(indataUrl));

            // cleanup
            File.Delete(utils.GetFullSolverUrl(nonExistingDay));
            File.Delete(indataUrl);
        }
        #endregion
    }
}
