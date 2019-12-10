using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day23
{ 
    internal class Day23 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Nanobot> bots = new List<Nanobot>();
            foreach (string line in input)
            {
                bots.Add(new Nanobot(line, ref bots));
            }
            Nanobot botWithLargestRange = bots.OrderByDescending(b => b.Radius).First();
            return botWithLargestRange.BotsInRange.Count().ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return "not implemented";
        }
    }
}