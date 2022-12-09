namespace AdventOfCode2022.Day08;

internal class Day08 : DayBase
{
    public override string Title => "--- Day 8: Treetop Tree House ---";
    public override bool Ignore => true;
    public Tree[,] Map { get; set; }

    static Direction Left = new Direction(0, -1);
    static Direction Right = new Direction(0, 1);
    static Direction Up = new Direction(-1, 0);
    static Direction Down = new Direction(1, 0);
    record Direction(int drow, int dcol);
    record TreeNode(int height, int irow, int icol);

    public override string Part1(List<string> input, bool isTestRun)
    {
        GetMap(input);
        return Map[0,0].AllNodes.Where(t => t.IsVisible()).Count().ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {

        GetMap(input);
        Tree highScore = Map[0,0].AllNodes.OrderByDescending(n => n.GetScenicScore()).First();
        //if (isTestRun)
        //{
        //    PrintScenicScoreToConsole();
        //}
        return highScore.GetScenicScore().ToString();        
    }



    private void PrintScenicScoreToConsole()
    {
        //Console.SetCursorPosition(Console.GetCursorPosition(), 0);
        Console.WriteLine();
        for (int y = 0; y < Map[0, 0].ZeilenCount(); y++)
        {
            for (int x = 0; x < Map[0, 0].SpaltenCount(); x++)
            {
                Console.Write(Map[x, y].GetScenicScore());
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    protected Tree[,] GetMap(List<string> input)
    {
        int xMapCount = input[0].Length;
        int yMapCount = input.Count;

        Map = new Tree[input[0].Length, input.Count];
        List<Tree> allNodes = new List<Tree>();
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                int height = int.Parse(input[y][x].ToString());
                Tree tree = new Tree(Map, x, y, allNodes);
                tree.Height = height;
                Map[x, y] = tree;
                allNodes.Add(tree);
            }
        }
        return Map;
    }
}

