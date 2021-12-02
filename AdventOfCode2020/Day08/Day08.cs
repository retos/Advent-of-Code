using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day08
{
    internal class Day08 : DayBase
    {
        public override string Title => "--- Day 8: Handheld Halting ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            List<Instruction> bootCode = Instruction.Convert(input);
            bool toUseTheSameMethodImUsingAUnusedBoolHere = false;
            int result = Run(bootCode, ref toUseTheSameMethodImUsingAUnusedBoolHere);

            return $"the value of the accumulator is: {result}";
        }

        private int Run(List<Instruction> bootCode, ref bool worked)
        {
            int nextOperation = 0;
            int accumulator = 0;
            do
            {
                bootCode[nextOperation].HasExecuted = true;

                switch (bootCode[nextOperation].Operation)
                {
                    //acc increases or decreases a single global value called the accumulator
                    // by the value given in the argument. For example, acc +7 would increase
                    // the accumulator by 7. The accumulator starts at 0. After an acc instruction,
                    // the instruction immediately below it is executed next.
                    case "acc":
                        accumulator = accumulator + bootCode[nextOperation].Argument;
                        nextOperation++;
                        break;

                    //jmp jumps to a new instruction relative to itself.The next instruction to
                    // execute is found using the argument as an offset from the jmp instruction;
                    // for example, jmp + 2 would skip the next instruction, jmp + 1 would continue
                    // to the instruction immediately below it, and jmp - 20 would cause the
                    // instruction 20 lines above to be executed next.
                    case "jmp":
                        nextOperation = nextOperation + bootCode[nextOperation].Argument;
                        break;

                    //nop stands for No OPeration -it does nothing.The instruction immediately
                    // below it is executed next.
                    case "nop":
                        nextOperation++;
                        break;
                    default:
                        break;
                }

                //This is for part2. Check if boot was sucessfull
                if (nextOperation > bootCode.Count() - 1)
                {
                    worked = true;
                    return accumulator;
                }

            } while (!bootCode[nextOperation].HasExecuted);

            return accumulator;
        }
        public override string Part2(List<string> input, bool isTestRun)
        {
            List<Instruction> bootCode = Instruction.Convert(input);
            bool worked = false;
            int result = 0;

            for (int i = 0; i < bootCode.Count; i++)
            {
                //reset the HasExecuted before running
                bootCode.ForEach(c => c.HasExecuted = false);
                //Tamper with current instruction if jmp or nop, and test it
                if (bootCode[i].Operation.Equals("jmp"))
                {
                    bootCode[i].Operation = "nop";
                    result = Run(bootCode, ref worked);
                    bootCode[i].Operation = "jmp";
                }
                else if (bootCode[i].Operation.Equals("nop"))
                {
                    bootCode[i].Operation = "jmp";
                    result = Run(bootCode, ref worked);
                    bootCode[i].Operation = "nop";
                }

                if (worked)
                {
                    return $"the value of the accumulator is: {result}";
                }
            }
            return $"could not find a solution.";
        }
    }
}
