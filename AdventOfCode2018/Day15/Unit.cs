using System;

namespace AdventOfCode2018.Day15
{
    internal class Unit
    {
        public char Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Hitpoints { get; set; }
        public int Attackpower { get; set; }
        public bool Dead { get; internal set; }

        public Unit(int x, int y, char t)
        {
            X = x;
            Y = y;
            Type= t;
            Hitpoints = 200;
            Attackpower = 3;
        }

        internal int Distance(Unit otherUnit)
        {
            return Math.Abs(otherUnit.X - X) + Math.Abs(otherUnit.Y - Y);
        }
    }
}