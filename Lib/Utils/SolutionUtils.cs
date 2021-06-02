using Aoc.Lib.Config;
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
        private readonly SystemConfig systemConfig;
        private readonly TemplateConfig templateConfig;
        private readonly Assembly[] assemblies;
        private readonly string solutionsNamespace = "Aoc.Client.Solutions";

        public SolutionUtils(SystemConfig systemConfig, TemplateConfig templateConfig)
        {
            this.systemConfig = systemConfig;
            this.templateConfig = templateConfig;
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
                .Where(t => t.IsClass && t.Namespace == solutionsNamespace);

            if (!string.IsNullOrEmpty(filter)) query = query.Where(t => t.Name == filter);
                
            return query.ToList();
        }

        public Result GenerateTemplate(int day, string problemName)
        {
            var eval = EvaluateGenerationRequest(day);
            if (eval.IsFailure) return eval;

            var solutionFolderUrl = GetTemplateFolderUrl(day);
            var fullUrl = GetTemplateUrl(day);

            Directory.CreateDirectory(solutionFolderUrl);

            using StreamWriter sw = File.CreateText(fullUrl);

            var templateBase = templateConfig.TemplateBase;

            foreach (var line in templateBase)
                sw.WriteLine(line.Replace("{0}", GetTemplateShortName(day)).Replace("{1}", problemName).Replace("{2}", day.TemplateNumberToPrint()));

            sw.Close();

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
            return Path.Combine(systemConfig.SolutionsBasePath, GetTemplateShortName(day));
        }

        public string GetTemplateUrl(int day)
        {
            return Path.Combine(systemConfig.SolutionsBasePath, GetSolutionTemplateFolderName(day), GetTemplateFileName(day));
        }
    }
}
