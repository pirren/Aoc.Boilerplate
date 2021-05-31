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
        /// Gets both solutions
        /// </summary>
        /// <param name="solver"></param>
        /// <returns>Result</returns>
        public static Result<object[]> GetSolutions(this ISolver solver)
        {
            return Result.Ok(solver.Solve().ToArray());
        }

        /// <summary>
        /// Gets a single solution
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="choice"></param>
        /// <returns>Result</returns>
        public static Result<object> GetSolution(this ISolver solver, Solution choice)
        {
            IEnumerable<object> solutions = solver.Solve();
            return Result.Ok(choice == Solution.One ? solutions.First() : solutions.Last());
        }

        public static Problem GetProblemInfo(this ISolver solver)
        {
            return (Problem)System.Attribute.GetCustomAttributes(solver.GetType()).First();
        }
    }
}
