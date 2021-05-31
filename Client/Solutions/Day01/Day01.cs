using Aoc.Lib.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Client.Solutions
{
    [Problem("Day01", "Problem 1 name")]
    public class Day01 : ISolver
    {
        public string Data { get; private set; }

        public Day01()
        {
            Data = File.ReadAllText("Solutions/Day01/day-01.in");
        }

        public IEnumerable<object> Solve()
        {
            yield return 0;
            yield return 0;
        }
    }
}