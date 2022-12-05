using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day05
{
    public class Instruction
    {
        internal string instruction;

        public Instruction(string input)
        {
            this.instruction = input;
            string[] parts = input.Split(' ');
            Amount = int.Parse(parts[1]);
            MoveFrom = int.Parse(parts[3]);
            MoveTo = int.Parse(parts[5]);
        }

        public int Amount { get; set; }
        public int MoveFrom { get; set; }
        public int MoveTo { get; set; }
    }
}
