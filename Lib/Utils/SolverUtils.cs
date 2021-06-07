using Aoc.Configuration;
using Aoc.Lib.Extensions;
using Aoc.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aoc.Lib.Utils
{
    public class SolverUtils
    {
        private readonly SystemConfig systemConfig;
        private readonly Assembly[] assemblies;

        public virtual string SolutionsNamespace { get; set; } = "Aoc.Client.Solutions";

        private const string indataFileNameFormat = "day-{0}.in";

        public SolverUtils(SystemConfig systemConfig)
        {
            this.systemConfig = systemConfig;
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Directory.CreateDirectory(systemConfig.SolutionsBasePath); // base path should always exist
        }

        /// <summary>
        /// Get all Solvers
        /// </summary>
        /// <param name="filter">Filter, e.g. Day01</param>
        /// <returns>List of Types</returns>
        public List<Type> GetSolvers(string filter = "")
        {
            var query = assemblies.AsQueryable()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == SolutionsNamespace && t.Name.Contains("Day"));

            if (!string.IsNullOrEmpty(filter)) query = query.Where(t => t.Name == filter);

            return query.ToList();
        }

        public Result GenerateSolver(int day, string problemName)
        {
            var eval = EvaluateGenerationRequest(day);
            if (eval.IsFailure) return eval;

            var solverFolderUrl = GetSolverFolderFullUrl(day);
            var fullUrl = GetFullSolverUrl(day);

            Directory.CreateDirectory(solverFolderUrl);
            try
            {
                using StreamWriter fileWriter = File.CreateText(fullUrl);
                var templateBase = systemConfig.TemplateBase;
                foreach (var line in templateBase)
                    fileWriter.WriteLine(line.Replace("{0}", GetSolverName(day)).Replace("{1}", problemName).Replace("{2}", day.SolverNumberToPrint()));
                fileWriter.Close();

                string fullIndataUrl = Path.Combine(solverFolderUrl, string.Format(indataFileNameFormat, day.SolverNumberToPrint()));
                using StreamWriter indataWriter = File.CreateText(fullIndataUrl);
            }
            catch (Exception e)
            {
                return Result.Fail(string.Format("Caught error trying to generate template {0}: {1}", GetSolverName(day), e));
            }

            return Result.Ok(fullUrl);
        }

        private Result EvaluateGenerationRequest(int day)
        {
            if(!day.DayInRange()) return Result.Fail(string.Format("Valid Solutions are 1-24. Entered: {0}", day));
            if (SolverExists(day).IsSuccess) return Result.Fail(string.Format("Solution for day {0} already exists", day));
            return Result.Ok();
        }

        /// <summary>
        /// Get whether the Solver already exists
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Result</returns>
        public Result SolverExists(int day)
        {
            if (!day.DayInRange()) return Result.Fail("Day not in range!");
            return File.Exists(GetFullSolverUrl(day)) ? Result.Ok() : Result.Fail("Solution does not exist");
        }

        /// <summary>
        /// Get name of solver e.g. Day01
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Name of Solver</returns>
        public string GetSolverName(int day)
            => $"Day{day.SolverNumberToPrint()}";

        private string GetSolverFileName(int day)
        {
            return $"{GetSolverName(day)}.cs";
        }

        private string GetSolverFolder(int day)
        {
            return GetSolverName(day);
        }

        /// <summary>
        /// Get the url to Solver's folder
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Solver's folder url</returns>
        public string GetSolverFolderFullUrl(int day)
        {
            if (!day.DayInRange()) return string.Empty;
            return Path.Combine(systemConfig.SolutionsBasePath, GetSolverName(day));
        }

        /// <summary>
        /// Get the full url to Solver
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Full url to Solver</returns>
        public string GetFullSolverUrl(int day)
        {
            if (!day.DayInRange()) return string.Empty;
            return Path.Combine(systemConfig.SolutionsBasePath, GetSolverFolder(day), GetSolverFileName(day));
        }
    }
}
