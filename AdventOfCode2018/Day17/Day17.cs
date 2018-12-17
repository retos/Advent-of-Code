using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day17
{
    internal class Day17 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            MapManager manager = new MapManager(input, isTestRun);
            manager.PrintMap();
            manager.PourWater(500, 0);

            //Real Data Stopped at: 26640

            //31955 is too high
            return manager.WaterCount.ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}