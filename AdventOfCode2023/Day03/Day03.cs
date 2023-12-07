using System.Collections.Generic;

namespace AdventOfCode2023.Day03;

internal class Day03 : DayBase
{
    public override string Title => "--- Day 3: Gear Ratios ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Entry> entries = new List<Entry>();
        string currentNumber = string.Empty;
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if(Char.IsNumber(input[y][x]))
                {
                    currentNumber += input[y][x];
                    if (x == input[y].Length-1 || !Char.IsNumber(input[y][x+1]))//end of line OR last digit
                    {
                        //save number
                        entries.Add(new Entry() { Value = currentNumber, X = x - (currentNumber.Length - 1), Y = y });
                        currentNumber = string.Empty;
                    }
                }
                else if (input[y][x] == '.')
                {
                    //ignore
                }
                else
                {
                    //save symbol
                    entries.Add(new Entry() { Value = input[y][x].ToString(), X = x, Y = y, IsSymbol = true });
                }
            }
        }

        List<Entry> partnumbers = entries.Where(e => !e.IsSymbol && entries.Where(en => en.IsSymbol).Any(en => en.AreAdjacent(e))).ToList();

        //PrintToConsole(input, partnumbers, entries);

        return $"{partnumbers.Sum(p => int.Parse(p.Value))}";
    }

    private void PrintToConsole(List<string> input, List<Entry> partnumbers, List<Entry> entries)
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                Console.Write(input[y][x].ToString());
            }
            Console.WriteLine();
        }

        Console.BackgroundColor = ConsoleColor.DarkBlue;

        foreach (Entry entry in partnumbers)
        {
            Console.SetCursorPosition(entry.X, entry.Y);
            Console.Write(entry.Value);
        }

        Console.BackgroundColor = ConsoleColor.Magenta;

        foreach (Entry entry in entries.Where(p => p.IsSymbol))
        {
            Console.SetCursorPosition(entry.X, entry.Y);
            Console.Write(entry.Value);
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }

    
    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Entry> entries = new List<Entry>();
        string currentNumber = string.Empty;
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (Char.IsNumber(input[y][x]))
                {
                    currentNumber += input[y][x];
                    if (x == input[y].Length - 1 || !Char.IsNumber(input[y][x + 1]))//end of line OR last digit
                    {
                        //save number
                        entries.Add(new Entry() { Value = currentNumber, X = x - (currentNumber.Length - 1), Y = y });
                        currentNumber = string.Empty;
                    }
                }
                else if (input[y][x] == '.')
                {
                    //ignore
                }
                else
                {
                    //save symbol
                    entries.Add(new Entry() { Value = input[y][x].ToString(), X = x, Y = y, IsSymbol = true });
                }
            }
        }
        List<Entry> gears = entries.Where(e => e.Value == "*").ToList();


        long totalGearRatio = 0;

        foreach (Entry entry in gears)
        {
            List<Entry> partNumbers = entries.Where(e => !e.IsSymbol && e.AreAdjacent(entry)).ToList();

            if (partNumbers.Count == 2)
            {
                long currentRatio = 1;
                foreach (Entry p in partNumbers)
                {
                    currentRatio = currentRatio * long.Parse(p.Value);
                }
                totalGearRatio += currentRatio;
            }

        }

        //PrintToConsole(input, partnumbers, entries);

        return $"{totalGearRatio}";
    }
}
