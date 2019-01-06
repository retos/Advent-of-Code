using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

namespace AdventOfCode2018.Day20
{ 
    internal class Day20 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            return "work in progress please ignore...";
            // https://www.reddit.com/r/adventofcode/comments/a7uk3f/2018_day_20_solutions/

            foreach (string line in input)
            {                
                Hashtable map = new Hashtable();

                List<Directions> followupDirections = new List<Directions>();
                List<Directions> directions = Directions.ReadDirections(line.Remove(line.Length-1).Remove(0,1), ref followupDirections);

                directions[0].WriteToMap(ref map, 0, 0);
                Console.WriteLine($"input {line}, result");
            }

            //wrong 4346
            //string path = directions[0].GetLongestPath();

            //3966 js lösung richtig?
            return "";// path.Length.ToString();                        
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}