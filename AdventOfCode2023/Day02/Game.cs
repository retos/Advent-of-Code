namespace AdventOfCode2023.Day02
{
    internal class Game
    {
        public long Id { get; internal set; }
        public List<Reveal> Reveals { get; internal set; } = new List<Reveal>();
        public long minRed
        {
            get
            {
                return Reveals.Max(r => r.Red);
            }
        }
        public long minBlue
        {
            get
            {
                return Reveals.Max(r => r.Blue);
            }
        }
        public long minGreen
        {
            get
            {
                return Reveals.Max(r => r.Green);
            }
        }

        public long PowerOfGame
        {
            get
            {
                return minRed * minGreen * minBlue;

            }
        }
    }
}