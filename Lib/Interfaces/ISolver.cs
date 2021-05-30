using System.Collections.Generic;

namespace Aoc.Lib.Interfaces
{
    public interface ISolver
    {
        IEnumerable<object> Solve();
        string Indata { get; }
        string ProblemName { get; }
        string Day { get; }
    }
}
