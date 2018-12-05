using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace AdventOfCode2018.Day01
{
    internal class Day01 : DayBase
    {
        public override string Title => "--- Day 1: Chronal Calibration ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input)
        {
            int frequency = 0;

            foreach (string line in input)
            {
                DataTable dt = new DataTable();
                frequency = (int)dt.Compute($"{frequency} {line}", "");
            }

            return $"Last frequency: {frequency}";
        }

        public override string Part2(List<string> input)
        {
            int frequency = 0;
            List<string> frequencyHistory = new List<string>();

            while (true)
            {
                foreach (string line in input)
                {
                    DataTable dt = new DataTable();
                    frequency = (int)dt.Compute($"{frequency} {line}", "");
                    if (frequencyHistory.Contains(frequency.ToString()))
                    {
                        return $"First frequency reached twice: {frequency}";
                    }
                    frequencyHistory.Add(frequency.ToString());
                }
            }
        }
    }
}