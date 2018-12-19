using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day18
{
    internal class Day18 : DayBase
    {
        public override string Title => "--- Day 18: Settlers of The North Pole ---";

        public override bool Ignore => true;
        public char[][] Map { get; set; }
        public char[][] NextMap { get; set; }
        public string LumberAndWood
        {
            get
            {
                int woodCount = 0;
                int lumberCount = 0;
                for (int x = 0; x < Map.Length; x++)
                {
                    for (int y = 0; y < Map[0].Length; y++)
                    {
                        if (Map[x][y].Equals('|'))
                        {
                            woodCount++;
                        }
                        if (Map[x][y].Equals('#'))
                        {
                            lumberCount++;
                        }
                    }
                }
                return $"wood: {woodCount}, lumner: {lumberCount} w*l = {woodCount*lumberCount}";
            }
        }
   
        public override string Part1(List<string> input, bool isTestRun)
        {
            Map = input.Select(i => i.ToArray()).ToArray();
            NextMap = DeepCopy(Map);
            //PrintMap();

            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            int numberOfIntervals = 10;
            List<KeyValuePair<int, string>> mapResults = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < numberOfIntervals; i++)
            {              
                for (int x = 0; x <= xDimension; x++)
                {
                    for (int y = 0; y <= yDimension; y++)
                    {
                        switch (Map[y][x])
                        {
                            case '.':
                                List<char> adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('|')).Count() >= 3)
                                {
                                    NextMap[y][x] = '|';
                                }
                                break;
                            case '|':
                                adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('#')).Count() >= 3)
                                {
                                    NextMap[y][x] = '#';
                                }
                                break;
                            case '#':
                                adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('#')).Count() >= 1 && adjascentTiles.Where(a => a.Equals('|')).Count() >= 1)
                                {
                                    NextMap[y][x] = '#';
                                }
                                else
                                {
                                    NextMap[y][x] = '.';
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }

                Map = DeepCopy(NextMap);
                //PrintMap();
            }

            int woodCount = 0;
            int lumberCount = 0;

            for (int x = 0; x < Map.Length; x++)
            {
                for (int y = 0; y < Map[0].Length; y++)
                {
                    if (Map[x][y].Equals('|'))
                    {
                        woodCount++;
                    }
                    if (Map[x][y].Equals('#'))
                    {
                        lumberCount++;
                    }
                }
            }
         


            return $"{lumberCount * woodCount}";
        }

        private char[][] DeepCopy(char[][] map)
        {
            //TODO Discuss DeepCopy
            char[][] deepCopy = new char[map.Length][];
            for (int y = 0; y < map[0].Length; y++)
            {
                deepCopy[y] = new char[map[0].Length];

                for (int x = 0; x < map.Length; x++)
                {
                    deepCopy[y][x] = map[y][x].ToString()[0];
                }
            }return deepCopy;
        }

        private void PrintMap()
        {
            for (int x = 0; x < Map[0].Length; x++)
            {
                for (int y = 0; y < Map.Length; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(Map[y][x]);
                }
            }
        }

        private List<char> GetAdjacentTiles(int x, int y)
        {
            List<char> a = new List<char>();
            for (int xDim = x-1; xDim <= x+1; xDim++)
            {
                for (int yDim = y-1; yDim <= y+1; yDim++)
                {
                    if (xDim>=0 && xDim<Map[0].Length && yDim>=0 && yDim<Map.Length && (x!=xDim || y != yDim))
                    {
                        a.Add(Map[yDim][xDim]);
                    }
                }
            }

            return a;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            Map = input.Select(i => i.ToArray()).ToArray();
            NextMap = DeepCopy(Map);
            //PrintMap();

            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            int numberOfIntervals = 1000000000; //10 for part 1
            List<KeyValuePair<int, string>> mapResults = new List<KeyValuePair<int, string>>();

            for (int i = 0; i < numberOfIntervals; i++)
            {
                KeyValuePair<int, string> currentResult = new KeyValuePair<int, string>(i, LumberAndWood);
                mapResults.Add(currentResult);
                List<KeyValuePair<int, string>> sameResults = mapResults.Where(m => m.Value.Equals(currentResult.Value)).OrderByDescending(r => r.Key).Take(3).ToList();
                if (sameResults.Count() > 2)
                {
                    int diff1 = sameResults[0].Key - sameResults[1].Key;
                    int diff2 = sameResults[1].Key - sameResults[2].Key;
                    //Found at least 3 results, lets see if the last have a repeating intervall
                    if (diff1 == diff2)
                    {
                        if ((numberOfIntervals - i) % diff1 == 0)
                        {
                            return currentResult.Value;
                        }
                    }
                }

                for (int x = 0; x <= xDimension; x++)
                {
                    for (int y = 0; y <= yDimension; y++)
                    {
                        switch (Map[y][x])
                        {
                            case '.':
                                List<char> adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('|')).Count() >= 3)
                                {
                                    NextMap[y][x] = '|';
                                }
                                break;
                            case '|':
                                adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('#')).Count() >= 3)
                                {
                                    NextMap[y][x] = '#';
                                }
                                break;
                            case '#':
                                adjascentTiles = GetAdjacentTiles(x, y);
                                if (adjascentTiles.Where(a => a.Equals('#')).Count() >= 1 && adjascentTiles.Where(a => a.Equals('|')).Count() >= 1)
                                {
                                    NextMap[y][x] = '#';
                                }
                                else
                                {
                                    NextMap[y][x] = '.';
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }

                Map = DeepCopy(NextMap);
                //PrintMap();
            }

            int woodCount = 0;
            int lumberCount = 0;

            for (int x = 0; x < Map.Length; x++)
            {
                for (int y = 0; y < Map[0].Length; y++)
                {
                    if (Map[x][y].Equals('|'))
                    {
                        woodCount++;
                    }
                    if (Map[x][y].Equals('#'))
                    {
                        lumberCount++;
                    }
                }
            }

            return $"{lumberCount * woodCount}";
        }
    }
}