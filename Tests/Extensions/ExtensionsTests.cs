using Aoc.Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aoc.Tests.Extensions
{
    public class ExtensionsTests
    {
        [Fact]
        public void TemplateNumberToPrint_LowNumberReturnsExtendedFormat()
        {
            int number = 5;
            string expected = new StringBuilder().Append("0").Append(number).ToString();
            Assert.Equal(expected, number.TemplateNumberToPrint());
        }

        [Fact]
        public void TemplateNumberToPrint_HighNumberReturnsShortFormat()
        {
            int number = 20;
            string expected = number.ToString();
            Assert.Equal(expected, number.TemplateNumberToPrint());
        }

        [Fact]
        public void TemplateNumberToPrint_ReturnsString()
        {
            int testValue = 5;
            Assert.IsType<string>(testValue.TemplateNumberToPrint());
        }
    }
}
