using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day03
{
    internal class Day03 : DayBase
    {
        public override string Title => "--- Day 3: Toboggan Trajectory ---";
        public override bool Ignore => true;
        /// <summary>
        /// Warning! first y then x axis! Map[y][x]
        /// </summary>
        public char[][] Map { get; set; }
        public int XDimension { get; set; }
        public int YDimension { get; set; }

        public override string Part1(List<string> input, bool isTestRun)
        {
            XDimension = input[0].Length;
            YDimension = input.Count;
            Map = input.Select(i => i.ToArray()).ToArray();
            int treeCounter = 0;
            int xPos = 3;

            for (int y = 1; y <= YDimension; y++)
            {
                if (Map[y % YDimension][xPos % XDimension] == '#')
                {
                    treeCounter++;
                }
                xPos = xPos + 3;
            }

            return $"Number of trees: {treeCounter}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            XDimension = input[0].Length;
            YDimension = input.Count;

            Map = input.Select(i => i.ToArray()).ToArray();

            //obviously wrong:  -1029563774 (int)
            //correct:           7560370818 (uint)
            return $"Number of trees: {(uint)CheckRoute(1, 1) * CheckRoute(3, 1) * CheckRoute(5, 1) * CheckRoute(7, 1) * CheckRoute(1, 2)}";

        }

        private int CheckRoute(int right, int down)
        {
            int treeCounter = 0;
            int xPos = right;

            for (int y = down; y < YDimension; y+=down)
            {
                if (Map[y % YDimension][xPos % XDimension] == '#')
                {
                    treeCounter++;
                }
                xPos = xPos + right;
            }

            return treeCounter;
        }
    }
}
