using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    internal interface IDay
    {
        string Title { get; }
        bool Ignore { get; }
        int SortOrder { get; }

        void Calculate();
        string Part1(List<string> input, bool isTestRun);
        string Part2(List<string> input, bool isTestRun);
    }
}
