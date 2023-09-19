namespace AdventOfCode2022.Day13
{
    internal class RockStructure
    {
        public int XStart { get; private set; }
        public int XEnd { get; private set; }

        private int YEnd;

        public int YStart { get; private set; }

        public int XMax 
        {
            get 
            {
                return (XStart > XEnd) ? XStart : XEnd ; 
            }  
        }
        public int XMin
        {
            get
            {
                return (XStart < XEnd) ? XStart : XEnd;
            }
        }
        public int YMax
        {
            get
            {
                return (YStart > YEnd) ? YStart : YEnd;
            }
        }
        public int YMin
        {
            get
            {
                return (YStart < YEnd) ? YStart : YEnd;
            }
        }

        internal static List<RockStructure> ReadInput(List<string> input)
        {
            List<RockStructure> structures = new List<RockStructure>();
            foreach (string line in input)
            {
                RockStructure structure = null;

                int? lastX = null;
                int? lastY = null;

                string[] parts = line.Split(" -> ");
                foreach (string coordinate in parts)
                {
                    string[] coordinateParts = coordinate.Split(",");
                    int currentX = int.Parse(coordinateParts[0]);
                    int currentY = int.Parse(coordinateParts[1]);

                    if(lastX == null)
                    {
                        //its the first entry, just keep but do not create vector for structure yet
                        lastX = currentX;
                        lastY = currentY;
                    }
                    else
                    {
                        structure = new RockStructure();
                        structures.Add(structure);
                        structure.XStart = (int)lastX;
                        structure.YStart = (int)lastY;
                        structure.XEnd = currentX;
                        structure.YEnd = currentY;
                        lastX = currentX;
                        lastY = currentY;
                    }
                }
            }

            return structures;
        }
    }
}