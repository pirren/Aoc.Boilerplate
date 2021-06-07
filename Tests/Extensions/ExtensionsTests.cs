using Aoc.Lib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Aoc.Tests.Extensions
{
    public class ExtensionsTests
    {
        #region IntExtensions
        [Fact] 
        public void DayInRange_InRangeReturnsTrue()
        {
            var inrange = Enumerable.Range(1, 24);
            Assert.True(inrange.All(i => i.DayInRange() == true));
        }

        [Fact]
        public void DayInRange_OutOfRangeReturnsFalse()
        {
            var inrange = new List<int> { -1, 0, 25 };
            Assert.True(inrange.All(i => i.DayInRange() == false));
        }

        [Fact]
        public void TemplateNumberToPrint_LowNumberReturnsExtendedFormat()
        {
            int number = 5;
            string expected = $"0{number}";
            Assert.Equal(expected, number.SolverNumberToPrint());
        }

        [Fact]
        public void TemplateNumberToPrint_HighNumberReturnsShortFormat()
        {
            int number = 20;
            string expected = number.ToString();
            Assert.Equal(expected, number.SolverNumberToPrint());
        }

        [Fact]
        public void TemplateNumberToPrint_ReturnsString()
        {
            int testValue = 5;
            Assert.IsType<string>(testValue.SolverNumberToPrint());
        }

        [Fact]
        public void ProblemPartToString_InvalidInputThrows()
        {
            int testValue = 5;

            var ex = Assert.Throws<System.Exception>(() => testValue.ProblemPartToString());
            Assert.Contains("Undefined part choice:", ex.Message);
        }

        [Fact]
        public void ProblemPartToString_ValidInputReturnsNewFormat()
        {
            int[] testValue = new[] { 1, 2 };

            Assert.Contains("Part One:", testValue[0].ProblemPartToString());
            Assert.Contains("Part Two:", testValue[1].ProblemPartToString());
        }
        #endregion
    }
}
