using System;
using System.Text;

namespace Aoc.Lib.Extensions
{
    public static class StringExtensions
    {
        public static string TemplateNumberToPrint(this int day)
        {
            return day > 9 ? day.ToString() : new StringBuilder().Append('0').Append(day).ToString();
        }
    }
}
