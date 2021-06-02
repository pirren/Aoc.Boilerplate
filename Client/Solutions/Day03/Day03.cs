using Aoc.Lib.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Client.Solutions
{
    [Problem("Day03", "Problem 3")]
    public class Day03 : ISolver
    {
        public string Data { get; private set; }

        public Day03()
        {
            Data = File.ReadAllText("Solutions/Day03/day-03.in");
        }

        public IEnumerable<object> Solve()
        {
            yield return 0;
            yield return 0;
        }
    }
}
