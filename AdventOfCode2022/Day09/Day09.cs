using System.Collections.Generic;

namespace AdventOfCode2022.Day09;

internal class Day09 : DayBase
{
    public override string Title => "--- Day 9: Rope Bridge ---";
    public override bool Ignore => false;

    public (long zeile, long spalte) TailPosition { get; set; }
    public (long zeile, long spalte) HeadPosition { get; set; }
    List<(long zeile, long spalte)> Knots { get; set; }
    List<(long zeile, long spalte)> PastTailPositions { get; set; }

    static Direction Left = new Direction(0, -1);
    static Direction Right = new Direction(0, 1);
    static Direction Up = new Direction(-1, 0);
    static Direction Down = new Direction(1, 0);

    public record Direction(int zeile, int spalte);

    public override string Part1(List<string> input, bool isTestRun)
    {
        Knots = new List<(long zeile, long spalte)>();
        for (int i = 0; i < 2; i++)
        {
            Knots.Add(new(0, 0));
        }

        PastTailPositions = new List<(long zeile, long spalte)>();
        PastTailPositions.Add(Knots.Last());
        //PrintMap();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ');
            switch (parts[0])
            {
                case "R":
                    Step(Right, int.Parse(parts[1])); break;
                case "U":
                    Step(Up, int.Parse(parts[1])); break;
                case "L":
                    Step(Left, int.Parse(parts[1])); break;
                case "D":
                    Step(Down, int.Parse(parts[1])); break;
                default:
                    break;
            }
        }

        return PastTailPositions.Distinct().Count().ToString();
    }

   
    private void PrintMap()
    {
        long spalteMin = PastTailPositions.Min(p => p.spalte);
        long spalteMax = PastTailPositions.Max(p => p.spalte);
        spalteMin = (spalteMin > Knots.Min(k => k.spalte)) ? Knots.Min(k => k.spalte) : spalteMin;
        spalteMax = (spalteMax < Knots.Max(k => k.spalte)) ? Knots.Max(k => k.spalte) : spalteMax;

        long zeileMin = PastTailPositions.Min(p => p.zeile);
        long zeileMax = PastTailPositions.Max(p => p.zeile);
        zeileMin = (zeileMin > Knots.Min(k => k.zeile)) ? Knots.Min(k => k.zeile) : zeileMin;
        zeileMax = (zeileMax < Knots.Max(k => k.zeile)) ? Knots.Max(k => k.zeile) : zeileMax;

        long zeilenShift = (zeileMin < 0) ? -zeileMin : 0;
        long spaltenShift = (spalteMin < 0) ? -spalteMin : 0;



        //Console.SetCursorPosition(Console.GetCursorPosition(), 0);
        Console.WriteLine();
        for (long z = 0; z <= zeileMax + zeilenShift; z++)
        {
            for (long s = 0; s <= spalteMax + spaltenShift; s++)
            {
                Console.Write(".");

                if (PastTailPositions.Any(p => p.spalte+spaltenShift == s && p.zeile+zeilenShift == z))
                {
                    var pos = Console.GetCursorPosition();
                    Console.SetCursorPosition(pos.Left - 1, pos.Top);
                    Console.Write("#");
                }

                for (int j = 1; j < Knots.Count(); j++)
                {
                    if (Knots[j].spalte + spaltenShift == s && Knots[j].zeile + zeilenShift == z)
                    {
                        var pos = Console.GetCursorPosition();
                        Console.SetCursorPosition(pos.Left-1, pos.Top);
                        Console.Write(j);
                    }
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();

    }

   
    private void Step(Direction dir, int numberOfSteps)
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            //set pos for head
            Knots[0] = (Knots.First().zeile + dir.zeile, Knots.First().spalte + dir.spalte);

            for (int j = 1; j < Knots.Count(); j++)
            {
                if (StillTouching(Knots[j-1], Knots[j]))
                {
                    //knots are touching -> remain
                }
                else
                {
                    long diffSpalte = Knots[j - 1].spalte - Knots[j].spalte;
                    long diffZeile = Knots[j - 1].zeile - Knots[j].zeile;
                    long stepSpalte = 0;
                    long stepZeile = 0;
                    switch (diffSpalte)
                    {
                        case 0:
                            stepSpalte = 0;
                            break;
                        case > 0:
                            stepSpalte = 1;
                            break;
                        case < 0:
                            stepSpalte = -1;
                            break;
                        default:
                            throw new Exception("Bad argument");
                            break;
                    }
                    switch (diffZeile)
                    {
                        case 0:
                            stepZeile = 0;
                            break;
                        case > 0:
                            stepZeile = 1;
                            break;
                        case < 0:
                            stepZeile = -1;
                            break;
                        default:
                            throw new Exception("Bad argument");
                            break;
                    }
                    Knots[j] = (Knots[j].zeile + stepZeile, Knots[j].spalte + stepSpalte);
                    if (j == Knots.Count()-1)
                    {
                        PastTailPositions.Add(Knots[j]);
                        //PrintMap();
                    }
                }
            }
        }
    }

    private bool StillTouching((long zeile, long spalte) first, (long zeile, long spalte) second)
    {
        long diffSpalte = first.spalte - second.spalte;
        long diffZeile = first.zeile - second.zeile;
        return ((diffSpalte <= 1 && diffSpalte >= -1) && (diffZeile <= 1 && diffZeile >= -1));
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        Knots = new List<(long zeile, long spalte)>();
        for (int i = 0; i < 10; i++)
        {
            Knots.Add(new(0, 0));
        }
        
        PastTailPositions = new List<(long zeile, long spalte)>();
        PastTailPositions.Add(Knots.Last());
        //PrintMap();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ');
            switch (parts[0])
            {
                case "R":
                    Step(Right, int.Parse(parts[1])); break;
                case "U":
                    Step(Up, int.Parse(parts[1])); break;
                case "L":
                    Step(Left, int.Parse(parts[1])); break;
                case "D":
                    Step(Down, int.Parse(parts[1])); break;
                default:
                    break;
            }
        }

        return PastTailPositions.Distinct().Count().ToString();
    }

}

