namespace AdventOfCode2021.Day04;

internal class Day04 : DayBase
{
    public override string Title => "--- Day 4: Giant Squid ---";
    public override bool Ignore => false;

    public override string Part1(List<string> input, bool isTestRun)
    {
        int[] numbers = Array.ConvertAll(input[0].Split(','), s => int.Parse(s));
        List<Board> boards = new List<Board>();

        //create boards
        for (int i = 1; i < input.Count; i++)
        {
            if (!string.IsNullOrEmpty(input[i]))
            {
                Board board = new Board(
                    Array.ConvertAll(input[i].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 1].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 2].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 3].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 4].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s))
                    );
                i = i + 4;
                boards.Add(board);
            }
        }

        foreach (int number in numbers)
        {
            boards.ForEach(c => c.Check(number));
            List<Board> winner = boards.Where(c => c.IsWinner).ToList();
            if (winner.Count() > 0)
            {
                return $" {winner.First().Score()}";
            }
        }

        return $"nothing";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        int[] numbers = Array.ConvertAll(input[0].Split(','), s => int.Parse(s));
        List<Board> boards = new List<Board>();

        for (int i = 1; i < input.Count; i++)
        {
            if (!string.IsNullOrEmpty(input[i]))
            {
                Board board = new Board(
                    Array.ConvertAll(input[i].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 1].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 2].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 3].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s)),
                    Array.ConvertAll(input[i + 4].Split(' ').Where(p => !string.IsNullOrEmpty(p)).ToArray(), s => int.Parse(s))
                    );
                i = i + 4;
                boards.Add(board);
            }
        }

        List<int> winningNumbers = new List<int>();
        Board winninBoard = null;

        foreach (int number in numbers)
        {
            boards.ForEach(c => c.Check(number));
            List<Board> winner = boards.Where(c => c.IsWinner).ToList();
            if (winner.Count() > 0 && winningNumbers.Count() < 1)
            {
                winninBoard = winner.First();
            }

            List<Board> remainingBoards = boards.Where(c => !c.IsWinner).ToList();

            if (remainingBoards.Count() < 1)
            {
                return $" {boards.First().Score()}";
            }
            boards = remainingBoards;
        }

        return $"nothing";
    }
}

