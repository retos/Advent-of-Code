namespace AdventOfCode2021.Day03;

internal class Day03 : DayBase
{
    public override string Title => "--- Day 3: asdf! ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        string gamma = "";
        string epsilon = "";

        for (int i = 0; i < input[0].Length; i++)
        {
            int zeros = input.Count(l => l[i] == '0');
            int ones = input.Count(l => l[i] == '1');

            if (zeros > ones)
            {
                gamma += "0";
                epsilon += "1";
            }
            else
            {
                gamma += "1";
                epsilon += "0";
            }
        }

        return $"power consumption: {Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)}";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        //most common
        List<string> oxygenList = input;

        for (int i = 0; i < oxygenList[0].Length; i++)
        {
            int zeros = oxygenList.Count(l => l[i] == '0');
            int ones = oxygenList.Count(l => l[i] == '1');

            if (ones >= zeros)
            {
                oxygenList = oxygenList.Where(l => l[i] == '1').ToList();
            }
            else
            {
                oxygenList = oxygenList.Where(l => l[i] == '0').ToList();
            }
        }

        //least common
        List<string> scrubberList = input;

        for (int i = 0; i < scrubberList[0].Length; i++)
        {
            int zeros = scrubberList.Count(l => l[i] == '0');
            int ones = scrubberList.Count(l => l[i] == '1');

            if (zeros > ones)
            {
                scrubberList = scrubberList.Where(l => l[i] == '1').ToList();
            }
            else
            {
                scrubberList = scrubberList.Where(l => l[i] == '0').ToList();
            }
            if (scrubberList.Count <=1)
            {
                break;
            }
        }

        return $"life support rating (oxygen generator rating * CO2 scrubber rating): {Convert.ToInt32(oxygenList[0], 2) * Convert.ToInt32(scrubberList[0], 2)}";
    }
}

