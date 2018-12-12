using System;
using System.Collections.Generic;

namespace AdventOfCode2018.Day12
{
    internal class Combination
    {
        public bool[] Configuration { get; set; }
        public bool LeadsToPlant { get; set; }

        public Combination(string rule)
        {
            Configuration = new bool[5];
            for (int i = 0; i < 5; i++)
            {
                Configuration[i] = rule[i].Equals('#');
            }

            LeadsToPlant = rule[9].Equals('#');            
        }
    }
}