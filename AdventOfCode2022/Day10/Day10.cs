using AdventOfCode2022.Day08;
using System.Collections.Generic;

namespace AdventOfCode2022.Day10;

internal class Day10 : DayBase
{
    public override string Title => "--- Day 10: Cathode-Ray Tube ---";
    public override bool Ignore => true;

    public long X { get; set; }
    private long cycle;
    public List<long> CyclesToRegister { get; set; }
    public List<long> SignalStrenghts { get; set; }
    public char[,] Screen { get; set; }
    public long Cycle 
    {
        get 
        {
            return cycle;
        }
        set 
        {
            #region Partt 1
            if (CyclesToRegister.Any(c => c==cycle))
            {
                SignalStrenghts.Add(SignalStrength);
            }
            #endregion
            #region Part 2
            if (Cycle != 0)
            {
                long xPosCRTDrawing = ((Cycle-1) % 40);
                long yPosCRTDrawing = ((Cycle-1)/ 40) % 6 ;
                
                List<long> spriteCoverage = new List<long>();
                spriteCoverage.Add(X+1-1);
                spriteCoverage.Add(X+1);
                spriteCoverage.Add(X+1+1);

                if (spriteCoverage.Any(c => c==xPosCRTDrawing+1)) 
                {
                    Screen[xPosCRTDrawing, yPosCRTDrawing] = '#';
                }
                else
                {
                    Screen[xPosCRTDrawing, yPosCRTDrawing] = '.';
                }

                //Print();
            }
            #endregion
            cycle = value;

        }
    }
    public long SignalStrength 
    {
        get
        {
            return cycle * X;
        }
    }

    public override string Part1(List<string> input, bool isTestRun)
    {
        CyclesToRegister= new List<long>() {20,60,100,140,180,220 };
        SignalStrenghts = new List<long>();
        X = 1;
        Cycle = 1;
        BuildScreen();
        foreach (string line in input)
        {
            if (line.StartsWith("noop"))
            {
                //one cycle with nothing
                Cycle++;
            }
            else
            {
                long param = long.Parse(line.Split(" ")[1]);
                Cycle++;
                Cycle++;
                X = X + param;
            }
        }

        return SignalStrenghts.Sum().ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        CyclesToRegister = new List<long>() { 20, 60, 100, 140, 180, 220 };
        SignalStrenghts = new List<long>();
        X = 1;
        Cycle = 1;
        BuildScreen();
        foreach (string line in input)
        {
            if (line.StartsWith("noop"))
            {
                //one cycle with nothing
                Cycle++;
            }
            else
            {
                long param = long.Parse(line.Split(" ")[1]);
                Cycle++;
                Cycle++;
                X = X + param;
            }
        }

        Print();
        return "see ASCII printout";
    }

    private void BuildScreen()
    {
        int xMapCount = 40;
        int yMapCount = 6;

        Screen = new char[xMapCount, yMapCount];

        for (int y = 0; y < yMapCount; y++)
        {
            for (int x = 0; x < xMapCount; x++)
            {
                Screen[x, y] = '_';
            }
        }
    }
    private void Print()
    {
        int xMapCount = 40;
        int yMapCount = 6;

        Console.WriteLine();
        
        for (int y = 0; y < yMapCount; y++)
        {
            for (int x = 0; x < xMapCount; x++)
            {
                Console.Write(Screen[x, y]);
            }
            Console.WriteLine();
        }
    }
}