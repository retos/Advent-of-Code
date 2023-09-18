namespace AdventOfCode2022.Day13
{
    public class Package
    {
        private string line;

        public Package(string line)
        {
            this.line = line;
        }

        public string RawData { get => line; set => line = value; }

    }
}