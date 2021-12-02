using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day07
{
    internal class Bag
    {
        public string Name { get; private set; }
        public List<Bag> Contains { get; set; }
        public bool? HasShinySubBag { get; private set; }
        private int? numberOfIndividualSubBagsIncludingSelf;
        public int NumberOfIndividualSubBagsIncludingSelf 
        {
            get
            {
                if (numberOfIndividualSubBagsIncludingSelf != null)
                {
                    return (int)numberOfIndividualSubBagsIncludingSelf;
                }
                else if (Contains == null)
                {
                    numberOfIndividualSubBagsIncludingSelf = 1;
                }
                else
                {
                    int sumOfSubBags = Contains.Sum(s => s.NumberOfIndividualSubBagsIncludingSelf);
                    numberOfIndividualSubBagsIncludingSelf= sumOfSubBags + 1;
                }
                
                return (int)numberOfIndividualSubBagsIncludingSelf;
            }
        }

        public Bag()
        {
            Contains = new List<Bag>();
        }

        internal List<Bag> Convert(List<string> input)
        {
            List<Bag> rules = new List<Bag>();

            foreach (string line in input)
            {
                string[] splitted = line.Split(" contain ");
                string name = splitted[0].Substring(0, splitted[0].Length - 5); //remove " bags" at the end
                Bag rule = rules.Find(b => b.Name.Equals(name));
                if (rule == null) 
                {
                    rule = new Bag();
                    rule.Name = name;
                    rules.Add(rule);
                }


                splitted = splitted[1].Split(", ");
                foreach (string subBag in splitted)
                {
                    if (subBag.Equals("no other bags."))
                    {
                        break;
                    }

                    int amount = int.Parse(subBag.Substring(0, 1));//assuming there are only one-digit numbers
                    name = subBag.Remove(0, 2).Replace(" bags", "").Replace(" bag", "").Replace(".", "");
                    Bag subRule = rules.Find(b => b.Name.Equals(name));
                    if (subRule == null) 
                    {
                        subRule = new Bag();
                        subRule.Name = name;
                        rules.Add(subRule);
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        rule.Contains.Add(subRule);
                    }

                }

            }

            return rules;
        }

        internal bool HasSubBag(string name)
        {
            if (HasShinySubBag != null)
            {
                return (bool)HasShinySubBag;
            }
            if (this.Contains.Any(s => s.Name.Equals(name)))
            {
                return true;
            }
            else
            {
                foreach (Bag subBag in Contains)
                {
                    if(subBag.HasSubBag(name))
                    {
                        return true;
                    }
                }
            }
            this.HasShinySubBag = false;
            return false;
        }
    }
}