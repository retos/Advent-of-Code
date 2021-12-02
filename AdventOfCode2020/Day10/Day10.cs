using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day10
{
    internal class Day10 : DayBase
    {
        public override string Title => "--- Day 10: Adapter Array ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<uint> numbers = input.Select(uint.Parse).OrderBy(n => n).ToList();
            uint lastJoltage = 0;
            uint builtinJoltage = numbers.Max(n => n) +3;
            numbers.Add(builtinJoltage);
            int numberOf1Joltage = 0;
            int numberOf3Joltage = 0;
            foreach (uint adapter in numbers)
            {
                uint diff = adapter - lastJoltage;
                if (diff==1)
                {
                    numberOf1Joltage++;
                }
                else if(diff == 3)
                {
                    numberOf3Joltage++;
                }
                lastJoltage = adapter;
            }

            return $"answer: {numberOf1Joltage * numberOf3Joltage}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<uint> numbers = input.Select(uint.Parse).ToList();
            List<Adapters> adapters = numbers.Select(n => new Adapters() { Joltage = n }).ToList(); ;
            uint builtinJoltage = numbers.Max(n => n) + 3;
            adapters.Add(new Adapters() { Joltage = 0 });
            adapters.Add(new Adapters() { Joltage = builtinJoltage });
            adapters = adapters.OrderBy(a => a.Joltage).ToList();


            ulong arrangements = FindNumberOfArrangements(ref adapters, 0, string.Empty);

            return $"possible arrangements: {arrangements}";
        }

        private static ulong FindNumberOfArrangements(ref List<Adapters> adapters, int startindex, string path)
        {
            if (adapters[startindex].NumberOfArrangementsFromHere != null)
            {
                Console.WriteLine($"{string.Join(", ", path)}, {adapters[startindex].Joltage} Number of arrangements from here: {adapters[startindex].NumberOfArrangementsFromHere}");
                return (ulong)adapters[startindex].NumberOfArrangementsFromHere;
            }
            path += ", " + adapters[startindex].Joltage;//path.Add(adapters[startindex]);

            if (adapters.Count - 1 == startindex)
            {
                Console.WriteLine(string.Join(", ", path));
                return 1; //reached the device, count this path
            }
            ulong arrangements = 0;

            uint lastJoltage = adapters[startindex].Joltage;
            //assuming there are no duplicate adapters
            List<Adapters> possibleNextAdapters = adapters.Where(a => a.Joltage - lastJoltage >= 1 && a.Joltage - lastJoltage <= 3 && a.Joltage != lastJoltage).ToList();
            foreach (Adapters adapter in possibleNextAdapters)
            {
                ulong numberOfArrangements = FindNumberOfArrangements(ref adapters, adapters.IndexOf(adapter), path);
                arrangements += numberOfArrangements;
            }

            adapters[startindex].NumberOfArrangementsFromHere = arrangements;

            return arrangements;
        }
    }
}
