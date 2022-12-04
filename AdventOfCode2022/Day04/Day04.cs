namespace AdventOfCode2022.Day04;

internal class Day04 : DayBase
{
    public override string Title => "--- Day 4: Camp Cleanup ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Elv> elves= new List<Elv>();
        foreach (string line in input)
        {
            elves.Add(new Elv(line));
        }



        return elves.Count(e => e.OneContainsTheOther).ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Elv> elves = new List<Elv>();
        foreach (string line in input)
        {
            elves.Add(new Elv(line));
        }



        return elves.Count(e => e.AnyOverlap).ToString();
    }
}

