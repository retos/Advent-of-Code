using System.Collections.Generic;

namespace AdventOfCode2022.Day11;

internal class Day11 : DayBase
{
    public override string Title => "--- Day 11 ---";
    public override bool Ignore => false;
    public List<Monkey> Monkeys { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        InitMonkeys(input);
        int rounds = 20;

        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in Monkeys)
            {
                monkey.InspectWorryLevel();
            }
        }

        List<Monkey> mostActive = Monkeys.OrderByDescending(m => m.InspectedItems).Take(2).ToList();

        ulong monkeyBusiness = mostActive[0].InspectedItems * mostActive[1].InspectedItems;

        return monkeyBusiness.ToString();
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        InitMonkeys(input);
        int rounds = 10000;

        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in Monkeys)
            {
                monkey.InspectWorryLevel(false); ;
            }
        }

        List<Monkey> mostActive = Monkeys.OrderByDescending(m => m.InspectedItems).Take(2).ToList();

        ulong monkeyBusiness = mostActive[0].InspectedItems * mostActive[1].InspectedItems;

        return monkeyBusiness.ToString();
    }
    private void InitMonkeys(List<string> input)
    {
        Monkeys = new List<Monkey>();

        foreach (string line in input)
        {
            if (line.StartsWith("Monkey"))
            {
                ulong number = ulong.Parse(line.Split(" ")[1].Replace(":", ""));
                Monkeys.Add(new Monkey(number, Monkeys));
            }
            else if(line.StartsWith("  Starting items"))
            {
                List<ulong> items = line.Remove(0, 18).Split(", ").Select(ulong.Parse).ToList();
                Monkeys.Last().Items = items;
            }
            else if (line.StartsWith("  Operation: new = "))
            {             
                //part 1
                string operation = line.Replace("  Operation: new = ", "");
                Monkeys.Last().OperationString = operation;

                ////part 2
                if (operation.StartsWith("old +"))
                {//old + int
                    Monkeys.Last().Operation = old => old + (ulong)int.Parse(operation.Split(" ")[2]);
                }
                else if (operation.StartsWith("old * old"))
                {//old * old
                    Monkeys.Last().Operation = old => old * old;
                }
                else
                {//old * int
                    Monkeys.Last().Operation = old => old * (ulong)int.Parse(operation.Split(" ")[2]);
                }
            }
            else if(line.StartsWith("  Test: divisible by "))
            {
                string test = line.Replace("  Test: divisible by ", "");
                Monkeys.Last().Test = ulong.Parse(test);
            }
            else if (line.StartsWith("    If true: throw to monkey"))
            {
                string trueString = line.Replace("    If true: throw to monkey ", "");
                Monkeys.Last().TrueMonkey = ulong.Parse(trueString);
            }
            else if (line.StartsWith("    If false: throw to monkey "))
            {
                string falseString = line.Replace("    If false: throw to monkey ", "");
                Monkeys.Last().FalseMonkey = ulong.Parse(falseString);
            }
            //ignore empty line
        }
    }
}