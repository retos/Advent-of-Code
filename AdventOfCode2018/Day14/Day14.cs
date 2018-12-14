using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day14
{
    internal class Day14 : DayBase
    {
        public override string Title => "--- Day 14: Chocolate Charts ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int numberOfRecipies = 633601;

            List<int> recipies = new List<int>() { 3, 7 };
            List<int> indexOfElves = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                indexOfElves.Add(i);
            }
            if (isTestRun)
            {
                numberOfRecipies = 9;
            }

            do
            {
                //Generate new recipe
                int newRecipie = recipies[indexOfElves[0]] + recipies[indexOfElves[1]];
                string newRecipieAsString = newRecipie.ToString();
                if (newRecipieAsString.Length > 1)
                {
                    recipies.Add(newRecipieAsString[0] - '0');
                    recipies.Add(newRecipieAsString[1] - '0');
                }
                else
                {
                    recipies.Add(newRecipie);
                }

                //Round of cooking
                for (int i = 0; i < indexOfElves.Count; i++)
                {
                    indexOfElves[i] = (indexOfElves[i] + recipies[indexOfElves[i]] + 1) % recipies.Count;
                }

            } while (recipies.Count < numberOfRecipies + 10);

            return string.Join("", recipies.GetRange(numberOfRecipies, 10));

          
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            int numberOfRecipies = 633601;

            List<int> recipies = new List<int>() { 3, 7 };
            List<int> indexOfElves = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                indexOfElves.Add(i);
            }
            if (isTestRun)
            {
                numberOfRecipies = 51589;
            }
            bool found = false;
            int skipCounter = 0;
            int lastCheckedIndex = 0;
            do
            {
                skipCounter++;
                //Generate new recipe
                int newRecipie = recipies[indexOfElves[0]] + recipies[indexOfElves[1]];
                string newRecipieAsString = newRecipie.ToString();
                if (newRecipieAsString.Length > 1)
                {
                    recipies.Add(newRecipieAsString[0] - '0');
                    recipies.Add(newRecipieAsString[1] - '0');
                }
                else
                {
                    recipies.Add(newRecipie);
                }

                //Round of cooking
                for (int i = 0; i < indexOfElves.Count; i++)
                {
                    indexOfElves[i] = (indexOfElves[i] + recipies[indexOfElves[i]] + 1) % recipies.Count;
                }
                //TODO Get rid of string operations and cheap skip trick to improve performance.
                if (skipCounter % 100000 < 1)
                {
                    found = string.Join("", recipies).IndexOf(numberOfRecipies.ToString(), lastCheckedIndex) > 0;
                    lastCheckedIndex = recipies.Count() - numberOfRecipies.ToString().Length;
                }

            } while (!found);

            return string.Join("", recipies).IndexOf(numberOfRecipies.ToString()).ToString();
        }
    }
}