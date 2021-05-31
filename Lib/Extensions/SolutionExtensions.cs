using Aoc.Lib.Infrastructure;
using Aoc.Lib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Lib.Extensions
{
    public static class SolutionExtensions
    {
        /// <summary>
        /// Gets both solutions
        /// </summary>
        /// <param name="solver">ISolver</param>
        /// <returns>Result</returns>
        public static Result<object[]> Solutions(this ISolver solver)
        {
            return Result.Ok(solver.Solve().ToArray());
        }

        /// <summary>
        /// Gets a single solution
        /// </summary>
        /// <param name="solver">ISolver</param>
        /// <param name="choice"></param>
        /// <returns>Result</returns>
        public static Result<object> Solution(this ISolver solver, Solution choice)
        {
            IEnumerable<object> solutions = solver.Solve();
            return Result.Ok(choice == Interfaces.Solution.One ? solutions.First() : solutions.Last());
        }

        /// <summary>
        /// Gets Problem info via custom Problem attribute
        /// </summary>
        /// <param name="solver">ISolver</param>
        /// <returns>Problem</returns>
        public static Problem GetProblemInfo(this ISolver solver)
        {
            return (Problem)System.Attribute.GetCustomAttributes(solver.GetType()).First();
        }
    }
}
