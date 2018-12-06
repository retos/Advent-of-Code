using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day06
{
    internal class Coordinates
    {

        public Coordinates(int v1, int v2)
        {
            X = v1;
            Y = v2;
            ClosestTo = new List<Coordinates>();
        }

        public int Y { get; internal set; }
        public int X { get; internal set; }
        public List<Coordinates> ClosestTo { get; internal set; }
        public bool Infinite { get; internal set; }

        internal int ManhattanDistance(int x, int y)
        {
            int diffX = X - x;
            int diffY = Y - y;
            return Math.Abs(diffX) + Math.Abs(diffY);
        }

        internal int Distance(int x, int y)
        {
            return Math.Abs(x-X) + Math.Abs(y-Y);
        }
    }
}