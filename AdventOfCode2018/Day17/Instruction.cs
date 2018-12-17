namespace AdventOfCode2018.Day17
{
    internal class Instruction
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }

        public Instruction(string line)
        {
            string[] split = line.Split(',');
            foreach (string s in split)
            {
                string currentInfo = s.Trim();
                if (currentInfo.StartsWith("x"))
                {
                    string[] values = currentInfo.Split('=');
                    if (!values[1].Contains("."))
                    {
                        StartX = int.Parse(values[1]);
                        EndX = int.Parse(values[1]);
                    }
                    else
                    {
                        StartX = int.Parse(values[1].Split('.')[0]);
                        EndX = int.Parse(values[1].Split('.')[2]);
                    }
                }
                else if (currentInfo.StartsWith("y"))
                {
                    string[] values = currentInfo.Split('=');
                    if (!values[1].Contains("."))
                    {
                        StartY = int.Parse(values[1]);
                        EndY = int.Parse(values[1]);
                    }
                    else
                    {
                        StartY = int.Parse(values[1].Split('.')[0]);
                        EndY = int.Parse(values[1].Split('.')[2]);
                    }
                }
            }
        }
    }
}