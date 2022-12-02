namespace AdventOfCode2022.Day01;

internal class Day01 : DayBase
{
    public override string Title => "--- Day 1: Calorie Counting ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Elv> elves = new List<Elv>();
        Elv elv = new Elv();
        elves.Add(elv);
        foreach (string line in input)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                elv.Load.Add(long.Parse(line));
            }
            else
            {
                elv = new();
                elves.Add(elv);
            }
        }

        return elves.OrderByDescending(e => e.TotalLoad).First().TotalLoad.ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Elv> elves = new List<Elv>();
        Elv elv = new Elv();
        elves.Add(elv);
        foreach (string line in input)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                elv.Load.Add(long.Parse(line));
            }
            else
            {
                elv = new();
                elves.Add(elv);
            }
        }

        return elves.OrderByDescending(e => e.TotalLoad).Take(3).Select(e => e.TotalLoad).Sum().ToString();
    }
}

