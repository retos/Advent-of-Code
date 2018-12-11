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

            int powerHigh = 0;
            string cordinatesHigh = string.Empty;
            for (int i = 3; i <= 300; i++)
            {
                Console.SetCursorPosition(0, consoleYPosition+1);
                int currentPower = 0;
                string coordinates = GetCoordinatesOfLargestPowerSquare(i, ref currentPower);
                Console.WriteLine($"Current coordinates: {coordinates} power: {currentPower.ToString()} square: {i}                  ");
                if (currentPower > powerHigh)
                {
                    powerHigh = currentPower;
                    cordinatesHigh = coordinates;
                    Console.WriteLine($"Highest coordinates: {cordinatesHigh} power: {powerHigh.ToString()} square: {i}                  ");
                }
            }
            Console.SetCursorPosition(consoleXPosition, consoleYPosition);
            return $"{cordinatesHigh},{powerHigh.ToString()}";
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