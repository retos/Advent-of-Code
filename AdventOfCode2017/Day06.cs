using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day06
    {
        internal static void CalculatePart1()
        {
            StreamReader reader = new StreamReader("input06.txt");

            string line = reader.ReadLine();
            List<int> memoryBanks = line.Split('\t').Select(Int32.Parse).ToList();

            List<string> memoryBankHistory = new List<string>();

            do
            {
                //Adding the current version to the history
                memoryBankHistory.Add(string.Join("_", memoryBanks));

                //Finding the largest (Or first, of them)
                int currentMax = memoryBanks.Max();
                int indexOfCurrentMax = memoryBanks.IndexOf(currentMax);
                int redistributionValue = currentMax / (memoryBanks.Count - 1);
                redistributionValue = (redistributionValue < 1) ? 1 : redistributionValue;
                int rest = (currentMax < memoryBanks.Count)? 0 : currentMax % (memoryBanks.Count - 1);

                //Redistributing
                for (int i = 1; i < memoryBanks.Count && i <= currentMax; i++)
                {
                    //Offset by current max AND modulo to start over if end of memory is reached
                    int currentPosition = (i + indexOfCurrentMax) % (memoryBanks.Count);
                    memoryBanks[currentPosition] = memoryBanks[currentPosition] + redistributionValue;
                }

                memoryBanks[indexOfCurrentMax] = rest;

            } while (!memoryBankHistory.Contains(string.Join("_", memoryBanks)));

            Console.WriteLine(memoryBankHistory.Count());
        }

        internal static void CalculatePart2()
        {
            StreamReader reader = new StreamReader("input06.txt");

            string line = reader.ReadLine();
            List<int> memoryBanks = line.Split('\t').Select(Int32.Parse).ToList();

            List<string> memoryBankHistory = new List<string>();

            do
            {
                //Adding the current version to the history
                memoryBankHistory.Add(string.Join("_", memoryBanks));

                //Finding the largest (Or first, of them)
                int currentMax = memoryBanks.Max();
                int indexOfCurrentMax = memoryBanks.IndexOf(currentMax);
                int redistributionValue = currentMax / (memoryBanks.Count - 1);
                redistributionValue = (redistributionValue < 1) ? 1 : redistributionValue;
                int rest = (currentMax < memoryBanks.Count) ? 0 : currentMax % (memoryBanks.Count - 1);

                //Redistributing
                for (int i = 1; i < memoryBanks.Count && i <= currentMax; i++)
                {
                    //Offset by current max AND modulo to start over if end of memory is reached
                    int currentPosition = (i + indexOfCurrentMax) % (memoryBanks.Count);
                    memoryBanks[currentPosition] = memoryBanks[currentPosition] + redistributionValue;
                }

                memoryBanks[indexOfCurrentMax] = rest;

            } while (!memoryBankHistory.Contains(string.Join("_", memoryBanks)));

            Console.WriteLine(memoryBankHistory.Count() - memoryBankHistory.IndexOf(string.Join("_", memoryBanks)));
        }
    }
}