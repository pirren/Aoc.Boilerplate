﻿using Aoc.Configuration;
using Aoc.Lib.Extensions;
using Aoc.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aoc.Lib.Utils
{
    public class SolutionUtils
    {
        private readonly SystemConfig systemConfig;
        private readonly Assembly[] assemblies;

        public virtual string SolutionsNamespace { get; set; } = "Aoc.Client.Solutions";

        private const string indataFileNameFormat = "day-{0}.in";

        public SolutionUtils(SystemConfig systemConfig)
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

        public Result GenerateTemplate(int day, string problemName)
        {
            var eval = EvaluateGenerationRequest(day);
            if (eval.IsFailure) return eval;

            var templateFolderUrl = GetTemplateFolderUrl(day);
            var fullUrl = GetTemplateUrl(day);

            Directory.CreateDirectory(templateFolderUrl);
            try
            {
                using StreamWriter fileWriter = File.CreateText(fullUrl);
                var templateBase = systemConfig.TemplateBase;
                foreach (var line in templateBase)
                    fileWriter.WriteLine(line.Replace("{0}", GetTemplateShortName(day)).Replace("{1}", problemName).Replace("{2}", day.TemplateNumberToPrint()));
                fileWriter.Close();

                string fullIndataUrl = Path.Combine(templateFolderUrl, string.Format(indataFileNameFormat, day.TemplateNumberToPrint()));
                using StreamWriter indataWriter = File.CreateText(fullIndataUrl);
            }
            catch (Exception e)
            {
                return Result.Fail(string.Format("Caught error trying to generate template {0}: {1}", GetTemplateShortName(day), e));
            }

            return Result.Ok(fullUrl);
        }

        private Result EvaluateGenerationRequest(int day)
        {
            if(!day.DayInRange()) return Result.Fail(string.Format("Valid Solutions are 1-24. Entered: {0}", day));
            if (TemplateExists(day).IsSuccess) return Result.Fail(string.Format("Solution for day {0} already exists", day));
            return Result.Ok();
        }

        public Result TemplateExists(int day)
        {
            if (!day.DayInRange()) return Result.Fail("Day not in range!");
            return File.Exists(GetTemplateUrl(day)) ? Result.Ok() : Result.Fail("Solution does not exist");
        }

        public string GetTemplateShortName(int day)
        {
            return $"Day{day.TemplateNumberToPrint()}";
        }

        private string GetTemplateFileName(int day)
        {
            return $"{GetTemplateShortName(day)}.cs";
        }

        private string GetSolutionTemplateFolderName(int day)
        {
            return GetTemplateShortName(day);
        }

        public string GetTemplateFolderUrl(int day)
        {
            if (!day.DayInRange()) return string.Empty;
            return Path.Combine(systemConfig.SolutionsBasePath, GetTemplateShortName(day));
        }

        public string GetTemplateUrl(int day)
        {
            if (!day.DayInRange()) return string.Empty;
            return Path.Combine(systemConfig.SolutionsBasePath, GetSolutionTemplateFolderName(day), GetTemplateFileName(day));
        }
    }
}
