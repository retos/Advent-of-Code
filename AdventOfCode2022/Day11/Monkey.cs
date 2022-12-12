using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day11
{
    public class Monkey
    {
        private ulong number;
        private List<Monkey> monkeyList;

        enum Operand
        {
            Multiply,
            Add
        }

        public Monkey(ulong number, List<Monkey> monkeys)
        {
            this.Number = number;
            monkeyList= monkeys;
            InspectedItems = 0;
        }
        private Operand? operand = null;
        public List<ulong> Items { get; internal set; }
        
        public string OperationString { get; internal set; }
        public ulong TrueMonkey { get; internal set; }
        public ulong FalseMonkey { get; internal set; }
        public ulong Test { get; internal set; }
        public ulong Number { get => number; set => number = value; }
        public ulong InspectedItems { get; set; }
        public Func<ulong, ulong> Operation { get; set; }

        internal void InspectWorryLevel(bool reduceWorrielevel = true)
        {
            List<long> toRemove = new List<long>();
            for (int i = 0; i < Items.Count; i++)
            {
                ulong newWorrieLevl = CalculateNewWorrieLevel(reduceWorrielevel, i);
                Items[i] = newWorrieLevl;

                //Test
                Monkey targetMonkey = null;
                if (newWorrieLevl % Test == 0)
                {
                    targetMonkey = monkeyList.Where(m => m.Number == TrueMonkey).Single();
                }
                else
                {
                    targetMonkey = monkeyList.Where(m => m.Number == FalseMonkey).Single();
                }
                //Console.Write($"{targetMonkey.Number} ");
                targetMonkey.Items.Add(Items[i]);
                toRemove.Add(i);
                InspectedItems++;
            }
            foreach (var r in toRemove.OrderByDescending(x => x).ToList())
            {
                Items.RemoveAt((int)r);
            }
        }

        private ulong CalculateNewWorrieLevel(bool reduceWorrielevel, int i)
        {
            if (reduceWorrielevel)
            {
                //worked fine for part 1, but not for part 2 (overflow :-)
                DataTable dt = new DataTable();
                string computeString = OperationString.Replace("old", Items[i].ToString());
                int newWorrieLevel = (int)dt.Compute(computeString, "") / 3;
                return (ulong) newWorrieLevel;
            }
            else
            {
                //mod ist die multiplikation aller .Test Werte, Somit muss das WorrieLevel nie höher sein als das
                //mod = monkeyList[0].Test * monkeyList[1].Test * monkeyList[2].Test monkeyList[3].Test ...
                var mod = monkeyList.Aggregate(1, (mod, monkey) => mod * (int)monkey.Test);
                ulong newWorrieLevel = Operation(Items[i]) % (ulong) mod;
                return newWorrieLevel;
            }
        }
    }
}
