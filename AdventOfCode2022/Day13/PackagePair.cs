namespace AdventOfCode2022.Day13
{
    internal class PackagePair
    {
        public Package Left { get; private set; }
        public Package Right { get; private set; }

        internal static List<PackagePair> ReadInput(List<string> input)
        {
            List<PackagePair> list = new List<PackagePair>();
            PackagePair pair = new PackagePair();
            list.Add(pair);
            foreach (string line in input)
            {
                //New pair?
                if (string.IsNullOrEmpty(line))
                {
                    pair = new PackagePair();
                    list.Add(pair);

                }
                else
                {
                    if (pair.Left == null)
                    {
                        pair.Left = new Package(line);
                    }
                    else
                    {
                        pair.Right = new Package(line);
                    }
                }

            }

            return list;
        }
    }
}