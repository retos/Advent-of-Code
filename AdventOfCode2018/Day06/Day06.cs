using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day06
{
    internal class Day06 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            foreach (string s in input)
            {
                string[] split = s.Split(',');
                coordinates.Add(new Coordinates(int.Parse(split[0]), int.Parse(split[1])));
            }

            int maxX = coordinates.Max(c => c.X);
            int maxY = coordinates.Max(c => c.Y);

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    List<Coordinates> closest = coordinates.OrderBy(c => c.ManhattanDistance(x, y)).Take(2).ToList();
                    if (closest[0].ManhattanDistance(x, y).Equals(closest[1].ManhattanDistance(x, y)))
                    {
                        //mindestens zwei gefunden, keiner bekommt es
                    }
                    else
                    {
                        //nur einer
                        closest[0].ClosestTo.Add(new Coordinates(x,y));
                    }
                }
            }

            List<Coordinates> winners = coordinates
                .Where(c => !c.ClosestTo.Any(co => co.X==0 || co.X==maxX || co.Y == 0 || co.Y == maxY
            ))
                .OrderByDescending(c => c.ClosestTo.Count).ToList();

            return winners.First().ClosestTo.Count.ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<Coordinates> coordinates = new List<Coordinates>();
            foreach (string s in input)
            {
                string[] splilt = s.Split(',');
                coordinates.Add(new Coordinates(int.Parse(splilt[0]), int.Parse(splilt[1])));
            }

            int maxX = coordinates.Max(c => c.X);
            int maxY = coordinates.Max(c => c.Y);

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    List<Coordinates> closest = coordinates.OrderBy(c => c.ManhattanDistance(x, y)).Take(5).ToList();
                    if (closest[0].ManhattanDistance(x, y).Equals(closest[1].ManhattanDistance(x, y)))
                    {
                        //zwei gefunden, keiner bekommt es
                    }
                    else
                    {
                        //nur einer
                        closest[0].ClosestTo.Add(new Coordinates(x, y));
                    }
                }
            }

            List<Coordinates> saveCoordinates = coordinates.Where(c => coordinates.Any(co => co.ClosestTo.Any( cl => cl.X.Equals(c.X) && cl.Y.Equals(c.Y)))).ToList();

            List<KeyValuePair<string, int>> distances = new List<KeyValuePair<string, int>>();

            int maxDistance = 10000;
            if (isTestRun)
            {
                maxDistance = 32;
            }

            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {

                    int totalDistances = saveCoordinates.Sum(c => c.Distance(x, y));
                    if (totalDistances < maxDistance)
                    {
                        distances.Add(new KeyValuePair<string, int>($"{x},{y}", totalDistances));
                    }
                }
            }

            return distances.Count.ToString();
        }
    }
}