using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day06
{
    internal class Day06 : DayBase
    {
        public override string Title => "--- Day 6: Custom Customs ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<int> groupoverlaps = new List<int>();
            string currentGroup = string.Empty;

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    int unique = currentGroup.Distinct().Count();
                    groupoverlaps.Add(unique);
                    currentGroup = string.Empty;
                }
                else
                {
                    currentGroup += line;
                }
            }
            //do not forget the last group...
            groupoverlaps.Add(currentGroup.Distinct().Count());

            int prod = 1;
            foreach (int i in groupoverlaps)
            {
                prod = prod * i;
            }

            return $"The sum of each group count is {groupoverlaps.Sum()}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<int> groupoverlaps = new List<int>();
            List<string> currentGroup = new List<string>();
            input.Add(string.Empty);//add empty line, so the code below, goes one last time into the foreach

            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    int currentGroupOverlap = 0;
                    foreach (char c in currentGroup[0])
                    {
                        if (currentGroup.All(p => p.Contains(c)))
                        {
                            currentGroupOverlap++;
                        }
                    }
                    groupoverlaps.Add(currentGroupOverlap);
                    currentGroup = new List<string>();
                }
                else
                {
                    currentGroup.Add(line);
                }
            }

            int prod = 1;
            foreach (int i in groupoverlaps)
            {
                prod = prod * i;
            }

            return $"The sum of each group count is {groupoverlaps.Sum()}";
        }
    }
}
