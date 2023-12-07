using System.Collections.Generic;

namespace AdventOfCode2023.Day05c;

internal class Day05c : DayBase
{
    public override string Title => "--- Day 5c: If You Give A Seed A Fertilizer (No Brute Force) ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        return "see first approach?";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        var seeds = input[0].Split(' ').Skip(1).Select(x => long.Parse(x)).ToList();
        var maps = new List<List<(long from, long to, long adjustment)>>();
        List<(long from, long to, long adjustment)>? currmap = null;
        foreach (var line in input.Skip(2))
        {
            if (line.EndsWith(':'))
            {
                currmap = new List<(long from, long to, long adjustment)>();
                continue;
            }
            else if (line.Length == 0 && currmap != null)
            {
                maps.Add(currmap!);
                currmap = null;
                continue;
            }

            var nums = line.Split(' ').Select(x => long.Parse(x)).ToArray();
            currmap!.Add((nums[1], nums[1] + nums[2] - 1, nums[0] - nums[1]));
        }
        if (currmap != null)
            maps.Add(currmap);

        var ranges = new List<(long from, long to)>();
        for (int i = 0; i < seeds.Count; i += 2)
            ranges.Add((from: seeds[i], to: seeds[i] + seeds[i + 1] - 1));

        foreach (var map in maps)
        {
            var orderedmap = map.OrderBy(x => x.from).ToList();

            var newranges = new List<(long from, long to)>();
            foreach (var r in ranges)
            {
                var range = r;
                foreach (var mapping in orderedmap)
                {
                    if (range.from < mapping.from) //since the mappins are ordered, if this is true no map covers FROM value --> reuse Id's
                    {
                        newranges.Add((range.from, Math.Min(range.to, mapping.from - 1)));
                        range.from = mapping.from;
                        if (range.from > range.to)
                            break;
                    }

                    if (range.from <= mapping.to) //match --> apply current mapping
                    {
                        newranges.Add((range.from + mapping.adjustment, Math.Min(range.to, mapping.to) + mapping.adjustment));
                        range.from = mapping.to + 1;//mark as solved
                        if (range.from > range.to)
                            break;
                    }
                }
                if (range.from <= range.to)
                    newranges.Add(range);
            }
            ranges = newranges;
        }
        var result2 = ranges.Min(r => r.from);


        return $"{result2}";
    }
}
