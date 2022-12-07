namespace AdventOfCode2022.Day06;

internal class Day06 : DayBase
{
    public override string Title => "--- Day 6: Tuning Trouble ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        int distinct = 4;
        string signal = input[0];
        //start-of-packet marker: four characters that are all different
        for (int i = distinct-1; i < signal.Length; i++)
        {
            if (signal.Substring(i - (distinct-1), distinct).ToCharArray().Distinct().ToArray().Length >= distinct)
            {
                return (i+1).ToString();
            }
        }

        return "not found";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        int distinct = 14;
        string signal = input[0];

        for (int i = distinct - 1; i < signal.Length; i++)
        {
            if (signal.Substring(i - (distinct - 1), distinct).ToCharArray().Distinct().ToArray().Length >= distinct)
            {
                return (i + 1).ToString();
            }
        }

        return "not found";
    }
}

