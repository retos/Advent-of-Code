using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode2022.Day13;

internal class Day14 : DayBase
{
    public override string Title => "--- Day 14: Regolith Reservoir ---";
    public override bool Ignore => true;
    public char[,] Map { get; set; }
    public int XMapCount { get; set; }
    public int YMapCount { get; set; }
    public int XOffset { get; set; }
    private bool debugPrintEnabled = false;


    public override string Part1(List<string> input, bool isTestRun)
    {
        List<RockStructure> structures = RockStructure.ReadInput(input);

        XMapCount = structures.Max(s => s.XMax) - structures.Min(s => s.XMin)+1;
        YMapCount = structures.Max(s => s.YMax)+1;
        XOffset = XMapCount - structures.Max(s => s.XMax) - 1;

        Map = new char[XMapCount, YMapCount];

        //init with air
        for (int y = 0; y < YMapCount; y++)
        {
            for (int x = 0; x < XMapCount; x++)
            {
                Map[x, y] = '.';
            }
        }

        //draw structures
        foreach (RockStructure structure in structures)
        {
            for (int x = structure.XMin; x <= structure.XMax; x++)
            {
                for (int y = structure.YMin; y <= structure.YMax; y++)
                {
                    Map[x + XOffset, y] = '#';
                }
            }
        }

        //draw sand inlet
        Map[500+XOffset, 0] = '+';
        if (debugPrintEnabled) { PrintToConsole(); }

        bool sandHasRoom = true;

        do
        {
            //add new sand
            int sandX = 500;
            int sandY = 0;

            if(Fall(ref sandX, ref sandY))
            {
                Map[sandX + XOffset, sandY] = 'o';
            }
            else
            {
                sandHasRoom = false;
            }

        } while (sandHasRoom);

        if (debugPrintEnabled) { PrintToConsole(); }

        //count sand
        int sandAmount = 0;
        for (int y = 0; y < YMapCount; y++)
        {
            for (int x = 0; x < XMapCount; x++)
            {
                if(Map[x, y] == 'o')
                {
                    sandAmount++;
                }
            }
        }

        return $" Amount of sand {sandAmount}";
    }

    private bool Fall(ref int sandX, ref int sandY, bool part2 = false)
    {
        //can it fall? -> do so, repeat
        bool roomToFall = true;
        while (roomToFall)
        {
            if (!IsOnMap(sandX, sandY + 1, part2)) { return false; }
            if(HasRoom(sandX, sandY + 1))
            {
                sandY++;
                if (debugPrintEnabled) { PrintToConsole(sandX, sandY); }
            }
            else
            {
                roomToFall = false;
            }
        }

        //no room below -> try below left
        if (!IsOnMap(sandX - 1, sandY + 1, part2))
        {
            return false;
        }
        if (HasRoom(sandX - 1, sandY + 1))
        {
            sandX--;
            sandY++;
            if (debugPrintEnabled) { PrintToConsole(sandX, sandY); }
            return Fall(ref sandX, ref sandY, part2);
        }
        //no room below -> try below right
        if (!IsOnMap(sandX + 1, sandY + 1, part2))
        {
            return false;
        }
        else if (HasRoom(sandX + 1, sandY + 1))
        {
            sandX++;
            sandY++;
            if (debugPrintEnabled) { PrintToConsole(sandX, sandY); }
            return Fall(ref sandX, ref sandY, part2);
        }
        if(sandX == 500 && sandY == 0) 
        {
            //Sand gets to rest exactly on the sand inlet
            Map[sandX + XOffset, sandY] = 'o';
            return false ; 
        }
        return true;
    }

    private bool IsOnMap(int x, int y, bool increaseMapIfNeeded = false)
    {
        if (x + XOffset < 0 || x + XOffset >= XMapCount)
        {
            if(increaseMapIfNeeded)
            {
                if (debugPrintEnabled)
                {
                    Console.WriteLine("Old map:");
                    PrintToConsole(x, y);
                }
                //create new larger map
                if (x + XOffset < 0)
                {
                    //increase left
                    IncreaseMap(true);
                }
                else
                {
                    //increase right
                    IncreaseMap(false);
                }
                if (debugPrintEnabled) 
                { 
                    Console.WriteLine("New map:");
                    PrintToConsole(x, y); 
                }
                return true;//map increased therefore it has room...
            }

            return false;
        }
        if (y < 0 || y >= YMapCount) 
            return false;
        return true;
    }

    private void IncreaseMap(bool left)
    {
        XMapCount++;
        char[,] newMap = new char[XMapCount, YMapCount];

        //copy over
        for (int y = 0; y < YMapCount; y++)
        {
            for (int x = 0; x < XMapCount; x++)
            {
                //PrintToConsole(x, y, newMap);
                if (left)
                {
                    if (x == 0) 
                    {
                        if (y == YMapCount-1) { newMap[x, y] = '#'; }//new floor
                        else { newMap[x, y] = '.'; } //new column with air
                    }
                    else { newMap[x, y] = Map[x-1, y]; }                    
                }
                else 
                {
                    if (x == XMapCount-1)
                    {
                        if (y == YMapCount - 1) { newMap[x, y] = '#'; }//new floor
                        else { newMap[x, y] = '.'; }//new column with air

                    }
                    else { newMap[x, y] = Map[x, y]; }
                }
            }
        }
        if (left) { XOffset++; }
        Map = newMap;
    }

    private bool HasRoom(int x, int y)
    {
        if (Map[x + XOffset, y] == '.')
        {
            return true;
        }
        return false;
    }

    private void PrintToConsole(int? sandX = null, int? sandY = null, char[,] printMap = null)
    {
        if (printMap == null)
        {
            printMap = Map;
        }
        Console.WriteLine();
        for (int y = 0; y < printMap.GetLength(1); y++)
        {
            for (int x = 0; x < printMap.GetLength(0); x++)
            {
                
                if (x == sandX+XOffset && y == sandY)
                {
                    //Print given coordinates as sand
                    ConsoleColor foreground = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write('o');
                    Console.ForegroundColor = foreground;
                }
                else
                {
                    //else, print what is on this tile of the map
                    Console.Write(printMap[x, y]);
                }
            }
            Console.WriteLine();
        }
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<RockStructure> structures = RockStructure.ReadInput(input);

        XMapCount = structures.Max(s => s.XMax) - structures.Min(s => s.XMin) + 1;
        YMapCount = structures.Max(s => s.YMax) + 3;
        XOffset = XMapCount - structures.Max(s => s.XMax) - 1;

        Map = new char[XMapCount, YMapCount];

        //init with air
        for (int y = 0; y < YMapCount; y++)
        {
            for (int x = 0; x < XMapCount; x++)
            {
                if (y == YMapCount-1)
                {
                    Map[x, y] = '#';
                }
                else
                {
                    Map[x, y] = '.';
                }
            }
        }

        if (debugPrintEnabled) { PrintToConsole(); }

        //draw structures
        foreach (RockStructure structure in structures)
        {
            for (int x = structure.XMin; x <= structure.XMax; x++)
            {
                for (int y = structure.YMin; y <= structure.YMax; y++)
                {
                    Map[x + XOffset, y] = '#';
                }
            }
        }

        //draw sand inlet
        Map[500 + XOffset, 0] = '+';
        if (debugPrintEnabled) { PrintToConsole(); }

        bool sandHasRoom = true;

        do
        {
            //add new sand
            int sandX = 500;
            int sandY = 0;

            if (Fall(ref sandX, ref sandY, true))
            {
                Map[sandX + XOffset, sandY] = 'o';
            }
            else
            {
                sandHasRoom = false;
            }

        } while (sandHasRoom);

        if (debugPrintEnabled) { PrintToConsole(); }

        //count sand
        int sandAmount = 0;
        for (int y = 0; y < YMapCount; y++)
        {
            for (int x = 0; x < XMapCount; x++)
            {
                if (Map[x, y] == 'o')
                {
                    sandAmount++;
                }
            }
        }
        return $" Amount of sand {sandAmount}";
    }
}