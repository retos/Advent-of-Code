namespace AdventOfCode2018.Day22
{
    public class Tile
    {
        private int? erosionRisk;
        private char? type;
        public char Type 
        {
            get
            {
                if (type == null)
                {
                    switch (RiskLevel)
                    {
                        case 0://rocky
                            type = '.';
                            break;
                        case 1://wet
                            type = '=';
                            break;
                        case 2://narrow
                            type = '|';
                            break;
                    }
                }

                return (char)type;
            }
        }
        public ulong? GeologicalIndex { get; internal set; }
        public ulong ErosionLevel
        {
            get
            {
                return (((ulong)GeologicalIndex) + (ulong)Depth) % 20183;
            }
        }
        public int RiskLevel
        {
            get
            {
                if (erosionRisk == null)
                {
                    erosionRisk = (int)ErosionLevel % 3;
                }
                return (int)erosionRisk;
            }
        }

        public int Depth { get; set; }
        public Tile[][] Map { get; internal set; }
        public int X { get; internal set; }
        public int Y { get; internal set; }
    }
}