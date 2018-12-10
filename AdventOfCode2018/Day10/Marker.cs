using System;

namespace AdventOfCode2018.Day10
{
    internal class Marker
    {
        public int VelocityY { get; internal set; }
        public int VelocityX { get; internal set; }
        public int Y { get; internal set; }
        public int X { get; internal set; }

        internal Marker SetNextPosition(int time)
        {
            Marker copy = new Marker();
            copy.VelocityX = this.VelocityX;
            copy.VelocityY = this.VelocityY;
            copy.X = X + VelocityX * time;
            copy.Y = Y + VelocityY * time;
            return copy;
        }
    }
}