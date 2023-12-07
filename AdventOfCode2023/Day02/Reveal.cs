namespace AdventOfCode2023.Day02
{
    internal class Reveal
    {
        public long Red { get; internal set; }
        public long Blue { get; internal set; }
        public long Green { get; internal set; }
        public long PowerOfReveal
        {
            get
            {
                return Red * Blue * Green;
            }
        }
    }
}