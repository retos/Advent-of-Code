using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day16
{
    internal class Day16 : DayBase
    {
        public override string Title => "--- Day 16: Chronal Classification ---";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {            
            List<Sample> samples = new List<Sample>();
            List<int[]> data = new List<int[]>();
            //Read samples & data
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].StartsWith("Before"))
                {
                    Sample s = new Sample(input[i++], input[i++], input[i++]);
                    samples.Add(s);
                }
                else if (!string.IsNullOrWhiteSpace(input[i]))
                {
                    data.Add(input[i].Split(' ').Select(int.Parse).ToArray());
                }
            }
            //Find possible opcode candidates
            foreach (Sample sample in samples)
            {
                sample.PossibleOpCodes = GetPossibleOpCodes(sample);               
            }

            return samples.Where(s => s.PossibleOpCodes.Count() > 2).Count().ToString();
        }

        private List<OpCode> GetPossibleOpCodes(Sample sample)
        {
            List<OpCode> possibleCodes = new List<OpCode>();
            foreach (OpCode opCode in Enum.GetValues(typeof(OpCode)))
            {
                int[] result = GetResult(opCode, sample.Before, sample.Instruction);
                if (result.SequenceEqual(sample.After))
                {
                    sample.PossibleOpCodes.Add(opCode);
                }
            }
            possibleCodes = sample.PossibleOpCodes.Distinct().ToList();
            return possibleCodes;
        }

        private int[] GetResult(OpCode opCode, int[] reg, int[] instruction)
        {
            int[] result = (int[])reg.Clone();
            int a = instruction[1];
            int b = instruction[2];
            int c = instruction[3];
            //opcode A B C
            switch (opCode)
            {
                case OpCode.addr:
                    result[c] = reg[a] + reg[b];
                    break;
                case OpCode.addi:
                    result[c] = reg[a] + b;
                    break;
                case OpCode.mulr:
                    result[c] = reg[a] * reg[b];
                    break;
                case OpCode.muli:
                    result[c] = reg[a] * b;
                    break;
                case OpCode.banr:
                    result[c] = reg[a] & reg[b];
                    break;
                case OpCode.bani:
                    result[c] = reg[a] & b;
                    break;
                case OpCode.borr:
                    result[c] = reg[a] | reg[b];
                    break;
                case OpCode.bori:
                    result[c] = reg[a] | b;
                    break;
                case OpCode.setr:
                    result[c] = reg[a];
                    break;
                case OpCode.seti:
                    result[c] = a;
                    break;
                case OpCode.gtir:
                    result[c] = (a > reg[b]) ? 1: 0 ;
                    break;
                case OpCode.gtri:
                    result[c] = (reg[a] > b) ? 1 : 0;
                    break;
                case OpCode.gtrr:
                    result[c] = (reg[a] > reg[b]) ? 1 : 0;
                    break;
                case OpCode.eqir:
                    result[c] = (a == reg[b]) ? 1 : 0;
                    break;
                case OpCode.eqri:
                    result[c] = (reg[a] == b) ? 1 : 0;
                    break;
                case OpCode.eqrr:
                    result[c] = (reg[a] == reg[b]) ? 1 : 0;
                    break;
                default:
                    throw new ArgumentException("Unknown OpCode");
            }
            return result;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            if (isTestRun)
            {
                return "skipped, since it has no second part to excecute";
            }
            List<Sample> samples = new List<Sample>();
            List<int[]> data = new List<int[]>();
            //read samples and data
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].StartsWith("Before"))
                {
                    Sample s = new Sample(input[i++], input[i++], input[i++]);
                    samples.Add(s);
                }
                else if (!string.IsNullOrWhiteSpace(input[i]))
                {
                    data.Add(input[i].Split(' ').Select(int.Parse).ToArray());
                }
            }
            //find possible opcode candidates
            foreach (Sample sample in samples)
            {
                sample.PossibleOpCodes = GetPossibleOpCodes(sample);
            }
            
            //Find matching int for opcode
            Dictionary<int, OpCode> opCodeDictionary = new Dictionary<int, OpCode>();
            do
            {
                OpCode code = GetUniqueOpCode(samples);
                int opcodeNumber = samples.Where(s => s.PossibleOpCodes.Contains(code))
                    .OrderBy(x => x.PossibleOpCodes.Count).First().Instruction[0];
                opCodeDictionary.Add(opcodeNumber, code);
                //TODO discuss: first I removed the current Opcodes from each sample, that didn't work. Why?
                //samples.ForEach(s => s.PossibleOpCodes.Remove(code));
                List<Sample> matchingSamples = samples.Where(s => s.PossibleOpCodes.Contains(code)).ToList();
                matchingSamples.ForEach(sa => samples.Remove(sa));
            } while (samples.Any(s => s.PossibleOpCodes.Count() > 0));

            //execute register and opcodes
            int[] register = new int[] { 0, 0, 0, 0 };
            foreach (int[] codeline in data)
            {
                register = GetResult(opCodeDictionary[codeline[0]], register, codeline);
            }

            return register[0].ToString();
        }

        private OpCode GetUniqueOpCode(List<Sample> samples)
        {
            //TODO tried it with a linq, try again?
            /*
OpCode code = ((OpCode[])Enum.GetValues(typeof(OpCode))) // List of enum
                    .Where(c => samples.Any(s => s.PossibleOpCodes.Contains(c) && !samples.Any(other => other.PossibleOpCodes.Contains(c) && other.Instruction[0] != s.Instruction[0])))
                    .First();
             */

            foreach (OpCode code in Enum.GetValues(typeof(OpCode)))
            {
                List<Sample> matchingSamples = samples.Where(s => s.PossibleOpCodes.Contains(code)).ToList();
                if (matchingSamples.Count() > 0)
                {
                    Sample first = matchingSamples.First();

                    bool codeIsUnique = !samples.Where(sa => sa.PossibleOpCodes.Contains(code)).Any(sa => sa.Instruction[0] != first.Instruction[0]);
                    if (codeIsUnique)
                    {
                        return code;
                    }
                }
            }
            throw new Exception("didn't find any");
        }
    }
}