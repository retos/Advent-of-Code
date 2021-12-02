namespace AdventOfCode2021.Day02;

internal class Day02 : DayBase
{
    public override string Title => "--- Day 2: Dive! ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        int horizontalPos = 0;
        int depth = 0;
        foreach (string line in input)
        {
            string[] parts = line.Split(" ");
            switch (parts[0])
            {
                case "forward":
                    horizontalPos = horizontalPos + int.Parse(parts[1]);
                    break;
                case "down":
                    depth = depth + int.Parse(parts[1]);
                    break;
                case "up":
                    depth = depth - int.Parse(parts[1]);
                    break;
                default:
                    throw new Exception("bad command");
                    break;
            }
        }

        return $"depth {depth}, horizontalPos {horizontalPos}, multiplied: {depth* horizontalPos}";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        int horizontalPos = 0;
        int depth = 0;
        int aim = 0;
        foreach (string line in input)
        {
            string[] parts = line.Split(" ");
            switch (parts[0])
            {
                case "forward":
                    horizontalPos = horizontalPos + int.Parse(parts[1]);
                    depth = depth + (aim * int.Parse(parts[1]));
                    break;
                case "down":
                    aim = aim + int.Parse(parts[1]);
                    break;
                case "up":
                    aim = aim - int.Parse(parts[1]);
                    break;
                default:
                    throw new Exception("bad command");
                    break;
            }
        }

        return $"depth {depth}, horizontalPos {horizontalPos}, multiplied: {depth * horizontalPos}";
    }
}

