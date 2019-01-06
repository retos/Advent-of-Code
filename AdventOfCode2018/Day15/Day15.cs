using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day15
{
    internal class Day15 : DayBase
    {
        public override string Title => "--- Day 15: Beverage Bandits ---";

        public override bool Ignore => false;
        public bool IsDebug { get; set; }
        public char[][] Map { get; set; }
        public List<Coordinates> WalkableCoordinates { get; private set; }
        public int Round { get; set; }
        public override string Part1(List<string> input, bool isTestRun)
        {
            if (isTestRun)
            {
                return "skipped";
            }
            List<Unit> units = DoBattle(3, input, isTestRun);

            return $"{(Round) * units.Where(u => !u.Dead).Sum(u => u.Hitpoints)}";
        }

        private List<Unit> DoBattle(int attackPower, List<string> input, bool isTestRun)
        {
            IsDebug = false;

            //read input
            Map = input.Select(i => i.ToArray()).ToArray();
            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            List<Unit> units = new List<Unit>();
            WalkableCoordinates = new List<Coordinates>();
            for (int x = 0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    switch (Map[y][x])
                    {
                        case 'G':
                            units.Add(new Unit(x, y, 'G', ref units, 3));
                            Map[y][x] = '.';
                            WalkableCoordinates.Add(new Coordinates() { X = x, Y = y });
                            break;
                        case 'E':
                            units.Add(new Unit(x, y, 'E', ref units, attackPower));
                            Map[y][x] = '.';
                            WalkableCoordinates.Add(new Coordinates() { X = x, Y = y });
                            break;
                        case '.':
                            WalkableCoordinates.Add(new Coordinates() { X = x, Y = y });
                            break;
                        default:
                            break;
                    }
                }

            }

            //order for round is reading order of position
            Round = 0;
            PrintMap(units);

            bool battleInProgress = true;
            do
            {
                foreach (Unit unit in units.Where(u => !u.Dead).OrderBy(u => u.Y).ThenBy(u => u.X))
                {
                    if (!unit.Dead)
                    {
                        if (!unit.IsInAttachRange()) //only move if there is no enemy in range already
                        {
                            //MOVE
                            //Select enemy-squares
                            IEnumerable<Unit> enemies = unit.GetEnemies();
                            if (enemies.Count() < 1)
                            {
                                battleInProgress = false;
                                break;
                            }
                            IEnumerable<Coordinates> targetTiles = GetAdjascentEmptyTiles(enemies);
                            Route targetRoute = GetRouteFromClosestTarget(unit.GetCoordinates(), targetTiles, ref units);

                            if (targetRoute != null && targetRoute.TargetPosition != null)
                            {
                                //This unit can reach a target, continue
                                //Step towards enemy
                                PrintMap(targetRoute, units);
                                unit.Step(targetRoute.TargetPosition);
                            }

                            PrintMap(units);
                        }

                        //ATTACK
                        IEnumerable<Unit> enemiesInRange = unit.GetEnemiesInRange();
                        if (enemiesInRange.Count() >= 1)
                        {
                            //This unit has an enemy within range, continue
                            Unit enemyWithLowestHitpoints = enemiesInRange.OrderBy(e => e.Hitpoints).ThenBy(e => e.Y).ThenBy(e => e.X).First();
                            //attacking...
                            enemyWithLowestHitpoints.Hitpoints -= unit.Attackpower;
                            if (enemyWithLowestHitpoints.Hitpoints <= 0)
                            {
                                enemyWithLowestHitpoints.Dead = true;
                                if (attackPower>3 && enemyWithLowestHitpoints.Type.Equals('E'))//Part 2 abort on elve-death
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
                if (battleInProgress)
                {
                    Round++;
                }
                PrintMap(units);
            } while (battleInProgress);

            return units;
        }

        private Route GetRouteFromClosestTarget(Coordinates startCoordinate, IEnumerable<Coordinates> targetCoordinates, ref List<Unit> units)
        {
            Route route = new Route();
            route.ReachableTiles = GetAllReachableTilesForUnit(startCoordinate, units);
            route.StartPosition = startCoordinate;
            route.ReachableTiles.Add(startCoordinate);
            
            foreach (Coordinates tile in route.ReachableTiles)
            {
                tile.Visited = false;
                tile.Cost = int.MaxValue;
                tile.IsTarget = targetCoordinates.Any(t => t.Equals(tile));
            }

            route.Djikstra();

            if (route.TargetPosition == null)
            {
                return null;//No route found!
            }

            if (IsDebug) { PrintMap(route, units); } 

            Route reverseRoute = new Route();
            reverseRoute.ReachableTiles = GetAllReachableTilesForUnit(route.TargetPosition, units);
            reverseRoute.StartPosition = route.TargetPosition;
            reverseRoute.ReachableTiles.Add(route.StartPosition);

            foreach (Coordinates tile in reverseRoute.ReachableTiles)
            {
                tile.Visited = false;
                tile.Cost = int.MaxValue;
                tile.IsTarget = tile.Distance(route.StartPosition) == 1; //Add acessible adjascent tiles as targets so they will all be visited during calculation!
            }


            reverseRoute.Djikstra();
            if (IsDebug) { PrintMap(route, units); }
            return reverseRoute;
        }

        private void PrintMap(Route route, IEnumerable<Unit> units)
        {
            if (IsDebug)
            {
                PrintMap(units);

                foreach (Coordinates coordinates in route.ReachableTiles)
                {
                    if (coordinates.IsTarget)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (coordinates.Equals(route.TargetPosition))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    else if (coordinates.Visited)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.SetCursorPosition(coordinates.X, coordinates.Y);
                    if (coordinates.Cost > 9)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write(coordinates.Cost);
                    }

                }
                //Mark start in Green, with correct Letter if it is a unit
                Unit unit = units.Where(u => u.GetCoordinates().Equals(route.StartPosition)).FirstOrDefault();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(route.StartPosition.X, route.StartPosition.Y);
                if (unit != null)
                {
                    
                    Console.Write(unit.Type);
                }
                else
                {
                    Console.Write('S');
                }

                //TODO: Weil der Startpunkt nicht mehr ein unit ist sondern die angrenzenden Flächen kann es nicht mehr markiert werden. Bei der reverse Route
                //Mark target in yellow, with correct Letter if it is a unit
                if (route.TargetPosition != null)
                {
                    unit = units.Where(u => u.GetCoordinates().Equals(route.TargetPosition)).FirstOrDefault();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(route.TargetPosition.X, route.TargetPosition.Y);
                    if (unit!=null)
                    {
                        Console.Write(unit.Type);
                    }
                    else
                    {
                        Console.Write('T');
                    }
                    
                }

                Console.ResetColor();
            }           
        }

        private void PrintMap(IEnumerable<Unit> units)
        {
            for (int x = 0; x < Map[0].Length; x++)
            {
                for (int y = 0; y < Map.Length; y++)
                {
                    Console.SetCursorPosition(x,y);
                    Console.Write(Map[y][x]);
                }
            }
            foreach (Unit unit in units.Where(u => !u.Dead))
            {
                Console.SetCursorPosition(unit.X, unit.Y);
                Console.Write(unit.Type);
            }

            Console.SetCursorPosition(Map[0].Length + 2, 0);
            Console.Write($"round: {Round}");
            


            Console.SetCursorPosition(Map[0].Length + 2, 1);
            Console.Write($"elves count:    {units.Where(u => !u.Dead && u.Type.Equals('E')).Count()}");

            Console.SetCursorPosition(Map[0].Length + 2, 2);
            Console.Write($"goblins count:  {units.Where(u => !u.Dead && u.Type.Equals('G')).Count()}");

            //Console.SetCursorPosition(Map[0].Length + 2, 3);
            //Console.Write("Game is paused, hit key for next round.");
            //Console.ReadKey();
        }
    


        private List<Coordinates> GetAllReachableTilesForUnit(Coordinates coordinates, List<Unit> units)
        {
            //remove the tiles occupied by unit
            List<Coordinates> currentlyWalkableCoordinates = WalkableCoordinates.Where(w => !units.Where(u => !u.Dead).Any(u => u.X.Equals(w.X) && u.Y.Equals(w.Y))).ToList();

            List<Coordinates> reachableTiles = new List<Coordinates>();
            int lastNumberOfReachableTiles;
            do
            {
                lastNumberOfReachableTiles = reachableTiles.Count();
                foreach (Coordinates c in currentlyWalkableCoordinates.Where(w => !reachableTiles.Contains(w)))
                {
                    if (reachableTiles.Any(r => r.Distance(c) <= 1) || c.Distance(coordinates) <= 1)
                    {
                        reachableTiles.Add(c);
                    }
                }
            } while (reachableTiles.Count > lastNumberOfReachableTiles);
            return reachableTiles;
        }

        private IEnumerable<Coordinates> GetAdjascentEmptyTiles(IEnumerable<Unit> enemies)
        {
            List<Coordinates> emptyTiles = new List<Coordinates>();
            foreach (Unit enemy in enemies)
            {
                //oben
                emptyTiles.Add(new Coordinates() { X = enemy.X, Y = enemy.Y - 1 });
                //links
                emptyTiles.Add(new Coordinates() { X = enemy.X - 1, Y = enemy.Y });
                //rechts
                emptyTiles.Add(new Coordinates() { X = enemy.X + 1, Y = enemy.Y });
                //unten
                emptyTiles.Add(new Coordinates() { X = enemy.X, Y = enemy.Y + 1 });
            }
            emptyTiles.Distinct();
            emptyTiles = emptyTiles.Where(t => Map[t.Y][t.X].Equals('.')).ToList();
            return emptyTiles;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            if (isTestRun)
            {
                return "skipped";
            }
            int attackPower = 4;
            do
            {
                List<Unit> units = DoBattle(attackPower, input, isTestRun);
                if (units == null)
                {
                    Console.WriteLine($"Elves die with attackpower {attackPower}     ");
                    attackPower++;
                }
                else
                {
                    return $"{(Round) * units.Where(u => !u.Dead && u.Type.Equals('E')).Sum(u => u.Hitpoints)}";
                }

            } while (true);
        }
    }
}