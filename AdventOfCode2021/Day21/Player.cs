namespace AdventOfCode2021.Day21
{
    internal class Player
    {

        public string Name { get; set; }
        public int Position { get; set; }
        public long Score { get; set; }

        public Player(string name, int position, int score)
        {
            Name = name;
            Position = position;
            Score = score;
        }
    }
}