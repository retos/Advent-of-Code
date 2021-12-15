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
        public Coordinate[,] Map { get; set; }
        public bool PartOfThePath { get; internal set; }

        public Coordinate(int value, int x, int y, ref Coordinate[,] map)
        {
            Value = value;
            X = x;
            Y = y;
            Visited = false;
            Cost = long.MaxValue;
            Map = map;
            PreviousCoordinate = null;
            Map[x, y] = this;
        }

        internal List<Coordinate> GetUnvisitedNeighbors()
        {
            List<Coordinate> neighbors = new();
            //up
            if (Y - 1 > 0 && !Map[X, Y - 1].Visited)
            {
                neighbors.Add(Map[X, Y - 1]);
            }
            //right
            if (X + 1 < Map.GetLength(0) && !Map[X+1, Y].Visited)
            {
                neighbors.Add(Map[X+1, Y]);
            }
            //down
            if (Y + 1 < Map.GetLength(1) && !Map[X, Y + 1].Visited)
            {
                neighbors.Add(Map[X, Y + 1]);
            }
            //left
            if (X - 1 > 0 && !Map[X-1, Y].Visited)
            {
                neighbors.Add(Map[X-1, Y]);
            }


            return neighbors;
        }
    }
}