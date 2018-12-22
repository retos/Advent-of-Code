using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day20
{ 
    internal class Day20 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Directions> directions = Directions.ReadDirections(input[0], null);

            
            Map map = new Map(directions[0]);
            map.PrintFloorplan();
            int mostDoors = map.GetFurthestRoom();
            ////70 was wrong
            return mostDoors.ToString();
            //return "not ready";
        }


        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}