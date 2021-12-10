namespace AdventOfCode2021.Day09;

internal class Day09 : DayBase
{
    public override string Title => "--- Day 9: Smoke Basin ---";
    public override bool Ignore => true;

    public int[,] Map { get; set; }
    public int XDimension { get; set; }
    public int YDimension { get; set; }
    public Dictionary<string, bool> BelongsToBasin { get; set; }
    public int GlobalPrintCounter { get; set; }
    public bool IsTestRun { get; set; }
    public bool DebugMode { get; set; }
    public override string Part1(List<string> input, bool isTestRun)
    {
        IsTestRun = isTestRun;
        GlobalPrintCounter = 1;
        XDimension = input[0].Length-1;
        YDimension = input.Count-1;
        char[][] map = input.Select(i => i.ToArray()).ToArray();
        Map = new int[XDimension+1, YDimension+ 1];

        //Migrate from 2d char Array to 2d int array 
        //would be better to directly go for the int array...
        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                Map[x, y] = int.Parse(map[y][x].ToString());
            }
        }

        int risklevelSum = 0;
        //checking all the points
        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                if (NeighborsAreHigher(x, y))
                {
                    risklevelSum += Map[x, y] + 1;
                }
            }
        }

        return $"{risklevelSum}";
    }

    private bool NeighborsAreHigher(int x, int y)
    {
        int up = (y > 0) ? Map[x, y - 1] : Map[x, y] + 1; //if out of bounds keep self
        int down = (y < YDimension) ? Map[x, y + 1] : Map[x, y] + 1; //if out of bounds keep self
        int right = (x < XDimension) ? Map[x + 1, y] : Map[x, y] + 1;  //if out of bounds keep self
        int left = (x > 0) ? Map[x - 1, y] : Map[x, y] + 1;  //if out of bounds keep self

        return Map[x, y] < up
            && Map[x, y] < down
            && Map[x, y] < right
            && Map[x, y] < left;
    }
    private int GatherBasin(int x, int y, bool isTestRun)
    {
        if (BelongsToBasin[$"{x},{y}"]) return 0;

        int basinSize = 0;

        if (Map[x, y] == 9) return 0;

        int up = (y > 0 && !BelongsToBasin[$"{x},{y - 1}"]) ? Map[x, y - 1] : Map[x, y] + 1; //if out of bounds keep self
        int down = (y < YDimension && !BelongsToBasin[$"{x},{y + 1}"]) ? Map[x, y + 1] : Map[x, y] + 1; //if out of bounds keep self
        int right = (x < XDimension && !BelongsToBasin[$"{x + 1},{y}"]) ? Map[x + 1, y] : Map[x, y] + 1;  //if out of bounds keep self
        int left = (x > 0 && !BelongsToBasin[$"{x - 1},{y}"]) ? Map[x - 1, y] : Map[x, y] + 1;  //if out of bounds keep self

        bool isLowPoint = Map[x, y] < up
            && Map[x, y] < down
            && Map[x, y] < right
            && Map[x, y] < left;

        WriteToHml(x, y);
        if (isLowPoint)
        {
            BelongsToBasin[$"{x},{y}"] = true;
            basinSize++;
            //if up exists check
            if (y > 0 && up > Map[x, y] )
            {
                basinSize += CheckSpot(x, y-1);
            }
            //if down exists check
            if (y < YDimension && down>Map[x,y])
            {
                basinSize += CheckSpot(x, y + 1);
            }
            //if left exists check
            if (x > 0 && left > Map[x, y])
            {
                basinSize += CheckSpot(x-1, y);
            }
            //if right exists check
            if (x < XDimension && right > Map[x, y])
            {
                basinSize += CheckSpot(x+1, y);
            }
        }
        return basinSize;
    }

    private int CheckSpot(int x, int y)
    {
        //belongs to basin or is 9
        if (BelongsToBasin[$"{x},{y}"] || Map[x, y] == 9) return 0;

        int poolSize = 1; //count selv
        BelongsToBasin[$"{x},{y}"] = true;
        WriteToHml(x, y);

        //if up exists check
        if (y > 0 && Map[x, y - 1] > Map[x, y])
        {
            poolSize += CheckSpot(x, y - 1);
        }
        //if down exists check
        if (y < YDimension && Map[x, y + 1] > Map[x, y])
        {
            poolSize += CheckSpot(x, y + 1);
        }
        //if left xists check
        if (x > 0 && Map[x - 1, y] > Map[x, y])
        {
            poolSize += CheckSpot(x - 1, y);
        }
        //if right exists check
        if (x < XDimension && Map[x + 1, y] > Map[x, y])
        {
            poolSize += CheckSpot(x + 1, y);
        }
        
        return poolSize;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        DebugMode = false;
        GlobalPrintCounter = 1;
        IsTestRun = isTestRun;
        XDimension = input[0].Length - 1;
        YDimension = input.Count - 1;
        char[][] map = input.Select(i => i.ToArray()).ToArray();
        Map = new int[XDimension + 1, YDimension + 1];

        BelongsToBasin = new Dictionary<string, bool>();


        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                Map[x, y] = int.Parse(map[y][x].ToString());
                BelongsToBasin[$"{x},{y}"] = false;
            }
        }

        List<int> pools = new List<int>();

        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                pools.Add(GatherBasin(x, y, isTestRun));
            }
        }

        pools = pools.OrderByDescending(x => x).ToList();

        //write map to textfile
        WriteToHml(int.MaxValue, int.MaxValue);

        return $"{pools[0] * pools[1] * pools[2]}";
    }

    private void WriteToHml(int xPos, int yPos)
    {
        if (!DebugMode) return;
        
        StringBuilder stringBuilder = new StringBuilder();
        for (int y = 0; y <= YDimension; y++)
        {
            for (int x = 0; x <= XDimension; x++)
            {
                if (x==xPos && y == yPos)
                {
                    stringBuilder.Append($"<span style='color: #009900;'>{Map[x, y]}</span>");
                }
                else if (BelongsToBasin[$"{x},{y}"])
                {
                    stringBuilder.Append($"<span style='color: #ffffff; font - style: normal; text - shadow: 0 0 5px #ffffff;'>{Map[x, y]}</span>");
                }
                else
                {
                    stringBuilder.Append($"<span style='color: #626260;'>{Map[x, y]}</span>");
                }
            }
            stringBuilder.Append(System.Environment.NewLine);
        }
        File.WriteAllText($"Day09_Part2_Testrun_{IsTestRun}{GlobalPrintCounter:0000000000}_{xPos}_{yPos}.html", $"<body style='background: #0f0f23'><pre>{stringBuilder.ToString()}</pre></body>");
        GlobalPrintCounter++;
    }
}