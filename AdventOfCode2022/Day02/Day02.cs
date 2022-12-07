namespace AdventOfCode2022.Day02;

internal class Day02 : DayBase
{
    public override string Title => "--- Day 2: Rock Paper Scissors ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Game> games= new List<Game>();
        foreach (string line in input)
        {
            string[] splits =  line.Split(' ');
            Game g = new Game();
            
            g.Left = splits[0];
            g.Right = splits[1];
            games.Add(g);
        }

        return games.Sum(g => g.Score).ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Game> games = new List<Game>();
        foreach (string line in input)
        {
            string[] splits = line.Split(' ');
            Game g = new Game();

            g.Left = splits[0];
            g.Right = splits[1];
            games.Add(g);
        }

        return games.Sum(g => g.Score2).ToString();
    }
}

