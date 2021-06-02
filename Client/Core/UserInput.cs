﻿using Aoc.Lib.Infrastructure;
using Aoc.Lib.Interfaces;
using Aoc.Lib.Utils;
using System;

namespace Aoc.Client.Core
{
    public class UserInput : IUserInput
    {
        public Result<string> GetString(string prompt)
        {
            SystemUtils.Print(prompt);
            var result = Console.ReadLine();
            if (result == null) return Result.Fail<string>("Input was null");
            return Result.Ok(result);
        }

        public Result<int> GetInt(string prompt)
        {
            SystemUtils.Print(prompt);
            var indata = Console.ReadLine();
            if (!int.TryParse(indata, out int result)) return Result.Fail<int>("Input could not be parsed");
            return Result.Ok(result);
        }
    }
}
