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

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            MapManager manager = new MapManager(input, isTestRun);
            manager.PourWater(500, 0);
            return $"all water: {manager.WaterCount}, only still water: {manager.WaterCountStillWater}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return "see first part...";
        }
    }
}