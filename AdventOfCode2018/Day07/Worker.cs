namespace AdventOfCode2018.Day07
{
    internal class Worker
    {
        public int TimeLeft { get; set; }
        public Step WorkingOnStep { get; set; }
        public char WorkinStepChar
        {
            get
            {
                if (WorkingOnStep== null)
                {
                    return '.';
                }
                else
                {
                    return WorkingOnStep.Name;
                }
            }
        }
    }
}