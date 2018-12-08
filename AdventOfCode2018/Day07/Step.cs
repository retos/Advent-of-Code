using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day07
{
    internal class Step
    {
        public Step()
        {
            PreConditionSteps = new List<Step>();
        }

        public char Name { get; internal set; }
        public List<Step> PreConditionSteps { get; set; }
        public bool Taken { get; internal set; }
        public bool Underprogress { get; internal set; }

        internal int Duration(bool isTestRun)
        {
            if (isTestRun)
            {
                return (int)Name - 64;
            }
            else
            {
                return (int)Name - 4;
            }
        }
    }
}