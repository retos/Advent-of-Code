namespace AdventOfCode2021.Day06
{
    internal class Lanternfish
    {
        private int i;
        private List<Lanternfish> fishes;

        public Lanternfish(int i)
        {
            this.i = i;
            //this.fishes = fishes;
        }

        public int Age { get { return i; }  }

        internal Lanternfish AgeOneDay()
        {
            i--;
            if (i<0)
            {
                i = 6;//Reset age

                //spawn new fish
                Lanternfish spawn = new Lanternfish(8);
                return spawn;
            }
            return null;
        }
    }
}