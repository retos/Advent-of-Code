using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day01
{
    internal class Day01 : DayBase
    {
        public override string Title => "--- Day 1: Report Repair ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<int> numbers = input.Select(int.Parse).ToList();

            foreach (int firstValue in numbers)
            {
                foreach (int secondValue in numbers)
                {                    
                    if (firstValue + secondValue == 2020)
                    {
                        return $"{firstValue} * {secondValue} = {firstValue * secondValue}";
                    }                    
                }
            }

            return $"not done anything";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<int> numbers = input.Select(int.Parse).ToList();

            foreach (int firstValue in numbers)
            {
                foreach (int secondValue in numbers)
                {
                    foreach (int thirdValue in numbers)
                    {
                        if (firstValue + secondValue + thirdValue == 2020)
                        {
                            return $"{firstValue} * {secondValue} * {thirdValue} = {firstValue * secondValue * thirdValue}";
                        }
                    }
                }
            }

            return $"not done anything";

        }
    }
}
