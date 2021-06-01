using Aoc.Lib.Extensions;
using Aoc.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aoc.Lib.Utils
{
    public class SolutionUtils
    {
        private readonly SystemConfig config;
        private readonly Assembly[] assemblies;
        private readonly string solutionsNamespace = "Aoc.Client.Solutions";

        public SolutionUtils(SystemConfig configuration)
        {
            this.config = configuration;
            assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Directory.CreateDirectory(config.SolutionsBasePath); // base path should always exist
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
                .Where(t => t.IsClass && t.Namespace == solutionsNamespace);

            if (!string.IsNullOrEmpty(filter)) query = query.Where(t => t.Name == filter);
                
            return query.ToList();
        }

        public Result GenerateTemplate(int day)
        {
            var eval = EvaluateGenerationRequest(day);
            if (eval.IsFailure) return eval;

            var solutionFolderUrl = GetTemplateFolderUrl(day);
            var fullUrl = GetTemplateUrl(day);

            Directory.CreateDirectory(solutionFolderUrl);

            using StreamWriter sw = File.CreateText(fullUrl);

            //todo: Add filecontent here

            return Result.Ok(fullUrl);
        }

        private Result EvaluateGenerationRequest(int day)
        {
            if (day < 1 || day > 24)
                return Result.Fail(string.Format("Valid Solutions are 1-24. Entered: {0}", day));
            if (TemplateExists(day).IsSuccess)
                return Result.Fail("Solution already exists");
            return Result.Ok();
        }

        public Result TemplateExists(int day)
        {
            return File.Exists(GetTemplateUrl(day)) ? Result.Ok() : Result.Fail("Solution does not exist");
        }

        public string GetTemplateShortName(int day)
        {
            return new StringBuilder().Append("Day").Append(day.TemplateNumberToPrint()).ToString();
        }

        private string GetTemplateFileName(int day)
        {
            return new StringBuilder().Append(GetTemplateShortName(day)).Append(".cs").ToString();
        }

        private string GetSolutionTemplateFolderName(int day)
        {
            return GetTemplateShortName(day);
        }

        private string GetTemplateFolderUrl(int day)
        {
            return Path.Combine(config.SolutionsBasePath, GetTemplateShortName(day));
        }

        public string GetTemplateUrl(int day)
        {
            return Path.Combine(config.SolutionsBasePath, GetSolutionTemplateFolderName(day), GetTemplateFileName(day));
        }
    }
}
