using System.Linq;

namespace AdventOfCode2022.Day08
{
    public class Tree
    {
        /// <summary>
        /// Postion Spalte
        /// </summary>
        private int spalte;
        /// <summary>
        /// Position Zeile
        /// </summary>
        private int zeile;
        /// <summary>
        /// Sammllung aller Nodes als 2d Array
        /// </summary>
        private Tree[,] map;
        /// <summary>
        /// Sammlung aller Nodes als Liste
        /// </summary>
        private List<Tree> allNodes;
        //private int zeileCount;
        public int ZeilenCount() => map.GetLength(0);

            /*
        int xMapCount = input[0].Length;
        int yMapCount = input.Count;
             */
        //private int spalteCount;
        public int SpaltenCount() => map.GetLength(1);
        private bool? isVisible = null;

        static Direction Left = new Direction(0, -1);
        static Direction Right = new Direction(0, 1);
        static Direction Up = new Direction(-1, 0);
        static Direction Down = new Direction(1, 0);

        public record Direction(int drow, int dcol);

        IEnumerable<Tree> SmallerTrees(Direction dir) =>
            TreesInDirection(dir).TakeWhile(treeT => treeT.Height < this.Height);

        IEnumerable<Tree> TreesInDirection(Direction dir)
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

        public Tree(Tree[,] map, int x, int y, List<Tree> allNodes)
        {
            this.map = map;
            this.spalte = x;
            this.zeile = y;
            this.AllNodes = allNodes;
            allNodes.Add(this);
        }

        public int Height { get; internal set; }
        public bool IsVisible() =>
            (IsTallest(Left) || IsTallest(Right) || IsTallest(Up) || IsTallest(Down));

        public bool IsTallest(Direction dir) =>
            TreesInDirection(dir).All(treeT => treeT.Height < this.Height);

        public long ViewDistance(Direction dir) =>
            IsTallest(dir) ? TreesInDirection(dir).Count() : SmallerTrees(dir).Count() + 1;

        public List<Tree> AllNodes { get => allNodes; set => allNodes = value; }
        //public int ZeileCount { get => ZeileCount(); set => ZeileCount()= value; }
        //public int SpaltenCount { get => spalteCount; set => spalteCount = value; }

        private bool IsEdgeTree()
        {
            return spalte == 0 || zeile == 0 || spalte == SpaltenCount() - 1 || zeile == ZeilenCount() - 1;
        }

        internal long GetScenicScore()
        {
            if (IsEdgeTree()) { return 0; };
            return ViewDistance(Up) * ViewDistance(Down) * ViewDistance(Left) * ViewDistance(Right);
        }
    }
}