using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day04
{
    internal class Guard
    {
        internal List<Shift> Shifts = new List<Shift>();


        public Guard(string lastGuardId)
        {
            Id = lastGuardId;
        }

        public List<int> SleepPerMinuteCount
        {
            get
            {
                List<int> sleepCount = new List<int>();
                for (int i = 0; i < 60; i++)
                {
                    sleepCount.Add(Shifts.Sum(s => s.SleepPerMinuteCount[i]));
                }
                return sleepCount;
            }
        }

        public string Id { get; internal set; }
        public int SleepMinutes
        {
            get
            {
                return Shifts.Sum(s => s.SleepMinutes);
            }
        }

        public int MostSleepyMinute
        {
            get
            {
                return this.SleepPerMinuteCount.IndexOf(SleepPerMinuteCount.Max());
            }
        }

    }
}