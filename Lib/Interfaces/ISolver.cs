using System;
using System.Collections.Generic;

namespace Aoc.Lib.Interfaces
{
    public interface ISolver
    {
        IEnumerable<object> Solve();
        string Data { get; }
    }

    public enum SolutionNumber
    {
        One,
        Two
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class Problem : Attribute
    {
        public Problem(string day, string name)
        {
            this.day = day;
            this.name = name;
        }

        public string GetName() => name;
        public string GetDay() => day;

        private readonly string name;
        private readonly string day;
    }
}
