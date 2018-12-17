using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2018.Day17
{
    internal class MapManager
    {
        private List<Instruction> instructions = new List<Instruction>();
        public char[][] Map { get; set; }
        public int XDimensionStart { get; set; }
        public int XDimensionEnd { get; set; }
        public int XLength { get; set; }
        public int YDimensionStart { get; set; }
        public int YDimensionEnd { get; set; }
        public int YLength { get; set; }
        public int WaterCount
        {
            get {
                int waterCount = 0;
                for (int x = 0; x < Map.Length; x++)
                {
                    for (int y = TranslateY(instructions.Min(i => i.StartY)); y < Map[0].Length; y++)
                    {
                        if (Map[x][y].Equals('~') || Map[x][y].Equals('|'))
                        {
                            waterCount++;
                        }
                    }
                }
                return waterCount;
            }
        }
        public int WaterCountStillWater
        {
            get
            {
                int waterCount = 0;
                for (int x = 0; x < Map.Length; x++)
                {
                    for (int y = TranslateY(instructions.Min(i => i.StartY)); y < Map[0].Length; y++)
                    {
                        if (Map[x][y].Equals('~'))
                        {
                            waterCount++;
                        }
                    }
                }
                return waterCount;
            }
        }
        private bool isTestRun = false;
        public MapManager(List<string> input, bool isTestRun)
        {
            this.isTestRun = isTestRun;
            instructions = new List<Instruction>();
            foreach (string line in input)
            {
                Instruction i = new Instruction(line);
                instructions.Add(i);
            }

            XDimensionStart = instructions.Min(i => i.StartX) - 1;//+1 links aussen
            XDimensionEnd = instructions.Max(i => i.EndX) + 1;//+1 rechts aussen
            XLength = XDimensionEnd - XDimensionStart;

            YDimensionStart = 0;
            YDimensionEnd = instructions.Max(i => i.EndY);
            YLength = YDimensionEnd - YDimensionStart;

            Map = new char[XLength + 1][];
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i] = new char[YLength + 1];
            }

            foreach (Instruction i in instructions)
            {
                for (int x = i.StartX - XDimensionStart; x <= i.EndX - XDimensionStart; x++)
                {
                    for (int y = i.StartY - YDimensionStart; y <= i.EndY - YDimensionStart; y++)
                    {
                        Map[x][y] = '#';
                    }
                }
            }
        }

        public void PourWater(int waterPositionX, int waterPositionY)
        {
            //Go down until clay
            char currentTile;
            do
            {
                if (IsOnMap(waterPositionX, waterPositionY))
                {
                    currentTile = GetChar(waterPositionX, waterPositionY);
                    if (currentTile.Equals('#') || currentTile.Equals('~'))
                    {
                        waterPositionY--;//Go one up, since we found clay or water
                        break;
                    }
                    else if (currentTile.Equals('|'))
                    {
                        break; //We found a flowing water level -> ignore
                    }
                    else
                    {
                        SetChar(waterPositionX, waterPositionY, '|');
                        waterPositionY++;
                    }
                }
            } while (IsOnMap(waterPositionX, waterPositionY));

            //go left an right and check if there are clay boundaries
            if (IsOnMap(waterPositionX,waterPositionY))
            {
                currentTile = GetChar(waterPositionX, waterPositionY);
                if (!currentTile.Equals('~'))
                {
                    //Only go left and right if there is not aleady water
                    LevelWater(waterPositionX, waterPositionY);
                }
            }
        }

        private bool IsOnMap(int waterPositionX, int waterPositionY)
        {
            return waterPositionX >= XDimensionStart && waterPositionX <= XDimensionEnd &&
                waterPositionY >= YDimensionStart && waterPositionY <= YDimensionEnd;
        }

        private void LevelWater(int waterPositionX, int waterPositionY)
        {
            int leftBoundary = waterPositionX;
            bool leftBoundaryExists = false;
            bool foundHoleLeft = false;
            do
            {
                int nextLeftBoundary = leftBoundary - 1;
                if (IsOnMap(nextLeftBoundary, waterPositionY))
                {
                    char tileToTheLeft = GetChar(nextLeftBoundary, waterPositionY);
                    char tileToTheLeftBelow = GetChar(nextLeftBoundary, waterPositionY + 1);
                    if (!tileToTheLeft.Equals('#') && (tileToTheLeftBelow.Equals('#') || tileToTheLeftBelow.Equals('~')))
                    {
                        leftBoundary = nextLeftBoundary;
                    }
                    else if (tileToTheLeft.Equals('|') || tileToTheLeft.Equals('~'))
                    {
                        break;
                    }
                    else if (tileToTheLeft.Equals('#'))
                    {
                        leftBoundaryExists = true;
                    }
                    else if (tileToTheLeftBelow.Equals('\0'))
                    {
                        foundHoleLeft = true;
                    }
                }
                else
                {//Outside map
                    break;
                }
                
            } while (!leftBoundaryExists && !foundHoleLeft);

            int rightBoundary = waterPositionX;
            bool rightBoundaryExists = false;
            bool foundHoleRight = false;
            do
            {
                int nextRightBoundary = rightBoundary + 1;
                if (IsOnMap(nextRightBoundary, waterPositionY))
                {
                    char tileToTheRight = GetChar(nextRightBoundary, waterPositionY);
                    char tileToTheRightBelow = GetChar(nextRightBoundary, waterPositionY + 1);
                    if (!tileToTheRight.Equals('#') && (tileToTheRightBelow.Equals('#') || tileToTheRightBelow.Equals('~')))
                    {
                        rightBoundary = nextRightBoundary;
                    }
                    else if (tileToTheRight.Equals('|') || tileToTheRight.Equals('~'))
                    {
                        break;
                    }
                    else if (tileToTheRight.Equals('#'))
                    {
                        rightBoundaryExists = true;
                    }
                    else if (tileToTheRightBelow.Equals('\0'))
                    {
                        foundHoleRight = true;
                    }
                }
                else
                {//outside map
                    break;
                }

            } while (!rightBoundaryExists && !foundHoleRight);

            if (leftBoundaryExists && rightBoundaryExists && !foundHoleLeft && !foundHoleRight)
            {
                for (int x = leftBoundary; x <= rightBoundary; x++)
                {
                    SetChar(x, waterPositionY, '~');
                }
                //TODO Level for all positions? So far only for the location of the waterentrance
                char tileAbove = GetChar(waterPositionX, waterPositionY - 1);
                if (!tileAbove.Equals('~'))
                {
                    LevelWater(waterPositionX, waterPositionY-1);
                }
            }
            if (foundHoleRight || foundHoleLeft)
            {
                //Fill with | water and pour
                for (int x = leftBoundary; x <= rightBoundary; x++)
                {
                    SetChar(x, waterPositionY, '|');
                }
                if (foundHoleLeft)
                {
                    PourWater(leftBoundary-1, waterPositionY);
                }
                if (foundHoleRight)
                {
                    PourWater(rightBoundary+1, waterPositionY);
                }
            }
        }

        private void SetChar(int waterPositionX, int waterPositionY, char c)
        {
            Map[TranslateX(waterPositionX)][TranslateY(waterPositionY)] = c;
            //PrintMap();
        }

        private char GetChar(int waterPositionX, int waterPositionY)
        {
            return Map[TranslateX(waterPositionX)][TranslateY(waterPositionY)];
        }

        private int TranslateY(int waterPositionY)
        {
            return waterPositionY - YDimensionStart;
        }

        private int TranslateX(int waterPositionX)
        {
            return waterPositionX - XDimensionStart;
        }

        internal void PrintMap()
        {
            int xPos = 0;
            if (isTestRun)
            {
                for (int x = 0; x < Map.Length; x++)
                {
                    for (int y = 0; y < Map[0].Length; y++)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(Map[x][y]);
                    }
                }
                xPos = Map.Length + 2;
            }
            
            Console.SetCursorPosition(xPos, 0);            
            Console.Write($"amout of water: {WaterCount}          ");

            Console.SetCursorPosition(500 - XDimensionStart, 0);
            Console.Write('+');

        }

        internal void GeneratePicture(int? xMarker, int? yMarker)
        {
            Bitmap bitmap = new Bitmap(Map.Length + 1, Map[0].Length + 1);
            for (int x = 0; x < Map.Length; x++)
            {
                for (int y = 0; y < Map[0].Length; y++)
                {
                    Color fontColor = Color.White;
                    if (Map[x][y].Equals('|'))
                    {
                        fontColor = Color.Blue;
                    }
                    if (Map[x][y].Equals('~'))
                    {
                        fontColor = Color.Green;
                    }
                    else if(Map[x][y].Equals('#'))
                    {
                        fontColor = Color.Black;
                    }
                    bitmap.SetPixel(x, y, fontColor);
                }
            }
            if (xMarker != null && yMarker != null)
            {
                bitmap.SetPixel(TranslateX((int)xMarker), TranslateY((int)yMarker), Color.Red);
            }

            bitmap.Save($"Day17 {isTestRun}.png");
        }
    }
}