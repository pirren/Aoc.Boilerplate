using System.Linq;
using System.Text;

namespace Aoc.Lib.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Get day of Solver in 0X format if short
        /// </summary>
        /// <param name="day"></param>
        /// <returns>String</returns>
        public static string SolverNumberToPrint(this int day)
        {
            return day > 9 ? day.ToString() : $"0{day}";
        }

        /// <summary>
        /// Get printable solution number, e.g. "Part One: "
        /// </summary>
        /// <param name="part"></param>
        /// <returns>String</returns>
        public static string ProblemPartToString(this int part)
        {
            if (part == 1) return "Part One: ";
            if (part == 2) return "Part Two: ";

            throw new System.Exception(string.Format("Undefined part choice: {0}", part));
        }

        /// <summary>
        /// Get whether or not given day is valid in 1-24 range
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool DayInRange(this int day) => Enumerable.Range(1,24).Contains(day);
    }
}
