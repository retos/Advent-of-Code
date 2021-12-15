namespace AdventOfCode2021.Day15
{
    internal class Coordinate
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public long Cost { get; set; }
        public Coordinate PreviousCoordinate { get; set; }
        public Coordinate(int value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
            Visited = false;
            Cost = long.MaxValue;
            PreviousCoordinate = null;
        }
    }
}