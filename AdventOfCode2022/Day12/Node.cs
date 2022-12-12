using AdventOfCode2022.Day08;

namespace AdventOfCode2022.Day12
{
    internal class Node
    {
        private string elevationIndex = "0abcdefghijklmnopqrstuvwxyz";
        private Node[,] map;
        private int spalte;
        private int zeile;
        private List<Node> allNodes;

        static Direction Left = new Direction(0, -1);
        static Direction Right = new Direction(0, 1);
        static Direction Up = new Direction(-1, 0);
        static Direction Down = new Direction(1, 0);

        public record Direction(int drow, int dcol);

        public Node(Node[,] map, int x, int y, List<Node> allNodes, char v, bool partTwo)
        {
            this.map = map;
            this.spalte = x;
            this.zeile = y;
            this.allNodes = allNodes;
            this.ElevationChar = v;
            if (v == 'S')
            {
                Cost = 0;
            }
            if (partTwo && v == 'a')
            {
                Cost = 0;
            }
        }
        /// <summary>
        /// Anzahl Zeilen auf der Map
        /// </summary>
        /// <returns></returns>
        public int ZeilenCount() => map.GetLength(1);
        public int SpaltenCount() => map.GetLength(0);
        public bool Visited { get; set; } = false;
        public long Cost { get; set; } = long.MaxValue;
        public Node PreviousNode { get; private set; }

        /// <summary>
        /// Anzahl Spalten auf der Map
        /// </summary>
        /// <returns></returns>
        IEnumerable<Node> NodesInDirection(Direction dir)
        {
            var (first, irow, icol) = (true, zeile, spalte);
            while (irow >= 0 && irow < ZeilenCount() && icol >= 0 && icol < SpaltenCount())
            {
                if (!first)
                {
                    yield return map[icol, irow];
                }
                (first, irow, icol) = (false, irow + dir.drow, icol + dir.dcol);
            }
        }
        public void Dijkstra(Node target, bool partTwo = false)
        {
            do
            {
                Node chepestSpot = allNodes.Where(c => !c.Visited && !(c.Cost == long.MaxValue)).OrderBy(c => c.Cost).FirstOrDefault();
                chepestSpot.Visited = true;

                List<Node> unvisitedNeighbors = chepestSpot.GetUnvisitedNeighbors();

                foreach (Node neighbor in unvisitedNeighbors)
                {
                    int elevationCosts = neighbor.GetElevation() - chepestSpot.GetElevation();
                    bool needToGetOutOfClimbingGear = elevationCosts > 1;
                    elevationCosts = (Math.Sign(elevationCosts) < 1) ? 0 : elevationCosts ;
                    long costFromHere = chepestSpot.Cost + elevationCosts + 1;

                    if (!needToGetOutOfClimbingGear && costFromHere < neighbor.Cost)
                    {//elevationCosts not higher than 1 chepest approach
                        neighbor.Cost = costFromHere;
                        neighbor.PreviousNode = chepestSpot;
                    }
                }
                //Console.SetCursorPosition(0, 0);
                //Console.WriteLine($"total nodes: {allNodes.Count()}");
                //Console.WriteLine($"visited    : {allNodes.Where(c => c.Visited).Count()}");
                //PrintToConsole();
            } while (!target.Visited);

            MarkPath(target, partTwo);
        }

        private void MarkPath(Node currentNode, bool partTwo)
        {
            //Walk back from the target and mark nodes as path
            currentNode.PartOfThePath = true;
            if (currentNode.ElevationChar == 'S') return;
            if (partTwo && currentNode.ElevationChar == 'a')
            {
                return;
            }
            MarkPath(currentNode.PreviousNode, partTwo);
        }

        private List<Node> GetUnvisitedNeighbors()
        {
            List<Node> neighbors = new List<Node>();
            neighbors.Add(GetNode(Up));
            neighbors.Add(GetNode(Left));
            neighbors.Add(GetNode(Down));
            neighbors.Add(GetNode(Right));
            neighbors.RemoveAll(item => item == null);
            return neighbors;
        }

        private Node GetNode(Direction dir)
        {
            return NodesInDirection(dir).TakeWhile(n => n != null).FirstOrDefault();
        }

        public int GetElevation()
        {
            if (ElevationChar == 'S')
            {
                return elevationIndex.IndexOf('a');
            }
            else if (ElevationChar == 'E')
            {
                return elevationIndex.IndexOf('z');
            }
            return elevationIndex.IndexOf(ElevationChar);
        }

        public char ElevationChar { get; internal set; }

        public bool PartOfThePath { get; private set; }
        internal List<Node> AllNodes { get => allNodes; set => allNodes = value; }

        private void PrintToConsole()
        {
            Console.WriteLine();
            for (int y = 0; y < map[0, 0].ZeilenCount(); y++)
            {
                for (int x = 0; x < map[0, 0].SpaltenCount(); x++)
                {
                    if (map[x, y].Visited)
                    {
                        if (map[x, y].Cost < 10)
                        {
                            Console.Write(map[x, y].Cost);
                        }
                        else
                        {
                            Console.Write('X');
                        }
                    }
                    else
                    {
                        Console.Write(map[x, y].ElevationChar);
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        internal List<Node> GetPathList()
        {
            List<Node> path = new List<Node>();
            if (PreviousNode != null)
            {
                path = PreviousNode.GetPathList();
            }

            path.Add(this);
            return path;         
        }
        internal int GetPathLenth()
        {
            if (PreviousNode != null)
            {
                return PreviousNode.GetPathLenth()+1;
            }
            else
            {
                return 0;
            }
        }
    }
}

