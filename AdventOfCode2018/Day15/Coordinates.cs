using System;

namespace AdventOfCode2018.Day15
{
    internal class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// The cost is used in the 'Dijkstra Algorithm' to calculate the shortest path
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// Used in the 'Dijkstra Algorithm' to figure out if it has been visited
        /// </summary>
        public bool Visited { get; set; }
        internal int Distance(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y);
        }
    }
}