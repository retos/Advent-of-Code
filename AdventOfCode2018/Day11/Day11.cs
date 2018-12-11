using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day11
{
    internal class Day11 : DayBase
    {
        public override string Title => "--- Day 11: Chronal Charge ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int dimensions = 300;// (isTestRun)? 5 : 300 ;
            FuelGrid grid = new FuelGrid(int.Parse(input[0]), dimensions);
            int i = 0;
            return grid.GetCoordinatesOfLargestPowerSquare(3, ref i);
        }

        public override string Part2(List<string> input, bool isTestRun)
        {            
            int dimensions = 300;// (isTestRun)? 5 : 300 ;
            FuelGrid grid = new FuelGrid(int.Parse(input[0]), dimensions);

            return grid.GetCoordinatesOfLargestPowerSquareDynamic();
        }
    }
}