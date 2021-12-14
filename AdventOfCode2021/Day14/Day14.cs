using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day14;

internal class Day14 : DayBase
{
    public override string Title => "--- Day 14 ---";
    public override bool Ignore => false;

    public override string Part1(List<string> input, bool isTestRun)
    {
        string template = input[0];
        List<PairInsertion> pairInsertions = ReadPairs(input);
        

        for (int steps = 1; steps <= 10; steps++)
        {
            List<KeyValuePair<PairInsertion, int>> matches = new();
            foreach (PairInsertion insertion in pairInsertions)
            {
                List<int> indexes = AllIndexesOf(template, insertion.Elements);
                foreach (int m in indexes)
                {
                    matches.Add(new KeyValuePair<PairInsertion, int>(insertion, m));
                }
            }

            foreach (var m in matches.OrderByDescending(x => x.Value))
            {
                template = $"{template.Substring(0, m.Value + 1)}{m.Key.InsertElement}{template.Substring(m.Value + 1)}";
            }
        }

        var groupedStuff = template.GroupBy(t => t).Select(g => new { Letter = g.Key, Count = g.Count() }).OrderBy(x => x.Count).ToList();
        var min = groupedStuff.First();
        var max = groupedStuff.Last();


        return $"{max.Count-min.Count}";
    }

    public List<int> AllIndexesOf(string str, string value)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentException("the string to find may not be empty", "value");
        List<int> indexes = new List<int>();
        for (int index = 0; ; index += value.Length-1)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
                return indexes;
            indexes.Add(index);
        }
    }

    public List<PairInsertion> ReadPairs(List<string> input)
    {
        List<PairInsertion> pairs = new List<PairInsertion>();

        foreach (string pair in input.Skip(2))
        {
            pairs.Add(new PairInsertion() { Elements = pair.Substring(0, 2), InsertElement = pair.Substring(6, 1) });
        }
        return pairs;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {        
        string template = input[0];

        Dictionary<string, (string PolyA, string PolyB, char Insertion)> transformations = new();
        Dictionary<string, long> pairCounts = new Dictionary<string, long>();
        Dictionary<char, long> singleCounts = new Dictionary<char, long>();

        for (int i = 0; i < template.Length; i++)
        {
            singleCounts[template[i]] = singleCounts.GetValueOrDefault(template[i], 0) + 1;
            if (i > template.Length - 2) break;
            var pair = template.Substring(i, 2);
            pairCounts[pair] = pairCounts.GetValueOrDefault(pair, 0) + 1;
        }

        for (long i = 2; i < input.Count; i++)
        {
            var split = input[(int)i].Split("->", StringSplitOptions.TrimEntries);
            transformations.Add(split[0], (split[0][0] + split[1], split[1] + split[0][1], split[1][0]));
            pairCounts[split[0]] = pairCounts.GetValueOrDefault(split[0], 0);
        }

        for (long i = 0; i < 40; i++)
        {
            var nextPairCounts = new Dictionary<string, long>(pairCounts);
            foreach (var t in transformations)
            {
                nextPairCounts[t.Key] = nextPairCounts[t.Key] - pairCounts[t.Key];
                nextPairCounts[t.Value.PolyA] = nextPairCounts[t.Value.PolyA] + pairCounts[t.Key];
                nextPairCounts[t.Value.PolyB] = nextPairCounts[t.Value.PolyB] + pairCounts[t.Key];
                singleCounts[t.Value.Insertion] = singleCounts.GetValueOrDefault(t.Value.Insertion, 0) + pairCounts[t.Key];
            }
            pairCounts = nextPairCounts;
        }
              
        return $"{singleCounts.Max(kv => kv.Value) - singleCounts.Min(kv => kv.Value)}";
        
    }
}