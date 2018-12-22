using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day19{
    internal class Day19 : DayBase
    {
        public override string Title => "--- Day 19: Go With The Flow ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            int ip = int.Parse(input[0].Split(' ')[1]);
            List<Instruction> instructions = new List<Instruction>();
            int[] register = new int[6];

            foreach (string line in input.Skip(1))
            {
                instructions.Add(new Instruction(line));
            }

            do
            {
                register = GetResult(register, instructions[ip], ref ip);

            } while (ip<instructions.Count && ip>=0);
 


            return register[0].ToString();
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
        private int[] GetResult(int[] reg, Instruction instruction, ref int ip)
        {
            //1. update register with value "ip" with the value of "ip"
            reg[0] = ip;
            //2. excecute opcode
            switch (instruction.OpCode)
            {
                case OpCode.addr:
                    reg[instruction.C] = reg[instruction.A] + reg[instruction.B];
                    break;
                case OpCode.addi:
                    reg[instruction.C] = reg[instruction.A] + instruction.B;
                    break;
                case OpCode.mulr:
                    reg[instruction.C] = reg[instruction.A] * reg[instruction.B];
                    break;
                case OpCode.muli:
                    reg[instruction.C] = reg[instruction.A] * instruction.B;
                    break;
                case OpCode.banr:
                    reg[instruction.C] = reg[instruction.A] & reg[instruction.B];
                    break;
                case OpCode.bani:
                    reg[instruction.C] = reg[instruction.A] & instruction.B;
                    break;
                case OpCode.borr:
                    reg[instruction.C] = reg[instruction.A] | reg[instruction.B];
                    break;
                case OpCode.bori:
                    reg[instruction.C] = reg[instruction.A] | instruction.B;
                    break;
                case OpCode.setr:
                    reg[instruction.C] = reg[instruction.A];
                    break;
                case OpCode.seti:
                    reg[instruction.C] = instruction.A;
                    break;
                case OpCode.gtir:
                    reg[instruction.C] = (instruction.A > reg[instruction.B]) ? 1 : 0;
                    break;
                case OpCode.gtri:
                    reg[instruction.C] = (reg[instruction.A] > instruction.B) ? 1 : 0;
                    break;
                case OpCode.gtrr:
                    reg[instruction.C] = (reg[instruction.A] > reg[instruction.B]) ? 1 : 0;
                    break;
                case OpCode.eqir:
                    reg[instruction.C] = (instruction.A == reg[instruction.B]) ? 1 : 0;
                    break;
                case OpCode.eqri:
                    reg[instruction.C] = (reg[instruction.A] == instruction.B) ? 1 : 0;
                    break;
                case OpCode.eqrr:
                    reg[instruction.C] = (reg[instruction.A] == reg[instruction.B]) ? 1 : 0;
                    break;
                default:
                    throw new ArgumentException("Unknown OpCode");
            }

            //3. write value of reg[ip] to ip
            ip = reg[0];
            ip++;
            return reg;
        }
    }
}