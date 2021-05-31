using Aoc.Lib.Interfaces;
using System;
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
        public static object[] Solutions(this ISolver solver)
        {
            var solutions = solver.Solve().ToArray();
            if(solutions.Length == 0 || solutions == null)
                throw new SolutionException("Found no solutions for ISolver");
            return solutions;
        }

        /// <summary>
        /// Gets a single solution
        /// </summary>
        /// <param name="solver">ISolver</param>
        /// <param name="choice"></param>
        /// <returns>Result</returns>
        public static object Solution(this ISolver solver, SolutionNumber choice)
        {
            var solutions = solver.Solve().ToArray();
            if (solutions.Length == 0 || solutions == null)
                throw new SolutionException("Found no solutions for ISolver");
            if(choice == SolutionNumber.Two && solutions.Length < 2)
                throw new SolutionException("ISolver has sufficient solutions");
            return choice == SolutionNumber.One ? solutions.First() : solutions.Last();
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

        public class SolutionException : ApplicationException
        {
            public SolutionException(string message) : base(message) { }
        }
    }
}
