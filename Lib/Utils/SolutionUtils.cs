using Aoc.Lib.Infrastructure;
using Aoc.Lib.Extensions;
using Microsoft.Extensions.Configuration;
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
        private readonly string solutionsNamespace = "Aoc.Client.Solutions";

        public SolutionUtils(SystemConfig configuration)
        {
            this.config = configuration;
            Directory.CreateDirectory(config.SolutionsBasePath); // base path should always exist
        }

        public List<Type> GetSolutions()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var foo = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == solutionsNamespace);

            return foo.ToList();
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

        private string GetTemplateShortName(int day)
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
