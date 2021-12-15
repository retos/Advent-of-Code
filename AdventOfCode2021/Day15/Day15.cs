namespace AdventOfCode2021.Day15;

internal class Day15 : DayBase
{
    public override string Title => "--- Day 15 ---";
    public override bool Ignore => false;
    public List<Coordinate> Coordinates{ get; set; }
    public int MapWith { get; set; }
    public int MapHeight { get; set; }
    public Coordinate Target { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        //map[][]  (+modulo)                                        Day 03 2020 https://adventofcode.com/2020/day/3
        //map[,]   (+output to console, to html file)               Day 11 2021, Day 09 2021
        //switch case string & int parsing                          Day 02 2021
        //convert binaryint to decimal  Convert.ToInt32(string, 2)  Day 03 2021
        //visualisation txt->jpg with irfanview, jpg frames->mp4 with avidemux Day 05 2021 & Day09 2021
        //visualisation png                                         Day 10 2018
        //dictionary                                                Day 06 2021

        Coordinates = new();
        MapHeight = input.Count;
        MapWith = input[0].Length;
        Coordinate[,] map = new Coordinate[MapWith, MapHeight];
        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWith; x++)
            {
                Coordinate coord = new Coordinate(int.Parse(input[y][x].ToString()), x, y, ref map);
                Coordinates.Add(coord);
            }
        }


        //Dijkstra’s Shortest Path Algorithm https://www.youtube.com/watch?v=pVfj6mxhdMw
        Coordinate start = Coordinates.Where(c => c.X == 0 && c.Y == 0).Single();
        start.Cost = 0;
        Target = start.Map[MapWith-1, MapHeight-1];
        Dijkstra();
        WriteHtml($"Part1_Testrun{(isTestRun)}");

        return $"{Target.Cost}";
    }

    private void Dijkstra()
    {
        do
        {
            Coordinate chepestSpot = Coordinates.Where(c => !c.Visited).OrderBy(c => c.Cost).FirstOrDefault();
            chepestSpot.Visited = true;

            List<Coordinate> unvisitedNeighbors = chepestSpot.GetUnvisitedNeighbors();

            foreach (Coordinate neighbor in unvisitedNeighbors)
            {
                long costFromHere = chepestSpot.Cost + neighbor.Value;
                if (costFromHere < neighbor.Cost)
                {
                    neighbor.Cost = costFromHere;
                    neighbor.PreviousCoordinate = chepestSpot;
                }
            }
            Console.SetCursorPosition(0,0);
            Console.WriteLine($"total nodes: {Coordinates.Count()}");
            Console.WriteLine($"visited    : {Coordinates.Where(c => c.Visited).Count()}");
            //WriteHtml($"Debug_visitedNodes_{Coordinates.Where(c => c.Visited).Count()}_of_{Coordinates.Count()}");
        } while (!Target.Visited);

        MarkPath(Target);
    }

    private void MarkPath(Coordinate currentNode)
    {
        //Walk back from the target and mark nodes as path
        currentNode.PartOfThePath = true;
        if (currentNode.X==0 && currentNode.Y==0) return;
        MarkPath(currentNode.PreviousCoordinate);
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        Coordinates = new();
        MapHeight = input.Count * 5;
        MapWith = input[0].Length * 5;
        Coordinate[,] map = new Coordinate[MapWith, MapHeight];

        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                //row 1
                int value = int.Parse(input[y][x].ToString());
                int stepper = input.Count;

                for (int yOffset = 0; yOffset < 5; yOffset++)
                {
                    for (int xOffset = 0; xOffset < 5; xOffset++)
                    {
                        int newValue = value + xOffset + yOffset;
                        int offsetValue = (newValue <= 9) ? newValue : newValue % 9;
                        Coordinate coord = new Coordinate(offsetValue, x + (stepper * xOffset), y + (stepper * yOffset), ref map);
                        Coordinates.Add(coord);
                    }
                }
            }
        }

        //Dijkstra’s Shortest Path Algorithm https://www.youtube.com/watch?v=pVfj6mxhdMw
        Coordinate start = Coordinates.Where(c => c.X == 0 && c.Y == 0).Single();
        start.Cost = 0;
        Target = Coordinates.Where(c => c.X == MapWith - 1 && c.Y == MapHeight - 1).Single();
        Dijkstra();
        WriteHtml($"Part2_Testrun{(isTestRun)}");

        return $"{Target.Cost}";
    }

    private void WriteHtml(string suffix)
    {
        Coordinate[,] map = Target.Map;
        StringBuilder stringBuilder = new StringBuilder();
        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWith; x++)
            {
                if (map[x,y].PartOfThePath)
                {
                    stringBuilder.Append($"<span style='color: #ffffff; font - style: normal; text - shadow: 0 0 5px #ffffff;'>{map[x, y].Value}</span>");
                }
                else if(!map[x,y].Visited)
                {
                    stringBuilder.Append($"<span style='color: #009900;'>{map[x, y].Value}</span>");
                }
                else
                {
                    stringBuilder.Append($"<span style='color: #626260;'>{map[x, y].Value}</span>");
                }
            }
            stringBuilder.Append(System.Environment.NewLine);
        }
        File.WriteAllText($"Day15_{suffix}.html", $"<body style='background: #0f0f23'><pre>{stringBuilder.ToString()}</pre></body>");
    }
}