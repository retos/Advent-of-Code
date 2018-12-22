using System.Collections.Generic;

namespace AdventOfCode2018.Day20
{
    internal class TileInfo
    {
        private TileInfo[][] floorplan;
        private Coordinates coordinates;
        private Tile room;

        public List<TileInfo> AdjascentRooms
        {
            get
            {
                List<TileInfo> adjascentTiles = new List<TileInfo>();
                //check up
                if (floorplan[PosX][PosY-1].Tile == Tile.DoorHorizontal)
                {
                    adjascentTiles.Add(floorplan[PosX][PosY - 2]);
                }
                //check right
                if (floorplan[PosX+1][PosY].Tile == Tile.DoorVertical)
                {
                    adjascentTiles.Add(floorplan[PosX+2][PosY]);
                }
                //check down
                if (floorplan[PosX][PosY + 1].Tile == Tile.DoorHorizontal)
                {
                    adjascentTiles.Add(floorplan[PosX][PosY + 2]);
                }
                //check left
                if (floorplan[PosX - 1][PosY].Tile == Tile.DoorVertical)
                {
                    adjascentTiles.Add(floorplan[PosX - 2][PosY]);
                }
                return adjascentTiles;
            }
        }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Tile Tile { get; set; }
        public int Cost { get; set; }
        public bool Visited { get; set; }

        public TileInfo(Coordinates coordinates, Tile tile)
        {
            PosX = coordinates.X;
            PosY = coordinates.Y;
            Tile = tile;
        }

        public TileInfo(Tile tile, ref TileInfo[][] floorplan, int x, int y)
        {
            this.floorplan = floorplan;
            Visited = false;
            Cost = int.MaxValue;
            PosX = x;
            PosY = y;
            Tile = tile;
        }

        public override string ToString()
        {
            switch (Tile)
            {
                case Tile.Room:
                    return ".";
                    break;
                case Tile.Wall:
                    return "#";
                    break;
                case Tile.DoorVertical:
                    return "|";
                    break;
                case Tile.DoorHorizontal:
                    return "-";
                    break;
                case Tile.Unknown:
                    return "?";
                    break;
                default:
                    break;
            }
            return Tile.ToString().Substring(0,1);
        }
    }
}