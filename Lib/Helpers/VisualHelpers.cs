using Aoc.Configuration;
using System;
using System.IO;

namespace Aoc.Lib.Helpers
{
    public static class VisualHelpers
    {
        /// <summary>
        /// Generic print method.
        /// </summary>
        /// <param name="text">Text to print</param>
        /// <param name="color">Console color to print</param>
        /// <param name="newLines">Number of newlines to add at the end. Default 0</param>
        public static void Print(string text = "", ConsoleColor color = ConsoleColor.Gray, int newLines = 0)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < newLines; i++)
                Console.Write(Environment.NewLine);
        }

        public static void PrintBlockStart() 
            => Print("--------------------------------------", color: ConsoleColor.Cyan, newLines: 2);

        public static void PrintBlockEnd()
            => Print("\n--------------------------------------", color: ConsoleColor.Cyan, newLines: 1);

        public static void PrintAsciiHeader(SystemConfig config, ConsoleColor color = ConsoleColor.Gray)
        {
            string[] ascii = File.ReadAllLines(config.AsciiUrl);
            foreach (var line in ascii)
                Print(line, color, 1);
        }
    }
}
