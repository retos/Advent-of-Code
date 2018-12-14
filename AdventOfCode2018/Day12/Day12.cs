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

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int offset = 3;
            bool[] plants = new bool[input[0].Length-15+offset];
            
            for (int i = 0; i < input[0].Length - 15; i++)
            {
                plants[i+offset] = input[0][i+15].Equals('#');
            }
            plants = AddEmptyPotsIfNeeded(plants, ref offset);

            List<Combination> combinations = new List<Combination>();

            foreach (string rule in input.Skip(2))
            {
                combinations.Add(new Combination(rule));
            }
            //Console.WriteLine("000"+ string.Join("", plants.Select(p => (p) ? "1" : ".")));
            for (int j = 0; j < 20; j++)
            {
                bool[] nextPlants = new bool[plants.Length];
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
                //Console.WriteLine((j+1).ToString().PadLeft(3) + string.Join("",nextPlants.Select(p => (p)? "1":".")));
                plants = AddEmptyPotsIfNeeded( nextPlants, ref offset);
            }

            int sum = 0;
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i])
                {
                    sum += i - offset;
                }
            }
            return sum.ToString();
        }

        private bool[] AddEmptyPotsIfNeeded(bool[] plants, ref int o)
        {
            int addToLeft = 0;
            
            if (!plants[0] && !plants[1] && !plants[2] && !plants[3])
            {//.... 0
                addToLeft = 0;
            }
            else if (!plants[0] && !plants[1] && !plants[2] && plants[3])
            {//...# 1

                addToLeft = 1;
            }
            else if (!plants[0] && !plants[1] && plants[2])
            {//..#* 2
                addToLeft = 2;
            }
            else if (!plants[0] && plants[1])
            {//.#** 3
                addToLeft = 3;
            }
            else if (plants[0])
            {//#*** 4
                addToLeft = 4;
            }

            o += addToLeft;
            
            int addToRight = 0;
            if (!plants[plants.Length-4] && !plants[plants.Length - 3] && !plants[plants.Length - 2] && !plants[plants.Length - 1])
            {//.... 0
                addToRight = 0;
            }
            else if (plants[plants.Length - 4] && !plants[plants.Length - 3] && !plants[plants.Length - 2] && !plants[plants.Length - 1])
            {//#... 1
                addToRight = 1;
            }
            else if (plants[plants.Length - 3] && !plants[plants.Length - 2] && !plants[plants.Length - 1])
            {//*#.. 2
                addToRight = 2;
            }
            else if (plants[plants.Length - 2] && !plants[plants.Length - 1])
            {//**#. 3
                addToRight = 3;
            }
            else if (plants[plants.Length - 1])
            {//***# 4
                addToRight = 4;
            }

            if (addToRight > 0 ||addToLeft > 0)
            {
                bool[] biggerPlantArray = new bool[plants.Length + addToRight + addToLeft];
                Array.Copy(plants, 0, biggerPlantArray, addToLeft, plants.Length);
                plants = biggerPlantArray;
            }

            return plants;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            //3600000002022
            int offset = 3;
            bool[] plants = new bool[input[0].Length - 15 + offset];

            for (int i = 0; i < input[0].Length - 15; i++)
            {
                plants[i + offset] = input[0][i + 15].Equals('#');
            }
            plants = AddEmptyPotsIfNeeded(plants, ref offset);

            List<Combination> combinations = new List<Combination>();

            foreach (string rule in input.Skip(2))
            {
                combinations.Add(new Combination(rule));
            }
            int numberOfPlants = plants.Count(p => p == true);
            for (int j = 0; j < 50000000000; j++)
            {
                bool[] nextPlants = new bool[plants.Length];
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
                string plantsString = string.Join("", plants.Select(p => (p) ? "1" : " ")).Trim();
                string nextPlantsString = string.Join("", nextPlants.Select(p => (p) ? "1" : " ")).Trim();
                if (plantsString.Equals(nextPlantsString))//Check if one array is a subset of the other 
                {
                    //Shift and there is your result
                    int firstPlant = plants.ToList().FindIndex(p => p) + offset;
                    int firstPlantNextGeneration = nextPlants.ToList().FindIndex(p => p) + offset;
                    long remainingIterations = 50000000000 - j;
                    
                    long sum = 0;
                    for (int i = 0; i < plants.Length; i++)
                    {
                        if (plants[i])
                        {
                            sum += i - offset + remainingIterations;
                        }
                    }
                    return sum.ToString();
                }
                plants = AddEmptyPotsIfNeeded(nextPlants, ref offset);
            }


            return "";
        }
    }
}