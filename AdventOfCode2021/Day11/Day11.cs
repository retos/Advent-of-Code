namespace AdventOfCode2021.Day11;

internal class Day11 : DayBase
{
    public override string Title => "--- Day 11 ---";
    public override bool Ignore => false;
    public int XDimension { get; set; }
    public int YDimension { get; set; }
    public int[,] Map { get; set; }
    public Dictionary<string, bool> Flashed { get; set; }
    public ulong TotalFlashcount { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        XDimension = input[0].Length - 1;
        YDimension = input.Count - 1;
        Map = new int[XDimension + 1, YDimension + 1];
        TotalFlashcount = 0;

        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                Map[x, y] = int.Parse(input[y][x].ToString());
            }
        }

        //STEPS
        for (int i = 1; i <= 100; i++)
        {
            Flashed = new();//reset flashdictionary

            //First, the energy level of each octopus increases by 1.
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    Map[x, y]++;
                }
            }
            //Then, any octopus with an energy level greater than 9 flashes.
            //This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.
            //If this causes an octopus to have an energy level greater than 9,
            //it also flashes.This process continues as long as new octopuses keep having their energy level increased beyond 9.
            //(An octopus can only flash at most once per step.)
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    if (Map[x, y] > 9)
                    {
                        //Flash
                        Flash(x, y);
                    }
                }
            }

            //Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    if (Flashed.ContainsKey($"{x},{y}") && Flashed[$"{x},{y}"])
                    {
                        Map[x, y] = 0;
                    }
                }
            }
            //PrintToConsole();
            TotalFlashcount = TotalFlashcount + (ulong)Flashed.Count();
        }
        

        return $"{TotalFlashcount}";
    }

    private void PrintToConsole()
    {
        //Console.SetCursorPosition(Console.GetCursorPosition(), 0);
        Console.WriteLine();
        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                Console.Write(Map[x,y]);
            }
            Console.WriteLine();
        }
    }

    private void Flash(int x, int y)
    {
        if (!Flashed.ContainsKey($"{x},{y}") || !Flashed[$"{x},{y}"])
        {
            Flashed[$"{x},{y}"] = true;
            //if up exists increase
            if (y > 0)
            {
                Map[x, y - 1]++;
                if (Map[x, y - 1] > 9)
                {
                    Flash(x, y - 1);
                }
            }
            //if right-up exists check
            if (x < XDimension && y > 0)
            {
                Map[x + 1, y - 1]++;
                if (Map[x + 1, y - 1] > 9)
                {
                    Flash(x + 1, y - 1);
                }
            }
            //if right exists check
            if (x < XDimension)
            {
                Map[x + 1, y]++;
                if (Map[x + 1, y] > 9)
                {
                    Flash(x + 1, y);
                }
            }
            //if right-down exists check
            if (x < XDimension && y < YDimension)
            {
                Map[x + 1, y + 1]++;
                if (Map[x + 1, y + 1] > 9)
                {
                    Flash(x + 1, y + 1);
                }
            }
            //if down exists check
            if (y < YDimension)
            {
                Map[x, y + 1]++;
                if (Map[x, y + 1] > 9)
                {
                    Flash(x, y + 1);
                }
            }
            //if down-left exists check
            if (y < YDimension && x > 0)
            {
                Map[x - 1, y + 1]++;
                if (Map[x - 1, y + 1] > 9)
                {
                    Flash(x - 1, y + 1);
                }
            }
            //if left exists check
            if (x > 0)
            {
                Map[x - 1, y]++;
                if (Map[x - 1, y] > 9)
                {
                    Flash(x - 1, y);
                }
            }
            //if left-up exists check
            if (x > 0 && y > 0)
            {
                Map[x - 1, y - 1]++;
                if (Map[x - 1, y - 1] > 9)
                {
                    Flash(x - 1, y - 1);
                }
            }
        }
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        XDimension = input[0].Length - 1;
        YDimension = input.Count - 1;
        Map = new int[XDimension + 1, YDimension + 1];
        TotalFlashcount = 0;

        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                Map[x, y] = int.Parse(input[y][x].ToString());
            }
        }

        //STEPS
        for (int i = 1; i <= int.MaxValue; i++)
        {
            Flashed = new();//reset flashdictionary

            //First, the energy level of each octopus increases by 1.
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    Map[x, y]++;
                }
            }
            //Then, any octopus with an energy level greater than 9 flashes.
            //This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.
            //If this causes an octopus to have an energy level greater than 9,
            //it also flashes.This process continues as long as new octopuses keep having their energy level increased beyond 9.
            //(An octopus can only flash at most once per step.)
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    if (Map[x, y] > 9)
                    {
                        //Flash
                        Flash(x, y);
                    }
                }
            }

            //Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
            for (int y = 0; y <= YDimension; y++)
            {
                for (int x = 0; x <= XDimension; x++)
                {
                    if (Flashed.ContainsKey($"{x},{y}") && Flashed[$"{x},{y}"])
                    {
                        Map[x, y] = 0;
                    }
                }
            }
            //PrintToConsole();
            if (Flashed.Count() >= 100) return ($"{i}");
            TotalFlashcount = TotalFlashcount + (ulong)Flashed.Count();
        }

        return $"not found!";
    }
}