using System.Collections.Generic;

namespace AdventOfCode2022.Day12;

internal class Day12 : DayBase
{
    public override string Title => "--- Day 12: Hill Climbing Algorithm ---";
    public override bool Ignore => false;
    public Node[,] Map { get; set; }


    public override string Part1(List<string> input, bool isTestRun)
    {
        GetMap(input);

        Map[0, 0].Dijkstra(Map[0,0].AllNodes.Where(n => n.ElevationChar == 'E').First());
        Node target = Map[0, 0].AllNodes.Where(n => n.ElevationChar == 'E').First();

        int pathLength = target.GetPathLenth();

        return (pathLength).ToString();
    }

    protected Node[,] GetMap(List<string> input, bool partTwo = false)
    {
        int xMapCount = input[0].Length;
        int yMapCount = input.Count;

        Map = new Node[input[0].Length, input.Count];
        List<Node> allNodes = new List<Node>();
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {                
                Node tree = new Node(Map, x, y, allNodes, input[y][x], partTwo);
                Map[x, y] = tree;
                allNodes.Add(tree);
            }
        }
        return Map;
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        GetMap(input, true);

        Map[0, 0].Dijkstra(Map[0, 0].AllNodes.Where(n => n.ElevationChar == 'E').First(), true);
        Node target = Map[0, 0].AllNodes.Where(n => n.ElevationChar == 'E').First();

        List<Node> path = target.GetPathList();
        return (path.Count()-1).ToString();
    }

}