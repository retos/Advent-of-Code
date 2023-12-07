
namespace AdventOfCode2023.Day03
{
    internal class Entry
    {
        public string Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int XEnd
        {
            get 
            {
                return this.X + Value.Length-1;
            }
        }

        public bool IsSymbol { get; internal set; }

        internal bool AreAdjacent(Entry other)
        {
            int yDiff = Math.Abs(Y - other.Y);
            if (yDiff > 1)
            {
                return false;
            }

            int smallestX = 2;
            for (int i = this.X; i <= this.XEnd; i++)
            {
                for (int j = other.X; j <= other.XEnd; j++)
                {
                    int xDiff = Math.Abs(i - j);
                    smallestX = (xDiff < smallestX) ? xDiff : smallestX;
                }
            }

            return smallestX < 2;

            throw new NotImplementedException();
        }
    }
}