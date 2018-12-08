using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day08
{
    internal class Node
    {
        public List<Node> ChildNodes { get; set; }
        public List<int> Metaentries { get; set; }

        public Node(List<int> header, ref int i)
        {
            ChildNodes = new List<Node>();
            Metaentries = new List<int>();
           
            int childrenCount = header[i++];
            int metaCount = header[i++];

            for (int j = 0; j < childrenCount; j++)
            {
                ChildNodes.Add(new Node(header, ref i));
            }

            for (int j = 0; j < metaCount; j++)
            {
                Metaentries.Add(header[i++]);
            }
         }

        public int SumOfMedatataEnties
        {
            get
            {
                return Metaentries.Sum(e => e) + ChildNodes.Select(c => c.SumOfMedatataEnties).Sum(c => c);
            }
        }

        public int Value
        {
            get
            {
                if (!ChildNodes.Any())
                {
                    return SumOfMedatataEnties;
                }

                int value = 0;
                foreach (int meta in Metaentries)
                {
                    if (meta <= ChildNodes.Count)
                    {
                        value += ChildNodes[meta - 1].Value;
                    }
                }
                return value;
            }
        }
    }
}