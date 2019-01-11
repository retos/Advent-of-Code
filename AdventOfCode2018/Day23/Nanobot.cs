using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day23
{
    internal class Nanobot
    {
        public int Radius { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Nanobot(string line, ref System.Collections.Generic.List<Nanobot> bots)
        {
            this.Bots = bots;
            this.Radius = int.Parse(line.Split('=')[2]);
            int start = line.IndexOf('<');
            int end = line.IndexOf('>');
            string[] coordinates = line.Substring(start + 1, end - start - 1).Split(',');
            this.X = int.Parse(coordinates[0]);
            this.Y = int.Parse(coordinates[1]);
            this.Z = int.Parse(coordinates[2]);
        }

        public List<Nanobot> Bots { get; private set; }
        public List<Nanobot> BotsInRange
        {
            get
            {
                return Bots.Where(b => b.Distance(this) <= Radius).ToList();
            }
        }

        private int Distance(Nanobot otherBot)
        {
            return Distance(otherBot.X, otherBot.Y, otherBot.Z);
        }
        private int Distance(int x, int y, int z)
        {
            return Math.Abs(this.X - x) + Math.Abs(this.Y - y) + Math.Abs(this.Z - z);
        }
        internal bool IsInRange(int x, int y, int z)
        {
            return this.Distance(x, y, z) <= Radius;
        }
    }
}