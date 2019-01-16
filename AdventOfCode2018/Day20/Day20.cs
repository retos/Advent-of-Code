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
        public override string Title => "--- Day 20: A Regular Map ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            string answer = string.Empty;
            foreach (string line in input)
            {                
                Hashtable map = new Hashtable();

                List<Directions> followupDirections = new List<Directions>();
                string[] reg = line.Split(' ');
                List<Directions> directions = Directions.ReadDirections(reg[0].Remove(reg[0].Length-1).Remove(0,1), ref followupDirections);

                directions[0].WriteToMap(ref map, 0, 0);
                List<Room> roomList = map.Values.Cast<Room>().ToList();
                //TODO fix this and than the Dijkstra calculation can be removed for part 1 and part 2
                //This gets the wrong result since my WriteToMap does depth first when drawing the map. Therefore if a shorter Path gets found. 
                //Room roomWithLongestShortPath = roomList.OrderByDescending(r => r.RoutesToRoom.OrderBy(p => p.Length).First().Length).First();
                int shortestPath = ((Room)map["0,0"]).Dijikstra(roomList);
                //Console.WriteLine($"input {reg[0]}, result {shortestPath}, expected {reg[1]}");

                answer += shortestPath + " ";
            }
            return answer;                      
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            Hashtable map = new Hashtable();

            List<Directions> followupDirections = new List<Directions>();
            string[] reg = input[0].Split(' ');
            List<Directions> directions = Directions.ReadDirections(reg[0].Remove(reg[0].Length - 1).Remove(0, 1), ref followupDirections);

            directions[0].WriteToMap(ref map, 0, 0);
            List<Room> roomList = map.Values.Cast<Room>().ToList();

            ((Room)map["0,0"]).Dijikstra(roomList);

            return roomList.Where(r => r.ShortestPath >= 1000).Count().ToString();                
        }
    }
}