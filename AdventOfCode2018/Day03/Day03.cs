using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day03
{
    internal class Day03 : DayBase
    {
        public override string Title => "--- Day 3: No Matter How You Slice It ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input)
        {
            List<Claim> claims = new List<Claim>();

            foreach (string claim in input)
            {
                claims.Add(Convert(claim));
            }

            int maxX = claims.OrderBy(c => c.XMax).Last().XMax;
            int maxY = claims.OrderBy(c => c.YMax).Last().YMax;
            int countMulti = 0;

            string[,] map = new string[maxX, maxY];
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (claims.Count(c => c.Occupies(x, y)) > 1)
                    {
                        countMulti++;
                    }
                }
            }

            return $"Overlapping tile count: {countMulti.ToString()}" ;
        }

        private Claim Convert(string claim)
        {
            Claim c = new Claim();
            string[] splitted = claim.Split(' ');
            c.Id = splitted[0];
            c.X = int.Parse( splitted[2].Split(',')[0]);
            c.Y = int.Parse(splitted[2].Split(',')[1].Split(':')[0]);
            c.Width = int.Parse(splitted[3].Split('x')[0] );
            c.Height = int.Parse(splitted[3].Split('x')[1] );
            return c;
        }

        public override string Part2(List<string> input)
        {
            List<Claim> claims = new List<Claim>();

            foreach (string claim in input)
            {
                claims.Add(Convert(claim));
            }

            foreach (Claim claim in claims)
            {
                claim.CheckOverlap(claims);
            }

            return $"Claim with no overlap: {claims.Where(c => !c.Overlapping).First().Id}";
        }
    }
}