using Aoc.Lib.Communication;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace Aoc.Lib.Utils
{
    public class SolutionUtils
    {
        private readonly SystemConfig config;

        public SolutionUtils(SystemConfig configuration)
        {
            this.config = configuration;
            Directory.CreateDirectory(config.SolutionBasePath); // base path should always exist
        }

        public Result BuildSolution(int day)
        {
            var eval = EvaluateBuildRequest(day);
            if (eval.IsFailure) return eval;

            var solutionFolderUrl = GetSolutionFolderUrl(day);
            var fullUrl = GetSolutionUrl(day);

            Directory.CreateDirectory(solutionFolderUrl);

            using StreamWriter sw = File.CreateText(fullUrl);

            //todo: Add filecontent here

            return Result.Ok(fullUrl);
        }

        private Result EvaluateBuildRequest(int day)
        {
            if (day < 1 || day > 24) 
                return Result.Fail(string.Format("Valid Solutions are 1-24. Entered: {0}", day));
            if (SolutionExists(day).IsSuccess) 
                return Result.Fail("Solution already exists");
            return Result.Ok();
        }

        public Result SolutionExists(int day)
        {
            return File.Exists(GetSolutionUrl(day)) ? Result.Ok() : Result.Fail("Solution does not exist");
        }

        private string GetSolutionShortName(int day)
        {
            return new StringBuilder().Append("Day").Append(SolutionPrintableNumbers(day)).ToString();
        }

        private string GetSolutionFileName(int day)
        {
            return new StringBuilder().Append(GetSolutionShortName(day)).Append(".cs").ToString();
        }

        private string GetSolutionFolderName(int day)
        {
            return GetSolutionShortName(day);
        }

        private string GetSolutionFolderUrl(int day)
        {
            return Path.Combine(config.SolutionBasePath, GetSolutionShortName(day));
        }

        public string GetSolutionUrl(int day)
        {
            return Path.Combine(config.SolutionBasePath, GetSolutionFolderName(day), GetSolutionFileName(day));
        }

        private string SolutionPrintableNumbers(int day) 
            => day > 9 ? day.ToString() : new StringBuilder().Append('0').Append(day).ToString();
    }
}
