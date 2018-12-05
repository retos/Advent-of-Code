using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day03
{
    internal class Claim
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int Height { get; internal set; }
        public int Width { get; internal set; }
        public string Id { get; set; }
        public bool Overlapping { get; internal set; }
        public int XMax
        { get
            {
                return X + Width - 1;
            }
        }
        public int YMax
        {
            get
            {
                return Y + Height - 1;
            }
        }

        internal bool Occupies(int x, int y)
        {
            if (X <= x && x <= XMax && Y <= y && y <= YMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void CheckOverlap(List<Claim> claims)
        {
            Overlapping = false;
            for (int x = X; x < X+Width; x++)
            {
                for (int y = Y; y < Y+Height; y++)
                {
                    if (claims.Count(c => c.Occupies(x,y)) > 1)
                    {
                        Overlapping = true;
                        return;
                    }
                }
            }
        }
    }
}