namespace AdventOfCode2022.Day05;

internal class Day05 : DayBase
{
    public override string Title => "--- Day 5: Supply Stacks ---";
    public override bool Ignore => false;
    public List<List<char>> Stacks { get; set; }

    public override string Part1(List<string> input, bool isTestRun)
    {
        Stacks = new List<List<char>>();
        List<Instruction> instructions = new List<Instruction>();

        //Init Stacks
        int numberOfStacks = (input[0].Length + 1) / 4;
        for (int i = 0; i < numberOfStacks; i++)
        {
            Stacks.Add(new List<char>());
        }

        foreach (string line in input)
        {
            if (!line.StartsWith('m'))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < numberOfStacks; i++)
                    {
                        //try and add current crate
                        char c = line[(4 * i) + 1];
                        if (char.IsLetter(c))
                        {
                            Stacks[i].Add(c);
                        }
                    }
                }
            }
            else
            {
                instructions.Add(new Instruction(line));
            }
        }

        Stacks.ForEach(s => s.Reverse());

        foreach (Instruction instruction in instructions)
        {
            Rearrange(instruction);
        }

        string lastItems = string.Empty;

        foreach (List<char> stack in Stacks)
        {
            lastItems += stack.Last();
        }
        //TSBPBNGZW
        return lastItems;
    }

    private void Rearrange(Instruction instruction)
    {
        for (int i = 0; i < instruction.Amount; i++)
        {
            Char c = Stacks[instruction.MoveFrom - 1].Last();
            Stacks[instruction.MoveFrom - 1].RemoveAt(Stacks[instruction.MoveFrom - 1].Count() - 1);
            Stacks[instruction.MoveTo - 1].Add(c);
        }
    }
    private void Rearrange2(Instruction instruction)
    {
        List<char> crates = Stacks[instruction.MoveFrom - 1].GetRange(Stacks[instruction.MoveFrom - 1].Count() - instruction.Amount, instruction.Amount);
        Stacks[instruction.MoveFrom - 1].RemoveRange(Stacks[instruction.MoveFrom - 1].Count() - instruction.Amount, instruction.Amount);

        Stacks[instruction.MoveTo - 1].AddRange(crates);
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        Stacks = new List<List<char>>();
        List<Instruction> instructions = new List<Instruction>();

        //Init Stacks
        int numberOfStacks = (input[0].Length + 1) / 4;
        for (int i = 0; i < numberOfStacks; i++)
        {
            Stacks.Add(new List<char>());
        }

        foreach (string line in input)
        {
            if (!line.StartsWith('m'))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < numberOfStacks; i++)
                    {
                        //try and add current crate
                        char c = line[(4 * i) + 1];
                        if (char.IsLetter(c))
                        {
                            Stacks[i].Add(c);
                        }
                    }
                }
            }
            else
            {
                instructions.Add(new Instruction(line));
            }
        }

        Stacks.ForEach(s => s.Reverse());

        foreach (Instruction instruction in instructions)
        {
            Rearrange2(instruction);
        }

        string lastItems = string.Empty;

        foreach (List<char> stack in Stacks)
        {
            lastItems += stack.Last();
        }
        //STVPBNGZW
        return lastItems;
    }
}

