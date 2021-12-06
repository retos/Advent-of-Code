namespace AdventOfCode2021.Day05;

internal class Day05 : DayBase
{
    public override string Title => "--- Day 5: Hydrothermal Venture ---";
    public override bool Ignore => true;

    public int[,] Map { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        //read to line objects
        List<Line> lines = ReadInput(input);

        //create map, find maxsize
        int xSize = (lines.Max(l => l.XStart) > lines.Max(l => l.XEnd)) ? lines.Max(l => l.XStart) : lines.Max(l => l.XEnd);
        int ySize = (lines.Max(l => l.YStart) > lines.Max(l => l.YEnd)) ? lines.Max(l => l.YStart) : lines.Max(l => l.YEnd);
        Map = new int[xSize+1,ySize+1];

        //draw lines on map
        foreach (Line line in lines)
        {
            //only horizontal lines
            if (line.XStart == line.XEnd)
            {
                for (int y = line.MinY; y <= line.MaxY; y++)
                {
                    Map[line.XStart, y]++;
                }
            }
            if (line.YStart == line.YEnd)
            {
                for (int x = line.MinX; x <= line.MaxX; x++)
                {
                    Map[x, line.YStart]++;
                }
            }
        }
        //write map to console
        /*
        Console.WriteLine();
        for (int y = 0; y <= xSize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                Console.Write(Map[x, y]);                
            }
            Console.WriteLine();
        }
        */

        //write map to textfile
        /*
        StringBuilder stringBuilder = new StringBuilder();        
        for (int y = 0; y <= xSize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                stringBuilder.Append(Map[x, y]);             
            }
            stringBuilder.Append(System.Environment.NewLine);
        }
        File.WriteAllText($"Day05_Part1_Testrun_{isTestRun}.html", $"<pre>{stringBuilder.ToString()}</pre>");        
        */

        //count overlaps: the number of points where at least two lines overlap
        int largerTwo = 0;

        for (int x = 0; x <= xSize; x++)
        {
            for (int y = 0; y <= xSize; y++)
            {
                if (Map[x,y]>=2)
                {
                    largerTwo++;
                }
            }
        }

        return $"number of points: {largerTwo}";
    }



    public override string Part2(List<string> input, bool isTestRun)
    {
        //read to line objects
        List<Line> lines = ReadInput(input);

        //create map, find maxsize
        int xSize = (lines.Max(l => l.XStart) > lines.Max(l => l.XEnd)) ? lines.Max(l => l.XStart) : lines.Max(l => l.XEnd);
        int ySize = (lines.Max(l => l.YStart) > lines.Max(l => l.YEnd)) ? lines.Max(l => l.YStart) : lines.Max(l => l.YEnd);
        Map = new int[xSize + 1, ySize + 1];

        //draw lines on map
        foreach (Line line in lines)
        {
            //only horizontal lines
            if (line.XStart == line.XEnd)
            {
                for (int y = line.MinY; y <= line.MaxY; y++)
                {
                    Map[line.XStart, y]++;
                }
            }
            else if (line.YStart == line.YEnd)
            {
                for (int x = line.MinX; x <= line.MaxX; x++)
                {
                    Map[x, line.YStart]++;
                }
            }
            else //Diagonal as well
            {
                int currentPosX = line.XStart;
                int currentPosY = line.YStart;
                do
                {
                    Map[currentPosX, currentPosY]++;
                    currentPosX = (line.XStart < line.XEnd) ? currentPosX + 1 : currentPosX - 1;
                    currentPosY = (line.YStart < line.YEnd) ? currentPosY + 1 : currentPosY - 1;

                } while (line.PosInRange(currentPosX));
            }

        }
        //write map to console
        /*
        Console.WriteLine();
        for (int y = 0; y <= xSize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                Console.Write(Map[x, y]);                
            }
            Console.WriteLine();
        }
        */

        //write map to textfile
        /*
        StringBuilder stringBuilder = new StringBuilder();        
        for (int y = 0; y <= xSize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                stringBuilder.Append(Map[x, y]);             
            }
            stringBuilder.Append(System.Environment.NewLine);
        }
        File.WriteAllText($"Day05_Part2_Testrun_{isTestRun}.html", $"<pre>{stringBuilder.ToString()}</pre>");        
        */

        //count overlaps: the number of points where at least two lines overlap
        int largerTwo = 0;

        for (int x = 0; x <= xSize; x++)
        {
            for (int y = 0; y <= xSize; y++)
            {
                if (Map[x, y] >= 2)
                {
                    largerTwo++;
                }
            }
        }

        return $"number of points: {largerTwo}";
    }

    private List<Line> ReadInput(List<string> input)
    {
        List<Line> lines = new List<Line>();

        foreach (string inputLine in input)
        {
            Line line = new Line(inputLine);
            lines.Add(line);
        }
        return lines;
    }
}