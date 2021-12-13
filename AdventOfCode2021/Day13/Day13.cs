using System.Drawing;

namespace AdventOfCode2021.Day13;

internal class Day13 : DayBase
{
    public override string Title => "--- Day 13: Transparent Origami ---";
    public override bool Ignore => true;
    public List<Coordinate> Coordinates { get; set; }
    public List<Tuple<char, int>> FoldInstructions { get; set; }
    public int PaperHeight { get; set; }
    public int PaperWidth { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        ReaderInput(input);
        PaperHeight = Coordinates.Max(c => c.Y);//start at 0
        PaperWidth = Coordinates.Max(c => c.X);
        //if (isTestRun) PrintPaper();

        int nextHeight;
        int nextWidth;

        foreach (Tuple<char, int> fold in FoldInstructions)
        {
            if (fold.Item1 == 'y')
            {
                int upperHeight = PaperHeight - fold.Item2;
                int lowerHeight = PaperHeight - upperHeight;
                nextHeight = (upperHeight > lowerHeight) ? upperHeight : lowerHeight;
                nextHeight--;
                nextWidth = PaperWidth;

                //fold up
                foreach (Coordinate coord in Coordinates)
                {
                    if (coord.Y > fold.Item2)//below fold
                    {                  
                        coord.Y = fold.Item2 - (coord.Y - fold.Item2);//subtract difference to fold
                    }
                }
            }
            else //x
            {
                //fold left
                int leftWith = PaperWidth - fold.Item2;
                int rightWidht = PaperWidth - leftWith;
                nextHeight = PaperHeight;
                nextWidth = (leftWith > rightWidht)? leftWith : rightWidht ;
                nextWidth--;

                //fold left
                foreach (Coordinate coord in Coordinates)
                {
                    if (coord.X > fold.Item2)//right of the fold
                    {                      
                        coord.X = fold.Item2 - (coord.X - fold.Item2);//subtract difference to fold
                    }
                }
            }

            PaperHeight = nextHeight;
            PaperWidth = nextWidth;

            //if (isTestRun) PrintPaper();
            //remove duplicates
            Coordinates = Coordinates.GroupBy(c => $"{c.X}-{c.Y}").Select(x => x.First()).ToList();

            return $"{Coordinates.Count()}";
        }


        return $"nothing found!";
    }

    private void PrintPaper()
    {
        Console.Clear(); ;
        for (int x = 0; x <= PaperHeight; x++)
        {
            for (int y = 0; y <= PaperWidth; y++)
            {
                Console.SetCursorPosition(y, x);
                Console.Write('.');
            }
        }
        foreach (Coordinate coordinate in Coordinates)
        {
            Console.SetCursorPosition(coordinate.X, coordinate.Y);
            Console.Write('#');
        }
    }

    private void ReaderInput(List<string> input)
    {
        bool gapFound = false;
        List<Coordinate> coordinates = new();
        FoldInstructions = new();
        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                gapFound = true;
            }
            else if (gapFound)
            {
                string[] parts = line.Split(' ');
                char axis = parts[2].Split('=')[0][0];
                int foldOn = int.Parse(line.Split('=')[1]);
                FoldInstructions.Add(Tuple.Create(axis, foldOn));
            }
            else
            {
                string[] parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                coordinates.Add(new Coordinate(x, y));    
            }
        }
        Coordinates = coordinates;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        ReaderInput(input);
        PaperHeight = Coordinates.Max(c => c.Y);//start at 0
        PaperWidth = Coordinates.Max(c => c.X);
        //if (isTestRun) PrintPaper();

        int nextHeight;
        int nextWidth;

        foreach (Tuple<char, int> fold in FoldInstructions)
        {
            if (fold.Item1 == 'y')
            {
                int upperHeight = PaperHeight - fold.Item2;
                int lowerHeight = PaperHeight - upperHeight;
                nextHeight = (upperHeight > lowerHeight) ? upperHeight : lowerHeight;
                nextHeight--;
                nextWidth = PaperWidth;

                //fold up
                foreach (Coordinate coord in Coordinates)
                {
                    if (coord.Y > fold.Item2)//below fold
                    {
                        coord.Y = fold.Item2 - (coord.Y - fold.Item2);//subtract difference to fold                    
                    }
                }
            }
            else //x
            {
                //fold left
                int leftWith = PaperWidth - fold.Item2;
                int rightWidht = PaperWidth - leftWith;
                nextHeight = PaperHeight;
                nextWidth = (leftWith > rightWidht) ? leftWith : rightWidht;
                nextWidth--;

                //fold left
                foreach (Coordinate coord in Coordinates)
                {
                    if (coord.X > fold.Item2)//right of the fold
                    {
                        coord.X = fold.Item2 - (coord.X - fold.Item2);//subtract difference to fold             
                    }
                }
            }

            PaperHeight = nextHeight;
            PaperWidth = nextWidth;

            //if (isTestRun) PrintPaper();
            //remove duplicates
            Coordinates = Coordinates.GroupBy(c => $"{c.X}-{c.Y}").Select(x => x.First()).ToList();
        }

        Bitmap bitmap = new Bitmap(PaperWidth + 1, PaperHeight+ 1);
        foreach (Coordinate c in Coordinates)
        {
            bitmap.SetPixel(c.X, c.Y, Color.Black);
        }
        bitmap.Save($"AoC_2021_Day13_IsTestRun-{isTestRun}.png");

        return $"See generated picture in output folder";
    }
}