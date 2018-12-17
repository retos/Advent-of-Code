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

        public override bool Ignore => true;
        public char[][] Map { get; set; }
        public List<Unit> Units { get; set; }
        public List<Coordinates> WalkableCoordinates { get; private set; }
        public int Round { get; set; }
        public override string Part1(List<string> input, bool isTestRun)
        {
            if (isTestRun)
            {
                return "skipped";
            }
            //read input
            Map = input.Select(i => i.ToArray()).ToArray();
            int xDimension = input[0].Length - 1;
            int yDimension = input.Count - 1;
            Units = new List<Unit>();
            WalkableCoordinates = new List<Coordinates>();
            for (int x = 0; x < xDimension; x++)
            {
                for (int y = 0; y < yDimension; y++)
                {
                    switch (Map[y][x])
                    {
                        case 'G':
                            Units.Add(new Unit(x, y, 'G'));
                            Map[y][x] = '.';
                            WalkableCoordinates.Add(new Coordinates() { X = x, Y = y });
                            break;
                        case 'E':
                            Units.Add(new Unit(x, y, 'E'));
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
            PrintMap();
            
            bool battleInProgress = true;
            do
            {
                foreach (Unit unit in Units.Where(u => !u.Dead).OrderBy(u => u.Y).ThenBy(u => u.X))
                {
                    if (!unit.Dead)
                    {

                        if (GetEnemiesInRange(unit, Units.Where(u => !u.Dead).ToList()).Count() < 1) //only move if there is no enemy in range already
                        {
                            //MOVE
                            //Select enemy-squares
                            List<Unit> enemies = Units.Where(u => !u.Dead).Where(u => !u.Type.Equals(unit.Type)).ToList();
                            if (enemies.Count < 1)
                            {
                                battleInProgress = false;
                                break;
                            }
                            List<Coordinates> targetTiles = GetAdjascentEmptyTiles(enemies);
                            Route routeCosts = GetRoute(unit.X, unit.Y);
                            foreach (var tile in targetTiles)
                            {
                                Coordinates costCoordinates = routeCosts.ReachableTiles.Where(r => r.X.Equals(tile.X) && r.Y.Equals(tile.Y)).FirstOrDefault();
                                tile.Cost = (costCoordinates != null) ? costCoordinates.Cost : int.MaxValue;
                            }
                            Coordinates targetTile = targetTiles
                                .OrderBy(t => t.Cost)
                                .ThenBy(r => r.Y)
                                    .ThenBy(r => r.X)
                                    .FirstOrDefault();
                            if (targetTile != null && targetTile.Cost < int.MaxValue)
                            {
                                //This unit can reach a target, continue
                                Route routeToClosestEnemy = GetRoute(unit, targetTile);
                                Coordinates firstStep = routeToClosestEnemy.FirstStep;
                                //Step towards enemy
                                unit.X = firstStep.X;
                                unit.Y = firstStep.Y;
                            }

                            //PrintMap();   
                        }



                        //ATTACK
                        List<Unit> enemiesInRange = GetEnemiesInRange(unit, Units.Where(u => !u.Dead).ToList());
                        if (enemiesInRange.Count >= 1)
                        {
                            //This unit has an enemy within range, contiue
                            Unit enemyWithLowestHitpoints = enemiesInRange.OrderBy(e => e.Hitpoints).ThenBy(e => e.Y).ThenBy(e => e.X).First();
                            //attacking...
                            enemyWithLowestHitpoints.Hitpoints -= unit.Attackpower;
                            if (enemyWithLowestHitpoints.Hitpoints <= 0)
                            {
                                enemyWithLowestHitpoints.Dead = true;
                            }
                        }
                    }
                }
                if (battleInProgress)
                {
                    Round++;
                }
                PrintMap();
            } while (battleInProgress);

            //First answer '206804' was too low
            //Wrong: 191068
            //202793 also round not reduced by one (205360) was wrong
            //Wrong 201687
            //Wrong 203267
            //Wrong 208198
            return $"{(Round) * Units.Where(u => !u.Dead).Sum(u => u.Hitpoints)}";

            //1. move into range of enemy
            //shortest distance to tiles, if multipe in reading order
            //shortest Path to tile is chosen in reading order 
            //2. attach (if in range)
            //identify targets in range
            //if there are none, end turn
            //select the one with the fewest hitpoints, reading order
            //

            return input.Count.ToString();
        }

        private void PrintMap()
        {
            for (int x = 0; x < Map[0].Length; x++)
            {
                for (int y = 0; y < Map.Length; y++)
                {
                    Console.SetCursorPosition(x,y);
                    Console.Write(Map[y][x]);
                }
            }
            foreach (Unit unit in Units.Where(u => !u.Dead))
            {
                Console.SetCursorPosition(unit.X, unit.Y);
                Console.Write(unit.Type);
            }

            Console.SetCursorPosition(Map[0].Length + 2, 0);
            Console.Write($"round: {Round}");
            


            Console.SetCursorPosition(Map[0].Length + 2, 1);
            Console.Write($"elves count:  {Units.Where(u => !u.Dead && u.Type.Equals('E')).Count()}");

            Console.SetCursorPosition(Map[0].Length + 2, 2);
            Console.Write($"elves count:  {Units.Where(u => !u.Dead && u.Type.Equals('G')).Count()}");

            //Console.SetCursorPosition(Map[0].Length + 2, 3);
            //Console.Write("Game is paused, hit key for next round.");
            //Console.ReadKey();
        }

        private List<Unit> GetEnemiesInRange(Unit unit, List<Unit> units)
        {
            return units.Where(e => !e.Type.Equals(unit.Type) && e.Distance(unit) <= 1).ToList();
        }
     
        private Route GetRoute(int x, int y)
        {
            Route routeToTarget = new Route();
            routeToTarget.CurrentPosition = new Coordinates() { X = x, Y = y, Cost = 0 };

            List<Coordinates> reachableTiles = GetAllReachableTilesForUnit(x, y);
            Coordinates self = routeToTarget.CurrentPosition;
            reachableTiles.Add(self);
            foreach (Coordinates tile in reachableTiles)
            {
                tile.Visited = false;
                tile.Cost = int.MaxValue;
            }
            //set 
            Coordinates currentPosition = self;

            currentPosition.Cost = 0;
            do
            {
                currentPosition.Visited = true;
                //Update cost of  adjascent tiles
                List<Coordinates> adjascentTiles = reachableTiles.Where(r => r.Distance(currentPosition.X, currentPosition.Y) <= 1).ToList();
                foreach (Coordinates adjascentTile in adjascentTiles.Where(t => !t.Visited))
                {
                    int calculatedCost = currentPosition.Cost + 1;
                    if (calculatedCost < adjascentTile.Cost)
                    {
                        adjascentTile.Cost = calculatedCost;
                    }
                }
                //pick cheapest one
                currentPosition = adjascentTiles.Where(r => !r.Visited).OrderBy(a => a.Cost).FirstOrDefault();
                if (currentPosition == null)
                {
                    currentPosition = reachableTiles.Where(r => !r.Visited).OrderBy(r => r.Cost).FirstOrDefault();
                }
            } while (reachableTiles.Any(r => !r.Visited));

            PrintDistances(x, y, reachableTiles);
            routeToTarget.ReachableTiles = reachableTiles;
            
            return routeToTarget;

        }
        private Route GetRoute(Unit unit, Coordinates target)
        {
            Route routeToTarget = GetRoute(target.X, target.Y);            

            PrintDistances(unit.X, unit.Y, routeToTarget.ReachableTiles);

            //Check the four directions, if there is a tie, pick the reading order
            routeToTarget.FirstStep = routeToTarget.ReachableTiles
                .Where(r => r.Distance(unit.X, unit.Y) <= 1)
                .OrderBy(r => r.Cost)
                .ThenBy(r => r.Y)
                .ThenBy(r => r.X).First();

            return routeToTarget;
        }

        private void PrintDistances(int x, int y, List<Coordinates> reachableTiles)
        {
            PrintMap();

            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (Coordinates coordinates in reachableTiles)
            {
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(x, y);
            Console.Write('@');
            Console.ResetColor();
        }



        private List<Coordinates> GetAllReachableTilesForUnit(int x, int y)
        {
            //remove the tiles occupied by unit
            List<Coordinates> currentlyWalkableCoordinates = WalkableCoordinates.Where(w => !Units.Where(u => !u.Dead).Any(u => u.X.Equals(w.X) && u.Y.Equals(w.Y))).ToList();

            List<Coordinates> reachableTiles = new List<Coordinates>();
            int lastNumberOfReachableTiles;
            do
            {
                lastNumberOfReachableTiles = reachableTiles.Count();
                foreach (Coordinates c in currentlyWalkableCoordinates.Where(w => !reachableTiles.Contains(w)))
                {
                    if (reachableTiles.Any(r => r.Distance(c.X, c.Y) <= 1) || c.Distance(x, y) <= 1)
                    {
                        reachableTiles.Add(c);
                    }
                }
            } while (reachableTiles.Count > lastNumberOfReachableTiles);
            return reachableTiles;
        }

        private List<Coordinates> GetAdjascentEmptyTiles(List<Unit> enemies)
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
            return input.Count.ToString();
        }
    }
}