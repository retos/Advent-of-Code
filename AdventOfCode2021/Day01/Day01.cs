namespace AdventOfCode2021.Day01;

internal class Day01 : DayBase
{
    public override string Title => "--- Day 1: Sonar Sweep ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<int> numbers = input.Select(int.Parse).ToList();

        int increasecounter = 0;
        int lastnumber = numbers.First();

        foreach (int value in numbers)
        {
            if (value > lastnumber)
            {
                increasecounter++;
            }
            lastnumber = value;
        }

        return $"it has increased {increasecounter}";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<int> numbers = input.Select(int.Parse).ToList();

        List<int> slidingWindowA = new List<int>();
        List<int> slidingWindowB = new List<int>();

        int increasecounter = 0;

        for (int i = 0; i < numbers.Count; i++)
        {
            slidingWindowA = numbers.Skip(i).Take(3).ToList();
            slidingWindowB = numbers.Skip(i + 1).Take(3).ToList();

            if (slidingWindowB.Sum() > slidingWindowA.Sum())
            {
                increasecounter++;
            }
        }

        return $"it has increased {increasecounter}";

    }
}

