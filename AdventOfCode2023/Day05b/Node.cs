
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace AdventOfCode2023.Day05b
{
    internal class Node
    {
        public long Id { get; private set; }
        public Node SubNode { get; private set; }

        internal static void GenerateNodeList(List<string> list, ref List<Node> validSourceNodes)
        {
            foreach (string map in list)
            {
                string[] parts = map.Split(' ');
                long destinationRangeStart = long.Parse(parts[0]);
                long sourceRangeStart = long.Parse(parts[1]);
                long rangeLenght = long.Parse(parts[2]);


                //for (long i = sourceRangeStart; i < sourceRangeStart+rangeLenght; i++)
                //{
                //    Node currentNode = validSourceNodes.Where(n => n.Id == i).FirstOrDefault();
                //    if (currentNode != null)
                //    {
                //        currentNode.SubNode = new Node();
                //        currentNode.SubNode.Id = i-sourceRangeStart + destinationRangeStart;
                //    }
                //}

                foreach (Node node in validSourceNodes)
                {
                    if (node.Id >= sourceRangeStart && node.Id <= sourceRangeStart+rangeLenght)
                    {
                        if (node.SubNode == null)//only add subnode if not already defined
                        {
                            node.SubNode = new Node();
                            node.SubNode.Id = destinationRangeStart - sourceRangeStart + node.Id;
                        }
                    }
                }
            }

            //Populate empty subnodes
            foreach (var n in validSourceNodes)
            {
                if (n.SubNode == null)
                {
                    n.SubNode = new Node() {Id = n.Id};
                }
            }
        }

        public static List<Node> ConvertSeed(string v)
        {
            List<Node> seeds = new List<Node>();
            string[] parts = v.Substring(7).Split(' ');
            foreach (string part in parts)
            {
                long number = long.Parse(part);
                Node node = new Node();
                node.Id = number;
                seeds.Add(node);
            }

            return seeds;
        }
        public static List<Node> ConvertSeedPart2(string v)
        {
            List<Node> seeds = new List<Node>();
            long[] parts = v.Substring(7).Split(' ').Select(s => long.Parse(s)).ToArray();

            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (long j = parts[i]; j < parts[i+1]+ parts[i]; j++)
                    {
                        Node node = new Node() {Id = j };
                        seeds.Add(node);
                    }
                }
                else
                {
                    //skip if odd
                }
            }


            return seeds;
        }
    }
}