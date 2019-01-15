using System;

namespace AdventOfCode2018.Day19
{
    internal class Instruction
    {
        public OpCode OpCode { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public Instruction(string line)
        {
            string[] s = line.Split(' ');
            OpCode = (OpCode)Enum.Parse(typeof(OpCode), s[0]);
            A = int.Parse(s[1]);
            B = int.Parse(s[2]);
            C = int.Parse(s[3]);
        }
        public override string ToString()
        {
            return $"{OpCode} {A} {B} {C}";
        }
    }
}