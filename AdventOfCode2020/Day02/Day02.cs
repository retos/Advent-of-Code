using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day02
{
    internal class Day02 : DayBase
    {
        public override string Title => "--- Day 2: Password Philosophy ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<PasswordEntry> passwords = PasswordEntry.Convert(input);

            return $"Number of valid passwords: {passwords.Count(p => p.IsValid)}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<PasswordEntry> passwords = PasswordEntry.Convert(input);

            return $"Number of valid passwords: {passwords.Count(p => p.IsValidPart2)}";

        }
    }
}
