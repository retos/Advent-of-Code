using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day13
{
    internal class Day13 : DayBase
    {
        public override string Title => "--- Day 13: Mine Cart Madness ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            char[][] map = input.Select(i => i.ToArray()).ToArray();
            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            List<Cart> carts = new List<Cart>();
            for (int x = 0; x < xDimension ; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    switch (map[y][x])
                    {
                        case '>':
                            carts.Add(new Cart(x, y, Direction.Right));
                            map[y][x] = '-';
                            break;
                        case '^':
                            carts.Add(new Cart(x, y, Direction.Up));
                            map[y][x] = '-';
                            break;
                        case '<':
                            carts.Add(new Cart(x, y, Direction.Left));
                            map[y][x] = '-';
                            break;
                        case 'v':
                            carts.Add(new Cart(x, y, Direction.Down));
                            map[y][x] = '-';
                            break;
                        default:
                            break;
                    }
                }

            }

            bool crashed = false;
            int steps = 0;
            do
            {
                steps++;
                foreach (Cart cart in carts.OrderBy(c => c.X).ThenBy(c => c.Y))
                {                   
                    cart.Move(map);                    
                    crashed = carts.Any(c => carts.Count(k => k.X.Equals(c.X) && k.Y.Equals(c.Y)) > 1);
                    
                    if (crashed)
                    {
                        break;
                    }
                }                
            } while (!crashed);

            //Not elegant, try with grouping?
            Cart crashedC = carts.Where(c => carts.Count(k => k.X == c.X && k.Y == c.Y) > 1).First();
            foreach (Cart c in carts.OrderBy(c => c.X)) { Console.WriteLine($"{c.X}, {c.Y}"); }

            return $"first collision after {steps} steps: {crashedC.X},{crashedC.Y}";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            if (isTestRun)
            {
                return "skipped";
            }
            char[][] map = input.Select(i => i.ToArray()).ToArray();
            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            List<Cart> carts = new List<Cart>();
            for (int x = 0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    switch (map[y][x])
                    {
                        case '>':
                            carts.Add(new Cart(x, y, Direction.Right));
                            map[y][x] = '-';
                            break;
                        case '^':
                            carts.Add(new Cart(x, y, Direction.Up));
                            map[y][x] = '-';
                            break;
                        case '<':
                            carts.Add(new Cart(x, y, Direction.Left));
                            map[y][x] = '-';
                            break;
                        case 'v':
                            carts.Add(new Cart(x, y, Direction.Down));
                            map[y][x] = '-';
                            break;
                        default:
                            break;
                    }
                }

            }

            bool crashed = false;
            int steps = 0;
            do
            {
                steps++;
                foreach (Cart cart in carts.OrderBy(c => c.X).ThenBy(c => c.Y))
                {
                    cart.Move(map);
                    crashed = carts.Any(c => carts.Count(k => k.X.Equals(c.X) && k.Y.Equals(c.Y)) > 1);

                    if (crashed)
                    {
                        Cart crashedC = carts.Where(c => carts.Count(k => k.X == c.X && k.Y == c.Y) > 1).First();
                        carts = carts.Where(c => !c.X.Equals(crashedC.X) && !c.Y.Equals(crashedC.Y)).ToList();
                    }
                }
            } while (carts.Count > 1);

            return $"first collision after {steps} steps: {carts.First().X},{carts.First().Y}";
        }
    }
}