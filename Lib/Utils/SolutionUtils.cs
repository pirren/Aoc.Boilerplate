using Aoc2020.Lib.Communication;
using System.IO;
using System.Text;

namespace Aoc2020.Lib.Utils
{
    public class SolutionUtils
    {
        private const string solutionBasePath = @"..\..\..\Solutions";

        public SolutionUtils()
        {
            Directory.CreateDirectory(solutionBasePath); // base path should always exist
        }

        public Result BuildSolution(int day)
        {
            if (SolutionExists(day).IsSuccess) return Result.Fail("Solution already exists");

            var solutionFolderUrl = GetSolutionFolderUrl(day);
            var solutionFileName = GetSolutionFileName(day);
            var fullUrl = GetSolutionUrl(day);

            Directory.CreateDirectory(solutionFolderUrl);

            using StreamWriter sw = File.CreateText(fullUrl);

            //todo: Add filecontent here

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
            return Path.Combine(solutionBasePath, GetSolutionShortName(day));
        }

        private string GetSolutionUrl(int day)
        {
            return Path.Combine(solutionBasePath, GetSolutionFolderName(day), GetSolutionFileName(day));
        }

        private static string SolutionPrintableNumbers(int day) 
            => day > 9 ? day.ToString() : new StringBuilder().Append('0').Append(day).ToString();
    }
}
