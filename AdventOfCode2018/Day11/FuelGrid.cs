using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day11
{
    internal class FuelGrid
    {
        public int GridId { get; set; }
        public int Dimensions { get; set; }
        public FuelCell[][] FuelCells { get; set; }

        public FuelGrid(int id, int dimensions)
        {
            GridId = id;
            Dimensions = dimensions;
            FuelCells = new FuelCell[dimensions][];

            //Init fuel cells
            for (int x = 1; x <= dimensions; x++)
            {
                FuelCells[x-1] = new FuelCell[dimensions];
                for (int y = 1; y <= dimensions; y++)
                {
                    FuelCell cell = new FuelCell(x, y, GridId);
                    FuelCells[x-1][y-1] = cell;
                }
            }
        }

        internal string GetCoordinatesOfLargestPowerSquareDynamic()
        {
            int consoleXPosition = Console.CursorLeft;
            int consoleYPosition = Console.CursorTop;

            int squaresize;
            int powerHigh = 0;
            int powerHighLastSquareSize = 0;
            string cordinatesHigh = string.Empty;
            for (squaresize = 3; squaresize <= 300; squaresize++)
            {
                Console.SetCursorPosition(0, consoleYPosition+1);
                int currentPower = 0;
                string coordinates = GetCoordinatesOfLargestPowerSquare(squaresize, ref currentPower);
                Console.WriteLine($"Current coordinates: {coordinates} power: {currentPower.ToString()} square: {squaresize}                  ");
                if (currentPower > powerHigh)
                {
                    powerHigh = currentPower;
                    cordinatesHigh = coordinates;
                    Console.WriteLine($"Highest coordinates: {cordinatesHigh} power: {powerHigh.ToString()} square: {squaresize}                  ");
                }

                powerHighLastSquareSize = currentPower;
                if (powerHighLastSquareSize < powerHigh)
                {
                    break;
                }
            }
            Console.SetCursorPosition(0, consoleYPosition + 1);
            Console.WriteLine("                                                                                             ");
            Console.WriteLine("                                                                                             ");
            Console.SetCursorPosition(consoleXPosition, consoleYPosition);
            return $"{cordinatesHigh},{squaresize-1}";
        }

        internal string GetCoordinatesOfLargestPowerSquare(int gridSize, ref int power)
        {
            int largeY = 0;
            int largeX = 0;
            int largePower = int.MinValue;
            for (int x = 1; x <= Dimensions-gridSize; x++)
            {
                for (int y = 1; y <= Dimensions-gridSize; y++)
                {
                    int gridPower = GetGridPower(x, y, gridSize);
                    if (gridPower > largePower)
                    {
                        largePower = gridPower;
                        largeX = x;
                        largeY = y;
                    }
                }
            }
            power = largePower;
            return $"{largeX},{largeY}";
        }

        private int GetGridPower(int xTop, int yTop, int gridSize)
        {
            int gridPower = 0;
            for (int x = xTop; x < gridSize+xTop; x++)
            {
                for (int y = yTop; y < gridSize+yTop; y++)
                {
                    gridPower += FuelCells[x-1][y-1].PowerLevel;
                }
            }
            return gridPower;
        }
    }
}