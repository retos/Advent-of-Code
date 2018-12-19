using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day16
{
    internal class Sample
    {
        public int[] Before { get; set; }
        public int[] Instruction { get; set; }
        public int[] After { get; set; }
        public List<OpCode> PossibleOpCodes { get; set; }

        public Sample(string before, string instruction, string after)
        {
            Before = before.Remove(before.Length - 1).Split('[')[1].Split(',').Select(int.Parse).ToArray();
            Instruction = instruction.Split(' ').Select(int.Parse).ToArray();
            After = after.Remove(before.Length - 1).Split('[')[1].Split(',').Select(int.Parse).ToArray();
            PossibleOpCodes = new List<OpCode>();
        }
    }
}