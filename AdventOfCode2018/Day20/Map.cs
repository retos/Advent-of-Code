using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day20
{
    internal class Map
    {
        public List<TileInfo> Tiles { get; set; }
        TileInfo[][] floorplan;
        public int StartPosX { get; set; }
        public int StartPosY { get; set; }

        public Map(Directions directions)
        {
            Tiles = new List<TileInfo>();
            BuildFloorPlan(directions, new List<Coordinates>() { new Coordinates(0, 0) });
        }

        public Map()
        {
        }

        private List<Coordinates> BuildFloorPlan(Directions directions, List<Coordinates> startCoordinates)
        {
            //TODO die Geschwister korrekt ablegen
            List<Coordinates> followUpCoordinates = new List<Coordinates>();
            List<Coordinates> nextCoordinates = new List<Coordinates>();
            foreach (Coordinates coordinatesOriginal in startCoordinates)
            {
                Coordinates coordinates = new Coordinates(coordinatesOriginal.X, coordinatesOriginal.Y);
                Tiles.Add(new TileInfo(coordinates, Tile.Room));

                foreach (char c in directions.Steps)
                {
                    switch (c)
                    {
                        case 'N':
                            coordinates.Y--;
                            Tiles.Add(new TileInfo(coordinates, Tile.DoorHorizontal));
                            coordinates.Y--;
                            Tiles.Add(new TileInfo(coordinates, Tile.Room));
                            break;
                        case 'E':
                            coordinates.X++;
                            Tiles.Add(new TileInfo(coordinates, Tile.DoorVertical));
                            coordinates.X++;
                            Tiles.Add(new TileInfo(coordinates, Tile.Room));
                            break;
                        case 'S':
                            coordinates.Y++;
                            Tiles.Add(new TileInfo(coordinates, Tile.DoorHorizontal));
                            coordinates.Y++;
                            Tiles.Add(new TileInfo(coordinates, Tile.Room));
                            break;
                        case 'W':
                            coordinates.X--;
                            Tiles.Add(new TileInfo(coordinates, Tile.DoorVertical));
                            coordinates.X--;
                            Tiles.Add(new TileInfo(coordinates, Tile.Room));
                            break;
                        default:
                            break;
                    }
                }
                nextCoordinates.Add(coordinates);
            }
            foreach (Directions nextDirection in directions.NextDirections)
            {
                followUpCoordinates.AddRange(BuildFloorPlan(nextDirection, nextCoordinates));
            }

            return followUpCoordinates.Distinct().ToList();
        }

        internal int GetFurthestRoom()
        {
            int startX = StartPosX;
            int startY = StartPosY;

            SetCost(ref floorplan[startX][startY], 0);

            floorplan[startX][startY].Cost = 0;
            floorplan[startX][startY].Visited = true;

            List<TileInfo> rooms = floorplan.SelectMany(f => f.Where(b => b.Tile==Tile.Room)).ToList();

            return floorplan.SelectMany(f => f.Where(b => b.Tile == Tile.Room)).ToList().Max(m => m.Cost);
        }

        private void SetCost(ref TileInfo room, int cost)
        {
            room.Cost = cost;
            room.Visited = true;
            List<TileInfo> adjascentRooms =  room.AdjascentRooms.Where(t => !t.Visited).ToList();
            for (int i = 0; i < adjascentRooms.Count; i++)
            {
                TileInfo t = adjascentRooms[i];
                SetCost(ref t, cost + 1);
            }
        }

        internal void PrintFloorplan()
        {
            int minX = Tiles.Min(t => t.PosX)-1;
            int maxX = Tiles.Max(t => t.PosX)+2;
            int minY = Tiles.Min(t => t.PosY)-1;
            int maxY = Tiles.Max(t => t.PosY)+2;

            StartPosX = (minX < 0) ? Math.Abs(minX) : 0;
            StartPosY = (minY < 0) ? Math.Abs(minY) : 0;
            floorplan = new TileInfo[maxX + StartPosX][];
            for (int i = 0; i < floorplan.Length; i++)
            {
                floorplan[i] = new TileInfo[maxY + StartPosY];
            }

            for (int x = 0; x < maxX+StartPosX; x++)
            {
                for (int y = 0; y < maxY + StartPosY; y++)
                {
                    Console.SetCursorPosition(x, y);
                    TileInfo currentTile = Tiles.Where(t => t.PosX == x - StartPosX && t.PosY == y - StartPosY).FirstOrDefault();
                    if (currentTile != null)
                    {
                        Console.Write(currentTile.ToString());
                        floorplan[x][y] = new TileInfo(currentTile.Tile, ref floorplan, x, y);
                    }
                    else
                    {
                        Console.Write("#");
                        floorplan[x][y] = new TileInfo(Tile.Wall, ref floorplan, x, y);
                    }
                }
            }

            Console.SetCursorPosition(StartPosX,StartPosY);
            Console.Write("X");
        }
    }
}