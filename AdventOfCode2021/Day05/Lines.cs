namespace AdventOfCode2021.Day05
{
    internal class Line
    {
        private string inputLine;


        public int XStart { get; set; }
        public int YStart { get; set; }
        public int XEnd { get; set; }
        public int YEnd { get; set; }
        public int MaxX 
        { 
            get 
            {
                return (XEnd > XStart) ? XEnd : XStart ;
            }
        }
        public int MinX
        {
            get
            {
                return (XEnd < XStart) ? XEnd : XStart;
            }
        }

        public int MaxY
        {
            get
            {
                return (YEnd > YStart) ? YEnd : YStart;
            }
        }
        public int MinY
        {
            get
            {
                return (YEnd < YStart) ? YEnd : YStart;
            }
        }

        public Line(string inputLine)
        {
            this.inputLine = inputLine;
            string[] parts = inputLine.Split(" -> ");
            string[] start = parts[0].Split(',');
            string[] end = parts[1].Split(',');

            XStart = int.Parse(start[0]);
            YStart = int.Parse(start[1]);

            XEnd = int.Parse(end[0]);
            YEnd = int.Parse(end[1]);



        }

        internal void DrawOnMap(ref char[,] map)
        {
            throw new NotImplementedException();
        }

        internal bool PosInRange(int currentPosX)
        {
            return currentPosX >= XStart && currentPosX <= XEnd ||
                   currentPosX <= XStart && currentPosX >= XEnd;
        }
    }
}