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
            //return "work in progress please ignore...";
            // https://www.reddit.com/r/adventofcode/comments/a7uk3f/2018_day_20_solutions/
            string answer = string.Empty;
            foreach (string line in input)
            {                
                Hashtable map = new Hashtable();

                List<Directions> followupDirections = new List<Directions>();
                string[] reg = line.Split(' ');
                List<Directions> directions = Directions.ReadDirections(reg[0].Remove(reg[0].Length-1).Remove(0,1), ref followupDirections);

                directions[0].WriteToMap(ref map, 0, 0);
                List<Room> roomList = map.Values.Cast<Room>().ToList();
                Room roomWithLongestShortPath = roomList.OrderByDescending(r => r.RoutesToRoom.OrderBy(p => p.Length).First().Length).First();
                List<Room> orderedList = roomList.OrderByDescending(r => r.RoutesToRoom.OrderBy(p => p.Length).First().Length).ToList();
                int shortestPath = ((Room)map["0,0"]).Dijikstra(roomWithLongestShortPath, roomList);

                Console.WriteLine($"input {reg[0]}, result {shortestPath}, expected {reg[1]}");
                answer += shortestPath + " ";
            }

            //wrong 4346
            //wrong 3964

            //3966 js lösung richtig?
            return answer;                      
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}