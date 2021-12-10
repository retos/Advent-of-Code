using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day25
{ 
    internal class Day25 : DayBase
    {
        public override string Title => "25";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<SpacetimePoints> points = ReadInput(input);

            return input.Count.ToString();

        }

        private List<SpacetimePoints> ReadInput(List<string> input)
        {
            List<SpacetimePoints> points = new List<SpacetimePoints>();
            foreach (string line in input)
            {
                points.Add(new SpacetimePoints(line));
            }
            return points;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}