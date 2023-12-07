using System.Collections.Generic;

namespace AdventOfCode2023.Day05b;

internal class Day05b : DayBase
{
    public override string Title => "--- Day 5b: If You Give A Seed A Fertilizer (Less Brute Force, too many objects) ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        input.Add(string.Empty);//adding empty line at end to ensure code adds last maps as well
        List<Node> initialNodes = Node.ConvertSeed(input[0]);
        int startLine = 0;
        int endLine = 0;
        List<Node> currentNodes = initialNodes;

        for (int i = endLine+2; i < input.Count; i++)
        {
            if (input[i].Contains(":"))
            {
                startLine = i + 1;
            }
            else if (string.IsNullOrEmpty(input[i]))
            {
                endLine = i - 1;
                List<string> map = input.GetRange(startLine, endLine - startLine + 1);
                Node.GenerateNodeList(map, ref currentNodes);
                //Preparation for next round
                currentNodes = currentNodes.Select(n => n.SubNode).ToList();
            }
        }

        long lowestLocation = initialNodes.Select(n => n.SubNode.SubNode.SubNode.SubNode.SubNode.SubNode.SubNode).OrderBy(n => n.Id).First().Id;   

        return $"{lowestLocation}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        input.Add(string.Empty);//adding empty line at end to ensure code adds last maps as well
        List<Node> initialNodes = Node.ConvertSeedPart2(input[0]);
        int startLine = 0;
        int endLine = 0;
        List<Node> currentNodes = initialNodes;

        for (int i = endLine + 2; i < input.Count; i++)
        {
            if (input[i].Contains(":"))
            {
                startLine = i + 1;
            }
            else if (string.IsNullOrEmpty(input[i]))
            {
                endLine = i - 1;
                List<string> map = input.GetRange(startLine, endLine - startLine + 1);
                Node.GenerateNodeList(map, ref currentNodes);
                //Preparation for next round
                currentNodes = currentNodes.Select(n => n.SubNode).ToList();
            }
        }

        long lowestLocation = initialNodes.Select(n => n.SubNode.SubNode.SubNode.SubNode.SubNode.SubNode.SubNode).OrderBy(n => n.Id).First().Id;

        return $"{lowestLocation}";
    }
}
