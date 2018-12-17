using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day15
{
    internal class Route
    {
        public Coordinates CurrentPosition { get; internal set; }
        public Coordinates TargetPosition { get; internal set; }
        public List<Coordinates> ReachableTiles { get; internal set; }
        public Coordinates FirstStep { get; internal set; }
        public int Cost
        {
            get
            {
                if (ReachableTiles == null || ReachableTiles.Count < 1)
                {
                    return int.MaxValue;
                }
                return ReachableTiles.Where(r => r.X.Equals(CurrentPosition.X) && r.Y.Equals(CurrentPosition.Y)).First().Cost;
            }
        }
    }
}