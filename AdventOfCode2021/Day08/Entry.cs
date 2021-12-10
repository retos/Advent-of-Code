namespace AdventOfCode2021.Day08
{
    internal class Entry
    {
        private string line;
        public string[] SignalPatterns { get; }
        public string[] OutputPatterns { get; }

        public Entry(string line)
        {
            this.line = line;

            string[] parts = line.Split('|');

            this.SignalPatterns = parts[0].Trim().Split(' ');
            this.OutputPatterns = parts[1].Trim().Split(' ');
        }

        public int CalculateOutput()
        {
            string one = this.SignalPatterns.Where(s => s.Length == 2).Single();
            string four = this.SignalPatterns.Where(s => s.Length == 4).Single();
            string seven = this.SignalPatterns.Where(s => s.Length == 3).Single();
            string eight = this.SignalPatterns.Where(s => s.Length == 7).Single();

           //Six Segments
            //9 is a superset of 4
            string nine = GetCandidates(new string[] {four},
                                        new string[]{ }, 
                                        new string[]{ }, 
                                        6 ).Single();
            //0 is a superset of 1 that is not 9
            string zero = GetCandidates(new string[] { one }, 
                                        new string[] { nine }, 
                                        new string[] { }, 
                                        6).Single();

            //6 is not 0 or 9
            string six = GetCandidates(new string[] { }, 
                                       new string[] { zero, nine }, 
                                       new string[] { }, 
                                       6).Single();

           //Five Segments
            //3 is a superset of 7
            string three = GetCandidates(new string[] { seven }, 
                                         new string[] { }, 
                                         new string[] { }, 
                                         5).Single();
            //5 is a subset of 6
            string five = GetCandidates(new string[] { }, 
                                        new string[] { }, 
                                        new string[] { six }, 
                                        5).Single();
            //2 is not 3 or 5
            string two = GetCandidates(new string[] { },
                                        new string[] { three, five},
                                        new string[] { },
                                        5).Single();

            //Calculate output 
            string output = string.Empty;

            foreach (string s in OutputPatterns)
            {

                if (new string(s.OrderBy(x => x).ToArray()) == new string(zero.OrderBy(x => x).ToArray()))
                {
                    output += "0";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(one.OrderBy(x => x).ToArray()))
                {
                    output += "1";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(two.OrderBy(x => x).ToArray()))
                {
                    output += "2";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(three.OrderBy(x => x).ToArray()))
                {
                    output += "3";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(four.OrderBy(x => x).ToArray()))
                {
                    output += "4";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(five.OrderBy(x => x).ToArray()))
                {
                    output += "5";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(six.OrderBy(x => x).ToArray()))
                {
                    output += "6";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(seven.OrderBy(x => x).ToArray()))
                {
                    output += "7";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(eight.OrderBy(x => x).ToArray()))
                {
                    output += "8";
                }
                if (new string(s.OrderBy(x => x).ToArray()) == new string(nine.OrderBy(x => x).ToArray()))
                {
                    output += "9";
                }

            }

            return int.Parse(output);
        }

        private List<string> GetCandidates(string[] mustInclude, string[] isNot, string[] isContainedIn, int length)
        {
            List<string> matches = new();

            List<string> remainingPatterns = SignalPatterns.Where(p => p.Length == length && !isNot.Contains(p)).ToList();

            foreach (string must in mustInclude)
            {
                remainingPatterns = remainingPatterns.Where(p => p.Intersect(must).Count() == must.Count() ).ToList();
            }

            foreach (string contained in isContainedIn)
            {
                remainingPatterns = remainingPatterns.Where(p => contained.Intersect(p).Count() == p.Count()).ToList();
            }

            return remainingPatterns;
        }

        internal int CountOutput(int v)
        {
            return OutputPatterns.Count(o => o.Length == v);
        }
    }
}