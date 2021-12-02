using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day08
{
    internal class Instruction
    {
        private int accumulator;
        public string Operation { get; set; }
        public int Argument { get; private set; }
        public bool HasExecuted { get; set; }

        public Instruction(ref int accu)
        {
            accumulator = accu;
        }

        internal static List<Instruction> Convert(List<string> input)
        {
            List<Instruction> instructions = new List<Instruction>();
            int accu = 0;

            foreach (string line in input)
            {
                Instruction instruction = new Instruction(ref accu);

                string[] splitted = line.Split(" ");
                instruction.Operation = splitted[0];
                instruction.Argument = int.Parse(splitted[1]);
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}