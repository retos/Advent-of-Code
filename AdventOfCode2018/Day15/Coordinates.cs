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
        public bool IsTarget { get; internal set; }

        internal int Distance(Coordinates targetCoordniates)
        {
            return Math.Abs(targetCoordniates.X - X) + Math.Abs(targetCoordniates.Y - Y);
        }
        public override bool Equals(object obj)
        {
            Coordinates other = (Coordinates)obj;
            return (other == null)? false : other.X.Equals(X) && other.Y.Equals(Y);
        }
    }
}