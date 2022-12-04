using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day04
{
    public class Elv
    {
        string input = string.Empty;
        long startA;
        long endA;
        long startB;
        long endB;
        public Elv(string input)
        {
            this.input = input;
            long[] parts = input.Split(',')
                .SelectMany(p => p.Split('-'))
                .Select(x => long.Parse(x))
                .ToArray();

            startA = parts[0];
            endA = parts[1];
            startB = parts[2];
            endB = parts[3];
        }

        public bool AnyOverlap
        {
            get
            {
                if (OneContainsTheOther)
                {
                    return true;
                }
                if (startA <= startB && endA >= startB)
                {
                    return true;
                }
                else if (startA <= endB && endA >= endB)
                {
                    return true;
                }
                return false;
            }
        }
        public bool OneContainsTheOther 
        { 
            get 
            {
                if (startA <= startB && endA >= endB)
                {
                    return true;
                }
                else if(startA >= startB && endA <= endB)
                {
                    return true;
                }
                return false;
            }  
        }
    }
}
