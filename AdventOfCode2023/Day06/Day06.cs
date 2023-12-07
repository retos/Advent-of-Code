using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2023.Day06;

internal class Day06 : DayBase
{
    public override string Title => "--- Day 6: Wait For It ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<long> times = input[0].Substring(11).Split(" ").Where(e => !string.IsNullOrEmpty(e)).Select(long.Parse).ToList();
        List<long> distances = input[1].Substring(11).Split(" ").Where(e => !string.IsNullOrEmpty(e)).Select(long.Parse).ToList();
        List<long> numberOfWays = new List<long>();

        for (int i = 0; i < times.Count; i++)
        {
            long time = times[i];
            long distance = distances[i];

            long n = CalculateNumberOfWays(time, distance);
            numberOfWays.Add(n);
        }
        long prod = 1;

        foreach (int i in numberOfWays)
        {
            prod = prod * i;
        }

        return $"{prod}";
    }

    private long CalculateNumberOfWays(long time, long distance)
    {
        long numberOfWays = 0;

        for (int i = 1; i < time; i++)
        {
            long speed = i;
            long remainingTime = time - i;
            long distanceTraveled = speed * remainingTime;
            if (distanceTraveled > distance)
            {
                numberOfWays++;
            }
        }
        return numberOfWays ;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        long time = long.Parse(input[0].Substring(11).Replace(" ", ""));
        long distance = long.Parse(input[1].Substring(11).Replace(" ", ""));

        long n = CalculateNumberOfWays(time, distance);

        return $"{n}";
    }
}
