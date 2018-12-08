using System.Collections.Generic;

namespace AdventOfCode2018.Day07
{
    internal class StepInstruction
    {
        public char Name { get; set; }
        public char BeforeStepName { get; set; }

        public StepInstruction(char v1, char v2)
        {
            Name = v1;
            BeforeStepName = v2;
        }
    }
}