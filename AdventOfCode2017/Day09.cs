using System;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day09
    {
        internal static void CalculatePart1()
        {
            StreamReader reader = new StreamReader("input09.txt");

            string line = reader.ReadLine();
            StreamGroup g = new StreamGroup(line);
            Console.WriteLine(g.Score);
        }

        internal static void CalculatePart2()
        {
            StreamReader reader = new StreamReader("input09.txt");

            string line = reader.ReadLine();
            StreamGroup g = new StreamGroup(line);
            Console.WriteLine(g.NoneCanceledCharacterCount);
        }
    }
}