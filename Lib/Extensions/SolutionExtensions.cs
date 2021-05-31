using Aoc.Lib.Infrastructure;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Lib.Extensions
{
    public static class SolutionExtensions
    {
        /// <summary>
        /// Prints both problems
        /// </summary>
        /// <param name="solver"></param>
        /// <returns>Result</returns>
        public static Result SolveBoth(this ISolver solver)
        {
            try
            {
                IEnumerable<object> solutions = solver.Solve();
                PrintStartMessage(solver);
                SystemUtils.Print($"Part 1: {solutions.First() ?? 0}\n");
                SystemUtils.Print($"Part 2: {solutions.Last() ?? 0}\n");
                PrintFinishedMessage();
                return Result.Ok();
            }
            catch
            {
                return Result.Fail("Caught error trying to run sulutions");
            }
        }

        /// <summary>
        /// Prints a given problem 
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="choice"></param>
        /// <returns>Result</returns>
        public static Result SolveSingle(this ISolver solver, Solution choice)
        {
            try
            {
                IEnumerable<object> solutions = solver.Solve();
                PrintStartMessage(solver);
                SystemUtils.Print(new StringBuilder()
                        .Append((string)(choice == Solution.One ? solutions.First() : solutions.Last()))
                        .Append('\n')
                        .ToString()
                    );
                PrintFinishedMessage();
                return Result.Ok();
            }
            catch
            {
                return Result.Fail("Caught error trying to run sulutions");
            }
        }

        private static void PrintStartMessage(ISolver solver)
        {
            Problem problemInfo = (Problem)System.Attribute.GetCustomAttributes(solver.GetType()).First();

            SystemUtils.Print("\n========================================\n");
            SystemUtils.Print("Problem name: ");
            SystemUtils.Print(new StringBuilder().Append(problemInfo.GetName()).Append('\n').ToString(), ConsoleColor.Red); 
            SystemUtils.Print("Day: ");
            SystemUtils.Print(new StringBuilder().Append(problemInfo.GetDay()).Append("\n\n").ToString(), ConsoleColor.DarkRed); // fix this with Attribute reflection
        }

        private static void PrintFinishedMessage()
        {
            SystemUtils.Print("\nFinished");
            SystemUtils.Print("\n========================================\n");
        }
    }
}
