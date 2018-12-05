using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2017.Day10
{
    internal class Day10 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => false;

        public override string Part1(List<string> input)
        {
            //init loop
            int[] loop = new int[int.Parse(input[1])];
            for (int i = 0; i < loop.Length; i++)
            {
                loop[i] = i;
            }
            int currentPosition = 0;
            int skipSize = 0;
            List<int> instructions = input[0].Split(',').Select(s => int.Parse(s)).ToList();

            foreach (int instruction in instructions)
            {
                //TODO Modulo
                //1000 / 90 = 11.1111
                //1000 % 90 = 10
                //11 * 90 = 990

                //invert from currentPostion to instruction

                //TODO Modulo!!!
                for (int i = 0; i < instruction/2; i++) // mit /2 wird erreicht das man nur die hälfte der punkte durchwandert
                {
                    int index = (currentPosition + i) % loop.Length;//endposition berechnen indem man über die loop-grenze hinaus kann
                    int switchIndex = (currentPosition + instruction - i - 1) % loop.Length;
                    int indexValue = loop[index];
                    loop[index] = loop[switchIndex];
                    loop[switchIndex] = indexValue;
                }
                currentPosition = (currentPosition + instruction + skipSize) % loop.Length;
                skipSize++;
            }

            return $"first two positions multiplied: {loop[0] * loop[1]}";
        }

        public override string Part2(List<string> input)
        {
            //init loop
            int[] loop = new int[int.Parse(input[1])];
            for (int i = 0; i < loop.Length; i++)
            {
                loop[i] = i;
            }
            int currentPosition = 0;
            int skipSize = 0;
            List<int> instructions = input[0].ToList().Select(s => (int)s).ToList();
            instructions.Add(17);//17,31,73,47,23
            instructions.Add(31);
            instructions.Add(73);
            instructions.Add(47);
            instructions.Add(23);

            for (int j = 0; j < 65; j++)
            {
                foreach (int instruction in instructions)
                {
                    for (int i = 0; i < instruction / 2; i++) // mit /2 wird erreicht das man nur die hälfte der punkte durchwandert
                    {
                        int index = (currentPosition + i) % loop.Length;//endposition berechnen indem man über die loop-grenze hinaus kann
                        int switchIndex = (currentPosition + instruction - i - 1) % loop.Length;
                        int indexValue = loop[index];
                        loop[index] = loop[switchIndex];
                        loop[switchIndex] = indexValue;
                    }
                    currentPosition = (currentPosition + instruction + skipSize) % loop.Length;
                    skipSize++;
                }
            }

            List<int> denseHash = new List<int>();
            try
            {
                for (int i = 0; i < 16; i++)
                {
                    DataTable dt = new DataTable();
                    string xor = string.Join(" ^ ", loop.ToList().GetRange(0, 16));
                    int denseH = (int)dt.Compute(xor, "");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return $"first two positions multiplied: {loop[0] * loop[1]}";
        }
    }
}