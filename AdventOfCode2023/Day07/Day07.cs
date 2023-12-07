using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode2023.Day07;

internal class Day07 : DayBase
{
    public override string Title => "--- Day 7: Camel Cards ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Hand> hands = new List<Hand>();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ');
            hands.Add(new Hand() { Cards = parts[0], Bid = long.Parse(parts[1]) });
        }

        hands = hands.Order().ToList();

        long totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Bid * (i + 1);
        }

        return $"{totalWinnings}";
    }



    public override string Part2(List<string> input, bool isTestRun)
    {
        List<HandPart2> hands = new List<HandPart2>();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ');
            hands.Add(new HandPart2() { Cards = parts[0], Bid = long.Parse(parts[1]) });
        }

        hands = hands.Order().ToList();

        long totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Bid * (i + 1);
        }

        return $"{totalWinnings}";
    }
}
