namespace AdventOfCode2018.Day11
{
    public class FuelCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PowerLevel { get; set; }
        public int RackId { get; set; }

        public FuelCell(int x, int y, int gridSerialNumber)
        {
            X = x;
            Y = y;
            RackId = X + 10;
            PowerLevel = GetDigit((RackId * Y + gridSerialNumber) * RackId) - 5;

        }
        public int GetDigit(int number)
        {
            string temp = number.ToString();
            if (temp.Length >= 3)
            {
                return temp[temp.Length - 3] - '0';
            }
            else
            {
                return 0;
            }           
        }
    }
}