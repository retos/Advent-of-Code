namespace AdventOfCode2022.Day02
{
    internal class Game
    {
        public Game()
        {
        }

        public string Left { get; internal set; }
        public string Oponent 
        { 
            get 
            { 
                return LetterToShape(Left);
            }
        }

        public string Right { get; internal set; }
        public string Me
        {
            get
            {
                return LetterToShape(Right);
            }
        }
        public long Score 
        { 
            get 
            {
                return (Oponent, Me) switch
                {
                    ("Rock", "Rock") => 1 + 3,
                    ("Rock", "Paper") => 2 + 6,
                    ("Rock", "Scissors") => 3 + 0,

                    ("Paper", "Rock") => 1 + 0,
                    ("Paper", "Paper") => 2 + 3,
                    ("Paper", "Scissors") => 3 + 6,

                    ("Scissors", "Rock") => 1 + 6,
                    ("Scissors", "Paper") => 2 + 0,
                    ("Scissors", "Scissors") => 3 + 3,
                    _ => throw new Exception("Bad arguments")
                };
            } 
        }
        public long Score2
        {
            get
            {
                return (Oponent, Me) switch
                {
                    ("Rock", "Rock") => 3 + 0,
                    ("Rock", "Paper") => 1 + 3,
                    ("Rock", "Scissors") => 2 + 6,

                    ("Paper", "Rock") => 1 + 0,
                    ("Paper", "Paper") => 2 + 3,
                    ("Paper", "Scissors") => 3 + 6,

                    ("Scissors", "Rock") => 2 + 0,
                    ("Scissors", "Paper") => 3 + 3,
                    ("Scissors", "Scissors") => 1 + 6,
                    _ => throw new Exception("Bad arguments")
                };
            }
        }
        private string LetterToShape(string letter)
        {
            return letter switch
            {
                "A" => "Rock",
                "B" => "Paper",
                "C" => "Scissors",
                "X" => "Rock",
                "Y" => "Paper",
                "Z" => "Scissors",
                _ => throw new Exception("Invalid letter")
            };
        }
    }
}