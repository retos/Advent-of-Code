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

        return "";

        Coordinates = new();
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                Coordinate coord = new Coordinate(int.Parse(input[y][x].ToString()), x, y);
                Coordinates.Add(coord);
            }
        }
        MapHeight = input.Count;
        MapWith = input[0].Length;

        //Dijkstra’s Shortest Path Algorithm https://www.youtube.com/watch?v=pVfj6mxhdMw
        Coordinate start = Coordinates.Where(c => c.X == 0 && c.Y == 0).Single();
        start.Cost = 0;
        Dijkstra();



        return $"{Coordinates.Where(c => c.X == MapWith - 1 && c.Y == MapHeight - 1).Single().Cost}";
    }

    private void Dijkstra()
    {
        do
        {
            Coordinate chepestSpot = Coordinates.Where(c => !c.Visited).OrderBy(c => c.Cost).FirstOrDefault();
            chepestSpot.Visited = true;

            List<Coordinate> unvisitedNeighbors = GetUnvisitedNeighbors(chepestSpot);

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
        } while (!Target.Visited);
    }

    private List<Coordinate> GetUnvisitedNeighbors(Coordinate chepestSpot)
    {
        List<Coordinate> neighbors = new();
        //up
        Coordinate up = Coordinates.Where(c => c.X == chepestSpot.X && c.Y == chepestSpot.Y - 1 && !c.Visited).SingleOrDefault();
        if (chepestSpot.Y > 1 && null != up)
        {
            neighbors.Add(up);
        }//right
        Coordinate right = Coordinates.Where(c => c.X == chepestSpot.X + 1 && c.Y == chepestSpot.Y && !c.Visited).SingleOrDefault();
        if (chepestSpot.Y < MapWith && null != right)
        {
            neighbors.Add(right);
        }//down
        Coordinate down = Coordinates.Where(c => c.X == chepestSpot.X && c.Y == chepestSpot.Y + 1 && !c.Visited).SingleOrDefault();
        if (chepestSpot.Y < MapHeight && null != down)
        {
            neighbors.Add(down);
        }//left
        Coordinate left = Coordinates.Where(c => c.X == chepestSpot.X - 1 && c.Y == chepestSpot.Y && !c.Visited).SingleOrDefault();
        if (chepestSpot.Y > 1 && null != left)
        {
            neighbors.Add(left);
        }
        return neighbors;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        Coordinates = new();
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                //row 1
                int value = int.Parse(input[y][x].ToString());
                Coordinate coord = new Coordinate(value, x, y);
                Coordinates.Add(coord);
                int stepper = input.Count;
                int newValue = value + 1;
                int value2 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 2;
                int value3 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 3;
                int value4 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 4;
                int value5 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 5;
                int value6 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 6;
                int value7 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 7;
                int value8 = (newValue <= 9) ? newValue : newValue % 9;
                newValue = value + 8;
                int value9 = (newValue <= 9) ? newValue : newValue % 9;

                coord = new Coordinate(value2, x+(stepper), y);
                Coordinates.Add(coord);
                coord = new Coordinate(value3, x+(2*stepper), y);
                Coordinates.Add(coord);
                coord = new Coordinate(value4, x+(3*stepper), y);
                Coordinates.Add(coord);
                coord = new Coordinate(value5, x+(4*stepper), y);
                Coordinates.Add(coord);
                //row2
                coord = new Coordinate(value2, x, y + stepper);
                Coordinates.Add(coord);
                coord = new Coordinate(value3, x + (stepper), y + (stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value4, x + (2 * stepper), y + (stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value5, x + (3 * stepper), y + (stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value6, x + (4 * stepper), y + (stepper));
                Coordinates.Add(coord);
                //row3
                coord = new Coordinate(value3, x, y + (2 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value4, x + (stepper), y + (2 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value5, x + (2 * stepper), y + (2 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value6, x + (3 * stepper), y + (2 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value7, x + (4 * stepper), y + (2 * stepper));
                Coordinates.Add(coord);
                //row4
                coord = new Coordinate(value4, x, y + (3 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value5, x + (stepper), y + (3 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value6, x + (2 * stepper), y + (3 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value7, x + (3 * stepper), y + (3 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value8, x + (4 * stepper), y + (3 * stepper));
                Coordinates.Add(coord);
                //row5
                coord = new Coordinate(value5, x, y + (4 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value6, x + (stepper), y + (4 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value7, x + (2 * stepper), y + (4 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value8, x + (3 * stepper), y + (4 * stepper));
                Coordinates.Add(coord);
                coord = new Coordinate(value9, x + (4 * stepper), y + (4 * stepper));
                Coordinates.Add(coord);
            }
        }

        MapHeight = input.Count*5;
        MapWith = input[0].Length*5;

        //Dijkstra’s Shortest Path Algorithm https://www.youtube.com/watch?v=pVfj6mxhdMw
        Coordinate start = Coordinates.Where(c => c.X == 0 && c.Y == 0).Single();
        start.Cost = 0;
        Target = Coordinates.Where(c => c.X == MapWith - 1 && c.Y == MapHeight - 1).Single();
        Dijkstra();



        return $"{Coordinates.Where(c => c.X == MapWith - 1 && c.Y == MapHeight - 1).Single().Cost}";
    }
}