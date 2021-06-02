using Aoc.Lib.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Client.Solutions
{
    [Problem("Day02", "Haxxing day two!")]
    public class Day02 : ISolver
    {
        public string Data { get; private set; }

        public Day02()
        {
            Data = File.ReadAllText("Solutions/Day02/day-02.in");
        }

        public IEnumerable<object> Solve()
        {
            yield return 0;
            yield return 0;
        }
    }
}
