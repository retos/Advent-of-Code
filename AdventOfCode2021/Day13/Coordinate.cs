namespace AdventOfCode2021.Day13
{
    internal class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; internal set; }
        public int X { get; internal set; }
    }
}