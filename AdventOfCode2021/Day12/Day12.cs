namespace AdventOfCode2021.Day12;

internal class Day12 : DayBase
{
    public override string Title => "--- Day 12 ---";
    public override bool Ignore => false;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Cave> caves = ReadInput(input);

        Cave start = caves.Where(c => c.Name == "start").Single();
        List<string> pathList = new();
        long count = start.Walk("", ref pathList, false);

        return $"{count}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Cave> caves = ReadInput(input);

        Cave start = caves.Where(c => c.Name == "start").Single();
        List<string> pathList = new();
        long count = start.Walk("", ref pathList, true);

        return $"{count}";
    }

    private List<Cave> ReadInput(List<string> input)
    {
        List<Cave> caves = new List<Cave>();
        foreach (string line in input)
        {
            string[] parts = line.Split('-');
            if (caves.Any(c => c.Name == parts[0]))
            {
                caves.Where( c => c.Name == parts[0]).Single().AddDestination(parts[1], ref caves);
            }
            else
            {
                caves.Add(new Cave(ref caves, parts[0], parts[1]) );
            }
        }
        return caves;
    }
}