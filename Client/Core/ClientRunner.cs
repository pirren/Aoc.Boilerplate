using Aoc.Lib.Extensions;
using Aoc.Lib.Helpers;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using Serilog;
using System;
using System.Linq;

namespace Aoc.Client.Core
{
    public class ClientRunner : ICore
    {
        private readonly SolutionUtils solutionUtils;

        private const string InputOperationPrompt = "Enter operation: ";
        private const string InputProblemNamePrompt = "Enter problem name: ";
        private const string InputProblemDayPrompt = "Enter problem day: ";

        private const string CommandRunAll = "a";
        private const string CommandCreateTemplate = "c";
        private const string CommandListTemplates = "l";

        public int NumProblems { get; private set; }
        public bool Running { get; private set; }

        public ClientRunner(SolutionUtils solutionUtils)
        {
            this.solutionUtils = solutionUtils;
            NumProblems = solutionUtils.GetSolvers().Count;
        }

        /// <summary>
        /// Runs core, needs to be looped
        /// </summary>
        public void Run()
        {
            VisualHelpers.Print(newLines: 1);

            UserInput userInput = new();
            var indataResult = userInput.GetString(InputOperationPrompt);

            if (indataResult.IsFailure) return;
            string input = indataResult.Value.ToLower();

            if (input == CommandRunAll)
                RunAllProblems();
            if (input == CommandCreateTemplate)
                RunCreateTemplate();
            if (input == CommandListTemplates)
                RunListTemplates();
            if (int.TryParse(input, out int choice))
                RunProblem(choice, singleOrLastIteration: true);
        }

        private void RunCreateTemplate()
        {
            UserInput userInput = new();

            VisualHelpers.Print(newLines: 1);

            var dayResult = userInput.GetInt(InputProblemDayPrompt);
            if (dayResult.IsFailure) return;
            int problemDay = dayResult.Value;

            var problemResult = userInput.GetString(InputProblemNamePrompt);
            if (problemResult.IsFailure) return;
            string problemName = problemResult.Value;


            var result = solutionUtils.GenerateTemplate(problemDay, problemName);
            if (result.IsFailure)
            {
                VisualHelpers.Print(result.Error, color: ConsoleColor.Red, newLines: 1);
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

            VisualHelpers.Print(newLines: 1);
            VisualHelpers.PrintBlockStart();

            VisualHelpers.Print($"Existing problem templates:", newLines: 1);
            foreach (var solver in solvers)
                VisualHelpers.Print(solver.GetProblemInfo().GetDay(), color: ConsoleColor.Green, newLines: 1);

            VisualHelpers.PrintBlockEnd();
        }

        private void RunProblem(int choice, bool singleOrLastIteration = false)
        {
            if (solutionUtils.TemplateExists(choice).IsFailure)
            {
                VisualHelpers.Print($"Problem number {choice} does not exist!", color: ConsoleColor.Red, newLines: 1);
                return;
            }

            VisualHelpers.Print(newLines: 1);

            Type type = solutionUtils.GetSolvers(solutionUtils.GetTemplateShortName(choice)).First();
            if (type == null) return;

            ISolver solver = (ISolver)Activator.CreateInstance(type);
            if (!solver.Solve().Any())
            {
                VisualHelpers.Print("No solutions available! Check the template.", color: ConsoleColor.Red, newLines: 1);
            }
            var problemInfo = solver.GetProblemInfo();

            VisualHelpers.PrintBlockStart();

            VisualHelpers.Print($"Problem: {problemInfo.GetDay()}", newLines: 1);
            VisualHelpers.Print($"Name: {problemInfo.GetName()}", newLines: 2);

            int solutionNumber = 0;
            foreach (var result in solver.Solve())
            {
                solutionNumber++;
                VisualHelpers.Print(solutionNumber.ProblemPartToString());
                VisualHelpers.Print(result.ToString(), color: ConsoleColor.Green, newLines: 1);
            }

            if (singleOrLastIteration)
                VisualHelpers.PrintBlockEnd();
        }

        private void RunAllProblems()
        {
            var problemCount = solutionUtils.GetSolvers().Count;
            for (int i = 1; i <= problemCount; i++) RunProblem(i, singleOrLastIteration: i == problemCount);
        }
    }
}
