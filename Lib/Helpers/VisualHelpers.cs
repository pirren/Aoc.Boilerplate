using Aoc.Configuration;
using System;
using System.IO;

namespace Aoc.Lib.Helpers
{
    public static class VisualHelpers
    {
        public static void NewLine() => Print(Environment.NewLine);

        public static void Print(string text = "", ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintBlockStart() 
            => Print("--------------------------------------\n\n", ConsoleColor.Cyan);

        public static void PrintBlockEnd()
            => Print("\n--------------------------------------\n", ConsoleColor.Cyan);

        public static void PrintAsciiHeader(SystemConfig config, ConsoleColor color = ConsoleColor.Gray)
        {
            string[] ascii = File.ReadAllLines(config.AsciiUrl);
            foreach (var line in ascii)
                Print($"{line}\n", color);
        }
    }
}
