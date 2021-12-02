using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day09
{
    internal class Day09 : DayBase
    {
        public override string Title => "--- Day 9: Encoding Error ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int preamblelength = (isTestRun) ? 5 : 25;
            List<long> numbers = input.Select(long.Parse).ToList();

            for (int i = preamblelength; i < numbers.Count; i++)
            {
                bool isValid = false;
                List<long> preamble = numbers.Skip(i - preamblelength).Take(preamblelength).ToList();

                foreach (int n1 in preamble)
                {
                    isValid = preamble.Any(n2 => n1 + n2 == numbers[i] && n1 != n2);
                    if (isValid)
                    {
                        break;//current number is valid, end verification
                    }
                }

                if (!isValid)
                {
                    return $"invalid number found {numbers[i]}";
                }

            }

            return $"no solution found";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            int indexOfInvalidNumber;
            long invalidNumber = 0;

            int preamblelength = (isTestRun) ? 5 : 25;
            List<long> numbers = input.Select(long.Parse).ToList();

            for (int i = preamblelength; i < numbers.Count; i++)
            {
                bool isValid = false;
                List<long> preamble = numbers.Skip(i - preamblelength).Take(preamblelength).ToList();

                foreach (int n1 in preamble)
                {
                    isValid = preamble.Any(n2 => n1 + n2 == numbers[i] && n1 != n2);
                    if (isValid)
                    {
                        break;//current number is valid, end verification
                    }
                }

                if (!isValid)
                {
                    indexOfInvalidNumber = i;
                    invalidNumber = numbers[i];
                    break;
                }

            }

            //find contiguous set
            long sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum = 0;

                for (int j = i; j < numbers.Count; j++)
                {
                    sum = sum + numbers[j];
                    if (sum > invalidNumber)
                    {
                        break;
                    }
                    if (sum == invalidNumber)
                    {
                        List<long> section = numbers.GetRange(i, j - i+1);
                        return $"the encryption weakness is {section.Min() + section.Max()}";
                    }
                }
            }

            return $"no solution found";
        }
    }
}
