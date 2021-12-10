namespace AdventOfCode2018.Day25
{
    internal class SpacetimePoints
    {
        private string input;
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int V { get; set; }


        public SpacetimePoints(string line)
        {
            this.input = line;
            string[] parts = line.Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);    
            Z = int.Parse(parts[2]);
            V = int.Parse(parts[3]);



        }
    }
}