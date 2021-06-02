using Aoc.Lib.Infrastructure;

namespace Aoc.Lib.Interfaces
{
    public interface IUserInput
    {
        Result<string> GetString(string prompt);
        Result<int> GetInt(string prompt);
    }
}
