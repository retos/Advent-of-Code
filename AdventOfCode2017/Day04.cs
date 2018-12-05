using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day04
    {
        internal static void CalculatePart1()
        {
            StreamReader reader = new StreamReader("input04.txt");

            int numberOfValidPhrases = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> phrases = line.Split(' ').ToList();
                bool noDuplicates = phrases.Count() == phrases.Distinct().Count();
                if (noDuplicates)
                {
                    numberOfValidPhrases++;
                }
            }

            Console.WriteLine(numberOfValidPhrases);
        }

        internal static void CalculatePart2()
        {
            StreamReader reader = new StreamReader("input04.txt");

            int numberOfValidPhrases = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> phrases = line.Split(' ').ToList();

                bool noAnagrams = phrases.Count() == phrases.Select(p => String.Concat(p.OrderBy(c => c))).Distinct().Count();

                if (noAnagrams)
                {
                    numberOfValidPhrases++;
                }
            }

            Console.WriteLine(numberOfValidPhrases);
        }


    }
}