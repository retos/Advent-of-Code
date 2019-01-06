using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day15
{
    internal class Route
    {
        public Coordinates StartPosition { get; internal set; }
        public Coordinates TargetPosition { get; internal set; }
        public List<Coordinates> ReachableTiles { get; internal set; }
        public int Cost
        {
            get
            {
                if (ReachableTiles == null || ReachableTiles.Count() < 1)
                {
                    return int.MaxValue;
                }
                return ReachableTiles.Where(r => r.X.Equals(StartPosition.X) && r.Y.Equals(StartPosition.Y)).First().Cost;
            }
        }

        internal void Djikstra()
        {
            Coordinates currentPosition = StartPosition;
            currentPosition.Cost = 0;

            do
            {
                currentPosition.Visited = true;
                //Update cost of  adjascent tiles
                //TODO do not calculate all reachable tiles, only check the four around...
                List<Coordinates> adjascentTiles = ReachableTiles.Where(r => r.Distance(currentPosition) == 1).ToList();
                foreach (Coordinates adjascentTile in adjascentTiles.Where(t => !t.Visited))
                {
                    int calculatedCost = currentPosition.Cost + 1;
                    if (calculatedCost < adjascentTile.Cost)
                    {
                        adjascentTile.Cost = calculatedCost;
                    }
                }
                //pick cheapest one
                currentPosition = ReachableTiles
                    .Where(r => !r.Visited)
                    .OrderBy(r => r.Cost)
                    .ThenBy(r => r.Y)
                    .ThenBy(r => r.X)
                    .FirstOrDefault();
                
                //TODO Testen ob ein abbruch beim ersten Ziel zum gleichen resultat führt... Vermutlich nicht wegen Sackgassen?
            } while (ReachableTiles.Any(r => !r.Visited && r.IsTarget));//solange bleiben bis alle erreichbaren Ziele besucht wurden


            Coordinates target = ReachableTiles
                .Where(r => r.IsTarget)
                .OrderBy(r => r.Cost)
                .ThenBy(r => r.Y)
                .ThenBy(r => r.X)
                .FirstOrDefault();

            TargetPosition = target;
        }
    }
}