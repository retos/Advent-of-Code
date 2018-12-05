using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day05
{
    internal class Day05 : DayBase
    {
        public override string Title => "--- Day 5: Alchemical Reduction ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input)
        {
            string reducedPolymer = RunReductionPart1(input[0]);
            return reducedPolymer.Length.ToString(); 
        }

        public override string Part2(List<string> input)
        {
            List<KeyValuePair<char, string>> reductionList = new List<KeyValuePair<char, string>>();
            string polymer = input[0];

            foreach (char c in polymer.ToList())
            {
                if (!reductionList.Any(r => r.Key == char.ToLower(c)))
                {
                    string reducedVariant = polymer.Replace(Char.ToLower(c).ToString(), "").Replace(Char.ToUpper(c).ToString(), "");
                    KeyValuePair<char, string> redu = new  KeyValuePair<char, string>(char.ToLower(c), RunReductionPart1(reducedVariant));
                    reductionList.Add(redu);
                }
            }
        
            string shortest = reductionList.OrderBy(r => r.Value.Length).First().Value;
            return shortest.Length.ToString();//Ohne Threading hat es 6 Minuten auf meiner Maschine
        }

        private string RunReductionPart1(string toReduce)
        {
            string lastPolymer = toReduce;
            string reducedPolymer = lastPolymer;
            do
            {
                lastPolymer = reducedPolymer;
                List<char> letters = reducedPolymer.ToList();
                for (int i = 0; i < letters.Count - 1; i++)
                {
                    if (Char.ToUpper(letters[i]) == Char.ToUpper(letters[i + 1]) && letters[i] != letters[i + 1])
                    {
                        string subToRemove = reducedPolymer.Substring(i, 2);
                        reducedPolymer = reducedPolymer.Replace(subToRemove, "");
                        break;
                    }
                }

            } while (lastPolymer.Length > reducedPolymer.Length || string.IsNullOrEmpty(reducedPolymer));

            return reducedPolymer;
        }
    }
}