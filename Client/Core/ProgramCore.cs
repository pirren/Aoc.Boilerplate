using Aoc.Configuration;
using Aoc.Lib.Extensions;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Serilog;
using System;
using System.Linq;
using System.Text;

namespace Aoc.Client.Core
{
    public class ProgramCore : IProgramCore
    {
        private readonly SystemConfig config;
        private readonly SolutionUtils solutionUtils;

        private const string InputOperationPrompt = "Enter operation: ";
        private const string InputProblemNamePrompt = "Enter problem name: ";
        private const string InputProblemDayPrompt = "Enter problem day: ";

        private const string CommandRunAll = "a";
        private const string CommandCreateTemplate = "c";
        private const string CommandListTemplates = "l";

        public int NumProblems { get; private set; }
        public bool Running { get; private set; }

        public ProgramCore(SystemConfig config, SolutionUtils solutionUtils)
        {
            this.config = config;
            this.solutionUtils = solutionUtils;
            NumProblems = solutionUtils.GetSolvers().Count;
        }

        /// <summary>
        /// Runs core, needs to be looped
        /// </summary>
        public void Run()
        {
            SystemUtils.NewBlock();

            UserInput userInput = new();
            var indataResult = userInput.GetString(InputOperationPrompt);

            if (indataResult.IsFailure) return;
            string input = indataResult.Value.ToLower();

            if (input == CommandRunAll) 
                RunAll();
            if (input == CommandCreateTemplate)
                RunCreateTemplate();
            if (input == CommandListTemplates)
                RunListTemplates();
            if (int.TryParse(input, out int choice))
                RunProblem(choice, true);
        }

        private void RunCreateTemplate()
        {
            UserInput userInput = new();

            SystemUtils.NewBlock();

            var dayResult = userInput.GetInt(InputProblemDayPrompt);
            if (dayResult.IsFailure) return;
            int problemDay = dayResult.Value;

            var problemResult = userInput.GetString(InputProblemNamePrompt);
            if (problemResult.IsFailure) return;
            string problemName = problemResult.Value;


            var result = solutionUtils.GenerateTemplate(problemDay, problemName);
            if (result.IsFailure)
            {
                SystemUtils.Print($"{result.Error}\n", ConsoleColor.Red);
                return;
            }
            Log.Information("Successfully generated template for day: {0}", problemDay.TemplateNumberToPrint());
        }

        private void RunListTemplates()
        {
            var solvers = solutionUtils
                .GetSolvers()
                .Select(s => (ISolver)Activator.CreateInstance(s))
                .ToList();

            SystemUtils.NewBlock();

            SystemUtils.Print($"Existing problem templates:\n");
            foreach (var solver in solvers)
                SystemUtils.Print($"{solver.GetProblemInfo().GetDay()}\n", ConsoleColor.Green);
        }

        private void RunProblem(int choice, bool singleOrLastIteration = false)
        {
            if (solutionUtils.TemplateExists(choice).IsFailure)
            {
                SystemUtils.Print($"Problem number {choice} does not exist!\n", ConsoleColor.Red);
                return;
            }

            SystemUtils.NewBlock();

            Type type = solutionUtils.GetSolvers(solutionUtils.GetTemplateShortName(choice)).First();
            if (type == null) return;

            ISolver solver = (ISolver)Activator.CreateInstance(type);
            var problemInfo = solver.GetProblemInfo();

            SystemUtils.Print("--------------------------------------\n\n", ConsoleColor.Cyan);

            SystemUtils.Print($"Problem: {problemInfo.GetDay()}\n");
            SystemUtils.Print($"Name: {problemInfo.GetName()}\n\n");

            int solutionNumber = 0;
            foreach (var result in solver.Solutions())
            {
                solutionNumber++;
                SystemUtils.Print($"{solutionNumber.ProblemPartToString()}");
                SystemUtils.Print($"{result}\n",ConsoleColor.Green);
            }

            if(singleOrLastIteration)
                SystemUtils.Print("\n--------------------------------------\n", ConsoleColor.Cyan);
        }

        private void RunAll()
        {
            var problemCount = solutionUtils.GetSolvers().Count;
            for(int i = 1; i <= problemCount; i++)
            {
                RunProblem(i, i == problemCount);
            }
        }
    }
}
