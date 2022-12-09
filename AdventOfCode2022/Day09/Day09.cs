using System.Collections.Generic;

namespace AdventOfCode2022.Day09;

internal class Day09 : DayBase
{
    public override string Title => "--- Day 9: Rope Bridge ---";
    public override bool Ignore => true;

    List<(long zeile, long spalte)> Knots { get; set; }
    List<(long zeile, long spalte)> PastTailPositions { get; set; }

    static Direction Up = new Direction(-1, 0);
    static Direction UpRight = new Direction(-1, 1);
    static Direction Right = new Direction(0, 1);
    static Direction RightDown = new Direction(1, 1);
    static Direction Down = new Direction(1, 0);
    static Direction DownLeft = new Direction(1, -1);
    static Direction Left = new Direction(0, -1);
    static Direction LeftUp = new Direction(-1, -1);
    static Direction NoDirection = new Direction(0, 0);

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



    private void Step(Direction dir, int numberOfSteps)
    {
        for (int i = 0; i < numberOfSteps; i++)
        {
            //set pos for head
            Knots[0] = (Knots.First().zeile + dir.zeile, Knots.First().spalte + dir.spalte);

            for (int j = 1; j < Knots.Count(); j++)
            {
                Direction stepForKnot = GetStepForKnot(Knots[j - 1], Knots[j]);
                
                Knots[j] = (Knots[j].zeile + stepForKnot.zeile, Knots[j].spalte + stepForKnot.spalte);
                if (j == Knots.Count()-1)
                {
                    PastTailPositions.Add(Knots[j]);
                    //PrintMap();
                }                
            }
        }
    }

    private Direction GetStepForKnot((long zeile, long spalte) knot1, (long zeile, long spalte) knot2)
    {
        if (StillTouching(knot1, knot2))
        {
            return NoDirection;
        }
        (long zeile, long spalte) diff = (knot1.zeile-knot2.zeile, knot1.spalte-knot2.spalte);

        switch (diff.zeile, diff.spalte)
        {
            case ( < 0, 0):
                return Up;
                break;
            case ( < 0, > 0):
                return UpRight;
                break;
            case (0, > 0):
                return Right;
                break;
            case ( > 0, > 0):
                return RightDown;
                break;
            case ( > 0, 0):
                return Down;
                break;
            case ( > 0, < 0):
                return DownLeft;
                break;
            case (0, < 0):
                return Left;
                break;
            case (< 0, < 0):
                return LeftUp;
                break;
            default:
                throw new Exception("Bad argument");
                break;
        }
        throw new Exception("Bad argument");
    }

    private bool StillTouching((long zeile, long spalte) firstKnot, (long zeile, long spalte) secondKnot)
    {
        long diffSpalte = firstKnot.spalte - secondKnot.spalte;
        long diffZeile = firstKnot.zeile - secondKnot.zeile;
        return ((diffSpalte <= 1 && diffSpalte >= -1) && (diffZeile <= 1 && diffZeile >= -1));
    }

    private void PrintMap()
    {
        var concatList = Knots.Concat(PastTailPositions);
        long spalteMin = concatList.Min(p => p.spalte);
        long spalteMax = concatList.Max(p => p.spalte);
        long zeileMin = concatList.Min(p => p.zeile);
        long zeileMax = concatList.Max(p => p.zeile);

        long zeilenShift = (zeileMin < 0) ? -zeileMin : 0;
        long spaltenShift = (spalteMin < 0) ? -spalteMin : 0;

        Console.WriteLine();
        for (long z = 0; z <= zeileMax + zeilenShift; z++)
        {
            for (long s = 0; s <= spalteMax + spaltenShift; s++)
            {
                Console.Write(".");

                if (PastTailPositions.Any(p => p.spalte + spaltenShift == s && p.zeile + zeilenShift == z))
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
                        Console.SetCursorPosition(pos.Left - 1, pos.Top);
                        Console.Write(j);
                    }
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

