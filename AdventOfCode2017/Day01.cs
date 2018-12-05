using System;

namespace AdventOfCode2017
{
    internal class Day01
    {
        internal static void Calculate(string input)
        {
            int sum = 0;
            Char lastChar = input[input.Length -1]; //initiieren mit dem letzen Buchstaben damit es den ersten mit dem letzten vergleicht.
            foreach (char c in input)
            {
                if (c == lastChar)
                {
                    sum += c - '0';//c-'0' konvertiert einen Char zu einem int
                }
                lastChar = c;
            }

            Console.WriteLine(sum);                        
        }

        internal static void CalculateSecond(string input)
        {
            int sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                //finding position to compare
                int compareToPosition = (i + (input.Length / 2)) % input.Length;
                char compareTo = input[compareToPosition];
                if (input[i] == compareTo)
                {
                    sum += input[i] - '0';//c-'0' konvertiert einen Char zu einem int
                }
            }

            Console.WriteLine(sum);
        }
    }
}