using Aoc.Lib.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Client.Solutions
{
    public class Day01 : ISolver
    {
        public string Indata => File.ReadAllText("day-01.in");
        public string ProblemName => "Problem 1 name";
        public string Day => "Day01";

        public IEnumerable<object> Solve()
        {
            yield return 0;
        }
    }
}