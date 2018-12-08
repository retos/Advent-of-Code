using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day08
{
    internal class Day08 : DayBase
    {
        public List<int> Header { get; set; }
        public override string Title => "--- Day 8: Memory Maneuver ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            Header = input[0].Split(' ').Select(s => int.Parse(s)).ToList();
            int i = 0;
            Node n = new Node(Header, ref i);
            return n.SumOfMedatataEnties.ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            Header = input[0].Split(' ').Select(s => int.Parse(s)).ToList();
            int i = 0;
            Node n = new Node(Header, ref i);
            return n.Value.ToString();
        }
    }
}