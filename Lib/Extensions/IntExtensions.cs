using System.Text;

namespace Aoc.Lib.Extensions
{
    public static class IntExtensions
    {
        public static string TemplateNumberToPrint(this int day)
        {
            return day > 9 ? day.ToString() : new StringBuilder().Append('0').Append(day).ToString();
        }

        public static string ProblemPartToString(this int part)
        {
            if (part == 1) return "Part One: ";
            if (part == 2) return "Part Two: ";
            throw new System.Exception(string.Format("Undefined part choice: {0}", part));
        }
    }
}
