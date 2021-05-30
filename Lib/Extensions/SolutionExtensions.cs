using Aoc.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Lib.Extensions
{
    public static class SolutionExtensions
    {
        public static void ProcessSolutions(this ISolver solver)
        {
            "\n ========================================\n".Print();
            "Problem name: ".Print();
            solver.ProblemName.Print(ConsoleColor.Red);
            "Day: ".Print();
            solver.Day.Last().ToString().Print(ConsoleColor.DarkRed);
            //Stopwatch Stopwatch = new Stopwatch();
            //Stopwatch.Start();
            var i = 1;
            foreach (var solution in solver.Solve())
            {
                $"Part {i}: {solution}\n".Print();
                i = 1 + i;
            }
            //Stopwatch.Stop();
            "\nFinished".Print();
                //, (Stopwatch.ElapsedMilliseconds < 40) ? ConsoleColor.Green : ConsoleColor.Red);
            "\n========================================\n".Print();
        }
    }
}
