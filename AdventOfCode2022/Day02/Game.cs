namespace AdventOfCode2022.Day02
{
    internal class Game
    {
        public Game()
        {
        }

        public string Left { get; internal set; }
        public string Right { get; internal set; }
        public long Score 
        { 
            get 
            {
                /*
                A Rock
                B Paper
                C Scissors

                X Rock
                Y Paper
                Z Scissors
                 */
                switch (Left)
                {
                    case "A": //Rock
                        if (Right == "X")
                        {//draw
                            return 1 + 3;
                        }
                        if (Right == "Y")
                        {//Right win
                            return 2 + 6;
                        }
                        if (Right == "Z")
                        {//Left win
                            return 3 + 0;
                        }
                        break;
                    case "B"://Paper
                        if (Right == "X")
                        {//righ lose
                            return 1 + 0;
                        }
                        if (Right == "Y")
                        {//draw
                            return 2 + 3;
                        }
                        if (Right == "Z")
                        {//right win
                            return 3 + 6;
                        }
                        break;
                    case "C"://Scissors
                        if (Right == "X")
                        {//righ win
                            return 1 + 6;
                        }
                        if (Right == "Y")
                        {//right lose
                            return 2 + 0;
                        }
                        if (Right == "Z")
                        {//draw
                            return 3 + 3;
                        }
                        break;

                }
                throw new Exception("Bad argument");
            } 
        }
        public long Score2
        {
            get
            {
                /*
                A Rock
                B Paper
                C Scissors

                X needs to loose
                Y draw needed
                Z needs to win
                 */
                switch (Left)
                {
                    case "A": //Rock
                        if (Right == "X")
                        {
                            return 3 + 0;
                        }
                        if (Right == "Y")
                        {
                            return 1 + 3;
                        }
                        if (Right == "Z")
                        {
                            return 2 + 6;
                        }
                        break;
                    case "B"://Paper
                        if (Right == "X")
                        {
                            return 1 + 0;
                        }
                        if (Right == "Y")
                        {
                            return 2 + 3;
                        }
                        if (Right == "Z")
                        {
                            return 3 + 6;
                        }
                        break;
                    case "C"://Scissors
                        if (Right == "X")
                        {
                            return 2 + 0;
                        }
                        if (Right == "Y")
                        {
                            return 3 + 3;
                        }
                        if (Right == "Z")
                        {
                            return 1 + 6;
                        }
                        break;
                }
                throw new Exception("Bad argument");
            }
        }
    }
}