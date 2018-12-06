using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day04
{
    internal class Day04 : DayBase
    {
        public override string Title => "--- Day 4: Repose Record ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Guard> guards = new List<Guard>();
            string lastGuardId = string.Empty;
            foreach (string s in input.OrderBy(i => i).ToList())
            {
                string[] splitted = s.Split(' ');
                DateTime date = DateTime.Parse(s.Split(']')[0].Remove(0, 1));
                Guard lastGuard = guards.Where(g => g.Id == lastGuardId).FirstOrDefault();

                switch (s.Substring(19))
                {
                    case "wakes up":
                        if (lastGuard != null)
                        {
                            Shift lastShift = lastGuard.Shifts.OrderByDescending(shift => shift.Start).FirstOrDefault();
                            if (lastShift != null)
                            {
                                lastShift.WakeupTimes.Add(date);

                                DateTime lastSleep = lastShift.SleepTimes.Last();

                                TimeSpan span = date.Subtract(lastSleep);
                                lastShift.SleepMinutes += (int)span.TotalMinutes;

                                for (int m = lastSleep.Minute; m < date.Minute; m++)
                                {
                                    lastShift.SleepPerMinuteCount[m]++;
                                }
                            }
                        }
                        break;
                    case "falls asleep":
                        if (lastGuard != null)
                        {
                            Shift lastShift = lastGuard.Shifts.OrderByDescending(shift => shift.Start).FirstOrDefault();
                            if (lastShift != null)
                            {
                                lastShift.SleepTimes.Add(date);
                            }
                        }
                        break;
                    default:
                        //Begins shift
                        lastGuardId = s.Substring(19).Split(' ')[1];
                        Guard guard = guards.Where(g => g.Id == lastGuardId).FirstOrDefault();
                        if (guard == null)
                        {
                            guard = new Guard(lastGuardId);
                            guards.Add(guard);
                        }

                        guard.Shifts.Add(new Shift(date));

                        break;
                }

            }

            Guard sleepyGuard = guards.OrderByDescending(g => g.SleepMinutes).First();
            int mostSleepInMinute = sleepyGuard.MostSleepyMinute;
            int sleepyId = int.Parse(sleepyGuard.Id.Substring(1));

            int answer = sleepyId * mostSleepInMinute;

            return $"answer: {answer}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<Guard> guards = new List<Guard>();
            string lastGuardId = string.Empty;
            foreach (string s in input.OrderBy(i => i).ToList())
            {
                string[] splitted = s.Split(' ');
                DateTime date = DateTime.Parse(s.Split(']')[0].Remove(0, 1));
                Guard lastGuard = guards.Where(g => g.Id == lastGuardId).FirstOrDefault();

                switch (s.Substring(19))
                {
                    case "wakes up":
                        if (lastGuard != null)
                        {
                            Shift lastShift = lastGuard.Shifts.OrderByDescending(shift => shift.Start).FirstOrDefault();
                            if (lastShift != null)
                            {
                                lastShift.WakeupTimes.Add(date);

                                DateTime lastSleep = lastShift.SleepTimes.Last();

                                TimeSpan span = date.Subtract(lastSleep);
                                lastShift.SleepMinutes += (int)span.TotalMinutes;

                                for (int m = lastSleep.Minute; m < date.Minute; m++)
                                {
                                    lastShift.SleepPerMinuteCount[m]++;
                                }
                            }
                        }
                        break;
                    case "falls asleep":
                        if (lastGuard != null)
                        {
                            Shift lastShift = lastGuard.Shifts.OrderByDescending(shift => shift.Start).FirstOrDefault();
                            if (lastShift != null)
                            {
                                lastShift.SleepTimes.Add(date);
                            }
                        }
                        break;
                    default:
                        //Begins shift
                        lastGuardId = s.Substring(19).Split(' ')[1];
                        Guard guard = guards.Where(g => g.Id == lastGuardId).FirstOrDefault();
                        if (guard == null)
                        {
                            guard = new Guard(lastGuardId);
                            guards.Add(guard);
                        }

                        guard.Shifts.Add(new Shift(date));

                        break;
                }

            }
            
            Guard sleepRecordholder = guards.OrderByDescending(g => g.SleepPerMinuteCount.Max()).First();
            int answer = int.Parse(sleepRecordholder.Id.Substring(1)) * sleepRecordholder.MostSleepyMinute;
            return $"answer: {answer}";
        }
    }
}