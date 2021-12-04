namespace AdventOfCode2021.Day04
{
    internal class Board
    {
        private int[] line1;
        private int[] line2;
        private int[] line3;
        private int[] line4;
        private int[] line5;

        private List<int> numbers;

        public Board(int[] vs1, int[] vs2, int[] vs3, int[] vs4, int[] vs5)
        {
            this.line1 = vs1;
            this.line2 = vs2;
            this.line3 = vs3;
            this.line4 = vs4;
            this.line5 = vs5;

            numbers = new List<int>();
        }

        public int[] GetRow(int row)
        {
                return new int[] { line1[row], line2[row], line3[row], line4[row], line5[row] };    
        }


        public bool IsWinner 
        { 
            get 
            {
                return this.HasAnyRowComplete | this.HasAnyColumnComplete;
            }  
        }

        public bool HasAnyColumnComplete
        {
            get
            {
                return GetRow(0).All(i => numbers.Contains(i)) |
                        GetRow(1).All(i => numbers.Contains(i)) |
                        GetRow(2).All(i => numbers.Contains(i)) |
                        GetRow(3).All(i => numbers.Contains(i)) |
                        GetRow(4).All(i => numbers.Contains(i));
            }
        }
        public bool HasAnyRowComplete
        {
            get
            {
                return line1.All(i => numbers.Contains(i)) |
                       line2.All(i => numbers.Contains(i)) |
                       line3.All(i => numbers.Contains(i)) |
                       line4.All(i => numbers.Contains(i)) |
                       line5.All(i => numbers.Contains(i));
            }
        }

        public List<int> Numbers { get { return numbers; } }

        internal void Check(int number)
        {
            numbers.Add(number);
        }

        internal int Score()
        {
            //unmarked numbers
            List<int> unDrawnNumbers = new List<int>();
            unDrawnNumbers.AddRange(line1.Where(n => !numbers.Contains(n)).ToList());
            unDrawnNumbers.AddRange(line2.Where(n => !numbers.Contains(n)).ToList());
            unDrawnNumbers.AddRange(line3.Where(n => !numbers.Contains(n)).ToList());
            unDrawnNumbers.AddRange(line4.Where(n => !numbers.Contains(n)).ToList());
            unDrawnNumbers.AddRange(line5.Where(n => !numbers.Contains(n)).ToList());

            return unDrawnNumbers.Sum() * numbers.Last();
        }
    }
}