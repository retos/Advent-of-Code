using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day07
{
    internal class Day07 : DayBase
    {
        public override string Title => "--- Day 7: Handy Haversacks ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Bag> rules = new Bag().Convert(input);

            int possibleBags = 0;
            foreach (Bag bag in rules)
            {
                //not 100% clear if the top bag could be shiny gold, but I'm exluding it
                if (!bag.Name.Equals("shiny gold"))
                {
                    if (bag.HasSubBag("shiny gold"))
                    {
                        possibleBags++;
                    }
                }
            }

            return $"possible bags: {possibleBags}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            List<Bag> rules = new Bag().Convert(input);

            Bag shinyGoldBag = rules.Find(b => b.Name.Equals("shiny gold"));

            return $"individual sub bags: {shinyGoldBag.NumberOfIndividualSubBagsIncludingSelf-1}";
        }
    }
}
