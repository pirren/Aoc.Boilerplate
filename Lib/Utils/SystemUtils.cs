using System;
using System.IO;
using System.Text;

namespace Aoc.Lib.Utils
{
    public static class SystemUtils
    {
        private const string asciiUrl = @"../../../ascii.txt";

        public static void Print(string text = "", ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintAsciiHeader(ConsoleColor color = ConsoleColor.Gray)
        {
            string[] ascii = File.ReadAllLines(asciiUrl);
            foreach (var line in ascii)
            {
                Print(new StringBuilder().Append(line).Append(Environment.NewLine).ToString(), color);
            }
        }
    }
}
