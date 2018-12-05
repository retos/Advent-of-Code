using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day05
    {
        internal static void CalculatePart1()
        {
            //Alternatives lesen vom File:
            //int[] Parse(string input) => ReadAllLines(input).Select(int.Parse).ToArray();
            

            StreamReader reader = new StreamReader("input05.txt");

            int numberOfSteps = 0;
            List<int> instructions = new List<int>();
            int currentPostion = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                instructions.Add(int.Parse(line));
            }

            do
            {
                //1. read instruction
                int currentJumpInstruction = instructions[currentPostion];

                //2. update current position
                instructions[currentPostion] = instructions[currentPostion] + 1;

                //3. execute instruction
                currentPostion = currentPostion + currentJumpInstruction;
                numberOfSteps++;

            } while (instructions.Count > currentPostion && currentPostion >= 0);

            Console.WriteLine(numberOfSteps);
        }

        internal static void CalculatePart2()
        {
            StreamReader reader = new StreamReader("input05.txt");

            int numberOfSteps = 0;
            List<int> instructions = new List<int>();
            int currentPostion = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                instructions.Add(int.Parse(line));
            }

            do
            {
                //1. read instruction
                int currentJumpInstruction = instructions[currentPostion];

                //2. update current position
                //if (Math.Abs(currentJumpInstruction) >= 3)
                if (currentJumpInstruction >= 3)
                {
                    instructions[currentPostion] = instructions[currentPostion] - 1;
                }
                else
                {
                    instructions[currentPostion] = instructions[currentPostion] + 1;
                }

                //3. execute instruction
                currentPostion = currentPostion + currentJumpInstruction;
                numberOfSteps++;

            } while (instructions.Count > currentPostion && currentPostion >= 0);

            Console.WriteLine(numberOfSteps);
        }

        public static void PartTwo()
        {
            string[] lines = File.ReadAllLines("input05Test.txt");
            int[] instructions = lines.Select(x => Convert.ToInt32(x)).ToArray();
            int position = 0;
            int numberOfSteps = 0;
            int currentInstruction = 0;
            while (position >= 0 && position < instructions.Length)
            {
                currentInstruction = instructions[position];
                instructions[position] += currentInstruction > 2 ? -1 : 1;
                position += currentInstruction;
                numberOfSteps++;
            }
            Console.WriteLine(numberOfSteps);
        }
    }
}