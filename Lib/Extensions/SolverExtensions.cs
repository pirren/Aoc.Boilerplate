using Aoc.Lib.Interfaces;
using System;
using System.Linq;

namespace Aoc.Lib.Extensions
{
    public static class SolverExtensions
    {
        /// <summary>
        /// Gets Problem info via custom Problem attribute
        /// </summary>
        /// <param name="solver">ISolver</param>
        /// <returns>Problem</returns>
        public static Problem GetProblemInfo(this ISolver solver)
            => (Problem)System.Attribute.GetCustomAttributes(solver.GetType()).First();
    }
}
