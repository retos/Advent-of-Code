using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day02
{
    internal class Day02 : DayBase
    {
        public override string Title => "--- Day 2: Inventory Management System ---";

        public override bool Ignore => true;

        private static int GetDifference(string idA, string idB)
        {
            int difference = 0;
            for (int i = 0; i < idA.Length; i++)
            {
                if (idA[i] != idB[i])
                {
                    difference++;
                }
            }
            return difference;
        }

        public override string Part1(List<string> input)
        {
            int doppelt = 0;
            int dreifach = 0;

            foreach (string line in input)
            {
                bool doppeltGefunden = false;
                bool dreifachGefunden = false;
                foreach (char c in line)
                {
                    int anzahl = line.Count(x => x == c);
                    if (anzahl == 2 && !doppeltGefunden)
                    {
                        doppeltGefunden = true;
                        doppelt++;
                    }
                    if (anzahl == 3 && !dreifachGefunden)
                    {
                        dreifachGefunden = true;
                        dreifach++;
                    }
                }
            }

            return $"checksum: {doppelt * dreifach}";
        }

        public override string Part2(List<string> input)
        {
            string s1 = string.Empty;
            string s2 = string.Empty;
            int difference = int.MaxValue;

            foreach (string idA in input)
            {
                foreach (string idB in input)
                {
                    if (idA != idB)
                    {
                        //diff = set1.Except(set2).ToList();
                        int currentDifference = GetDifference(idA, idB);

                        if (currentDifference < difference)
                        {
                            difference = currentDifference;
                            s1 = idA;
                            s2 = idB;
                        }
                    }
                }
            }

            string answer = string.Empty;

            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    return $"Part2: common letters in both ids: {s1.Remove(i, 1)}";
                }
            }

            return answer;
        }
    }
}