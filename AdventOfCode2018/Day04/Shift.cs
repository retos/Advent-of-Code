using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day04
{
    internal class Shift
    {
        public Shift(DateTime date)
        {
            Start = date;
            SleepTimes = new List<DateTime>();
            WakeupTimes = new List<DateTime>();
            SleepPerMinuteCount = new List<int>();
            for (int i = 0; i < 60; i++)
            {
                SleepPerMinuteCount.Add(0);
            }
        }

        public List<int> SleepPerMinuteCount { get; set; }
        public DateTime Start { get; internal set; }
        public List<DateTime> SleepTimes { get; internal set; }
        public List<DateTime> WakeupTimes { get; internal set; }
        public int SleepMinutes { get; set; }
    }
}