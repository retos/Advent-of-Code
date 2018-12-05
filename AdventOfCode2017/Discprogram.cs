using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Discprogram
    {
        string originalMessage;
        public string Name
        {
            get
            {
                return originalMessage.Split(' ')[0];
            }
        }
        public int Weight
        {
            get
            {
                return int.Parse(originalMessage.Split(' ')[1].Trim('(', ')'));
            }
        }

        public int WeightIncludingChildren
        {
            get
            {
                return Weight + Children.Sum(c => c.WeightIncludingChildren);                
            }
        }


        public List<string> ChildrensNames
        {
            get
            {
                int startChildrensNames = originalMessage.IndexOf(" -> ") + 4;
                if (startChildrensNames < 9)
                {
                    return new List<string>();
                }
                return  originalMessage.Substring(startChildrensNames).Split(',').Select(n => n.Trim()).ToList();
            }
        }

        public Discprogram Parent { get; set; }
        public List<Discprogram> Children { get; set; }

        public Discprogram(string line)
        {
            originalMessage = line;
            Children = new List<Discprogram>();
        }

        public void AddChildren(Discprogram child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        internal void CheckWeight()
        {
            foreach (Discprogram child in Children)
            {
                child.CheckWeight();                
            }

            if (Children.Count > 0)
            {
                if (Children.Average(c => c.WeightIncludingChildren) == Children.First().WeightIncludingChildren)
                {
                    //Children are in Balance
                }
                else
                {
                    Dictionary<int, int> keys = new Dictionary<int, int>();

                    foreach (Discprogram child in Children)
                    {
                        if (keys.ContainsKey(child.WeightIncludingChildren))
                        {
                            keys[child.WeightIncludingChildren]++;
                        }
                        else
                        {
                            keys.Add(child.WeightIncludingChildren, 1);
                        }
                    }
                    int wrongWeight = keys.Where(k => k.Value == 1).Single().Key;
                    int desiredWeight = keys.Where(k => k.Value != 1).First().Key;
                    Discprogram wrongProgram = Children.Where(c => c.WeightIncludingChildren == wrongWeight).Single();
                    int newWeightOfChild = wrongProgram.Weight + desiredWeight - wrongWeight;

                    Console.WriteLine($"Not Balanced: {this.Name}, childname: {wrongProgram.Name} new weight: {newWeightOfChild}");
                }
            }
        }
    }
}