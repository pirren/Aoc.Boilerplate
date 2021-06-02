using Aoc.Lib.Config;
using Aoc.Lib.Extensions;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using System;
using System.Linq;
using System.Text;

namespace Aoc.Client.Core
{
    public class ProgramCore : IProgramCore
    {
        private readonly SystemConfig config;
        private readonly SolutionUtils solutionUtils;

        private const string TextInput = "Enter operation: ";
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
            SystemUtils.Print(TextInput);

            string rawinput = Console.ReadLine();

            SystemUtils.Print("\n");

            if (rawinput == null) return;

            string input = rawinput.ToLower();
            if (input == CommandRunAll) 
                RunAll();
            if (input == CommandCreateTemplate)
                RunCreateTemplate();
            if (input == CommandListTemplates)
                RunListTemplates();
            if (int.TryParse(input, out int choice) && choice.IsValid(NumProblems))
                RunProblem(choice);
        }

        private void RunCreateTemplate()
        {
            SystemUtils.Print("\n");
            var result = solutionUtils.GenerateTemplate(3, "Problem 3");
        }

        private void RunListTemplates()
        {

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
