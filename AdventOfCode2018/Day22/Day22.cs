using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day22
{ 
    internal class Day22 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => false;

        public Tile[][] Map { get; set; }

        public override string Part1(List<string> input, bool isTestRun)
        {
            //Initialize variables
            int depth = 4002;
            int targetX = 5;
            int targetY = 746;
            int erosionModuloNumber = 20183;
            int geoIndexXZeroFactor = 16807;
            int geoIndexYZeroFactor = 48271;
            if (isTestRun)
            {
                depth = 510;
                targetX = 10;
                targetY = 10;
            }

            //Initialize map (with null to distinguish checked vs. unchecked regions on the map)
            Map = new Tile[targetX + 1][];
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i] = new Tile[targetY + 1];
            }

            //region divided into squares(type: rocky, narrow, wet) of X, Y coordinates(starting at 0, 0)
            //type defined by erosion level
            //erosion level defined by geological index
            //geolocial index derived from 5 rules (the first applicable counts)
            //  1) (0, 0) has geological index of 0
            //  2) target coordinate has geological index 0
            //  3) If Y = 0->geoIdx = X * 16807
            //  4) If X = 0->geoIdx = Y * 48271
            //  5) Else: geoIdx = erosionLvl(X - 1, Y) * erosionLvl(X, Y - 1)
            //Erosion level = (geoIdx * depth) % 20183
            //Region type = erosionLvl % 3(where 0:rocky, 1:wet, 2:narrow)

            //Rule 1
            Map[0][0] = new Tile() { GeologicalIndex = 0 };
            //Rule 2
            Map[targetX][targetY] = new Tile() { GeologicalIndex = 0 };

            for (int x = 0; x < Map.Length; x++)
            {
                for (int y = 0; y < Map[0].Length; y++)
                {
                    //Rule 3
                    if (Map[x][y] == null && y == 0)
                    {
                        Map[x][y] = new Tile() { GeologicalIndex = (ulong)(x * geoIndexXZeroFactor), Map = Map, X = x, Y = y, Depth = depth };
                    }
                    //Rule 4
                    else if (Map[x][y] == null && x == 0)
                    {
                        Map[x][y] = new Tile() { GeologicalIndex = (ulong)(y * geoIndexYZeroFactor), Map = Map, X = x, Y = y, Depth = depth };
                    }
                }
            }

            //Rule #5
            do
            {
                List<Tile> unsetTiles = GetUnsetTilesWhereRule5IsPossible(depth);
                foreach (Tile tile in unsetTiles)
                {
                    tile.GeologicalIndex = Map[tile.X - 1][tile.Y].ErosionLevel * Map[tile.X][tile.Y - 1].ErosionLevel;
                    Map[tile.X][tile.Y] = tile;
                }
            } while (Map.SelectMany(x => x).Any(t => t == null));


            //DrawMap(targetX, targetY);

            int totalAreaRisk = Map.SelectMany(x => x).Where(t => t.X <= targetX && t.Y <= targetY).Sum(t => t.RiskLevel);
            return totalAreaRisk.ToString();
        }

        private void DrawMap(int targetX, int targetY)
        {
            foreach (Tile tile in Map.SelectMany(x=>x).ToList())
            {
                Console.SetCursorPosition(tile.X, tile.Y);
                Console.Write(tile.Type);
            }
            Console.SetCursorPosition(0, 0);
            //Console.Write(tile.RiskLevel);
            Console.Write("M");

            Console.SetCursorPosition(targetX, targetY);
            //Console.Write(tile.RiskLevel);
            Console.Write("T");
        }

        private List<Tile> GetUnsetTilesWhereRule5IsPossible(int depth)
        {
            List<Tile> unsetTiles = new List<Tile>();
            for (int x = 0; x < Map.Length; x++)
            {
                for (int y = 0; y < Map[0].Length; y++)
                {
                    //Rule 3
                    if (Map[x][y] == null && Map[x-1][y] !=null && Map[x][y-1] != null)
                    {
                        unsetTiles.Add(new Tile() { GeologicalIndex = null, Map = Map, X = x, Y = y, Depth = depth });
                    }
                }
            }

            return unsetTiles;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }
    }
}