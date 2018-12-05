using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day02
    {
        internal static void Calculate()
        {
            StreamReader reader = new StreamReader("input02.txt");

            int sumOfLineDiff = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<int> numbers = line.Split('\t').Select(Int32.Parse).ToList();
                int lineDiff = numbers.Max() - numbers.Min();

                sumOfLineDiff += lineDiff;
            }

            Console.WriteLine(sumOfLineDiff);
        }

        internal static void CalculateSecond()
        {
            StreamReader reader = new StreamReader("input02.txt");

            int sumOfLineDivisions = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<int> numbers = line.Split('\t').Select(Int32.Parse).ToList();

                for (int i = 0; i < numbers.Count; i++)
                {
                    for (int j = 0; j < numbers.Count; j++)
                    {
                        if (i!= j)
                        {
                            int valueOne = numbers[i];
                            int valueTwo = numbers[j];
                            if (valueOne % valueTwo == 0)
                            {
                                sumOfLineDivisions += valueOne / valueTwo;
                            };
                            
                        }
                    }
                }
            }

            Console.WriteLine(sumOfLineDivisions);
        }
    }
}