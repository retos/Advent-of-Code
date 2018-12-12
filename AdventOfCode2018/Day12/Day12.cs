using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace AdventOfCode2018.Day12
{
    internal class Day12 : DayBase
    {
        public override string Title => "--- Day 12: Subterranean Sustainability ---";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int offset = 3;
            bool[] plants = new bool[input[0].Length-15+offset]; //+2 to add pots for the future

            for (int i = 0; i < input[0].Length - 15; i++)
            {
                plants[i+offset] = input[0][i+15].Equals('#');
            }

            List<Combination> combinations = new List<Combination>();

            foreach (string rule in input.Skip(2))
            {
                combinations.Add(new Combination(rule));
            }
            Console.WriteLine("000"+ string.Join("", plants.Select(p => (p) ? "1" : ".")));
            for (int j = 0; j < 20; j++)
            {
                bool[] nextPlants = new bool[plants.Length+1];
                for (int i = 0; i < plants.Length; i++)
                {
                    bool[] currentPlantRange = new bool[5];
                    currentPlantRange[0] = (i - 2 >= 0) ? plants[i - 2] : false;
                    currentPlantRange[1] = (i - 1 >= 0) ? plants[i - 1] : false;
                    currentPlantRange[2] = plants[i];
                    currentPlantRange[3] = (i + 1 < plants.Length) ? plants[i + 1] : false;
                    currentPlantRange[4] = (i + 2 < plants.Length) ? plants[i + 2] : false;                    
                    
                    List<Combination> matchingRules = combinations.Where(c => c.Configuration.SequenceEqual(currentPlantRange)).ToList();
                    if (matchingRules.Any())
                    {
                        nextPlants[i] = matchingRules.First().LeadsToPlant;
                    }
                    else
                    {
                        nextPlants[i] = false;
                    }

                }
                Console.WriteLine((j+1).ToString().PadLeft(3) + string.Join("",nextPlants.Select(p => (p)? "1":".")));
                plants = nextPlants;
            }

            int sum = 0;
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i])
                {
                    sum += i - offset;
                }
            }
            //too high 3462, Wieso?
            return sum.ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}