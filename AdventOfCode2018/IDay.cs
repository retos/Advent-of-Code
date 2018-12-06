using System.Collections.Generic;

namespace AdventOfCode2018
{
    internal interface IDay
    {
        string Title { get; }
        bool Ignore { get; }
        void Calculate();
        string Part1(List<string> input, bool isTestRun);
        string Part2(List<string> input, bool isTestRun);
    }
}