namespace AdventOfCode2021.Day08;

internal class Day08 : DayBase
{
    public override string Title => "--- Day 8: Seven Segment Search ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Entry> entries = ReadInput(input);

        int count1 = entries.Sum(e => e.CountOutput(2));        //Number 1 needs two digits
        int count4 = entries.Sum(e => e.CountOutput(4));        //Number 4 needs two digits
        int count7 = entries.Sum(e => e.CountOutput(3));        //Number 7 needs two digits
        int count8 = entries.Sum(e => e.CountOutput(7));        //Number 8 needs two digits
        return $"{count1 + count4 + count7 + count8}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Entry> entries = ReadInput(input);
        int totalOutput = entries.Sum(e => e.CalculateOutput());
        return $"{totalOutput}";
    }
    private List<Entry> ReadInput(List<string> input)
    {
        List<Entry> entries = new List<Entry>();
        foreach (string line in input)
        {
            entries.Add(new Entry(line));
        }
        return entries;
    }
}