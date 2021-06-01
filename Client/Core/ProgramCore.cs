using Aoc.Lib;
using Aoc.Lib.Extensions;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc.Client.Core
{
    public class ProgramCore : IProgramCore
    {
        private const string TextStartup = "Found {0} problems!\n";
        private const string TextHelp = "[number] to run single problem. [a] to run all. [ctrl+c] to quit.\n";
        private const string TextInput = "Enter operation: ";

        private readonly SystemConfig config;
        private readonly SolutionUtils solutionUtils;

        public int NumProblems { get; private set; }
        public bool Running { get; private set; }

        public ProgramCore(SystemConfig config, SolutionUtils solutionUtils)
        {
            this.config = config;
            this.solutionUtils = solutionUtils;
            NumProblems = solutionUtils.GetSolvers().Count;
        }

        /// <summary>
        /// Runts core, needs to be looped
        /// </summary>
        public void Run()
        {
            SystemUtils.Print(string.Format(TextStartup, NumProblems));
            SystemUtils.Print(TextHelp);
            SystemUtils.Print(TextInput);

            string input = Console.ReadLine();

            SystemUtils.Print("\n");

            if (input == null) 
                return;
            if (input.ToLower() == "a") 
                RunAll();
            if (int.TryParse(input, out int choice) && choice.IsValid(NumProblems))
                RunProblem(choice);
        }

        private void RunProblem(int choice)
        {
            if (solutionUtils.TemplateExists(choice).IsFailure) return;

            Type type = solutionUtils.GetSolvers(solutionUtils.GetTemplateShortName(choice)).First();
            if (type == null) return;

            ISolver solver = (ISolver)Activator.CreateInstance(type);
            var problemInfo = solver.GetProblemInfo();

            SystemUtils.Print(new StringBuilder().Append("Day: ").Append(problemInfo.GetDay()).Append('\n').ToString());
            SystemUtils.Print(new StringBuilder().Append("Name: ").Append(problemInfo.GetName()).Append('\n').ToString());

            int part = 1;
            foreach (var result in solver.Solutions())
            {
                SystemUtils.Print(new StringBuilder().Append(part.ProblemPartToString()).ToString());
                SystemUtils.Print(new StringBuilder().Append(result.ToString()).Append('\n').ToString(), 
                    ConsoleColor.Green);
                part++;
            }

            SystemUtils.Print("\n");
        }

        private void RunAll()
        {
            SystemUtils.Print("Runs all...\n");
        }
    }
}
