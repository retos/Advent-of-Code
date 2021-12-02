using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day05
{
    internal class BoardingPass
    {
        public string SpacePartitioning { get; private set; }
        private int? row;
        private int? column;
        private int? seatId;
        public int Row 
        { 
            get
            {
                if (row != null)
                {
                    return (int)row;
                }
                string rowInfo = SpacePartitioning.Substring(0, 7);
                int lowerLimit = 0;
                int upperLimit = 127;
                foreach (char c in rowInfo)
                {
                    if (c == 'F')
                    {
                        upperLimit = ((upperLimit - lowerLimit) / 2) + lowerLimit;
                    }
                    else
                    {
                        if (c != 'B')
                        {
                            throw new FormatException("Unexpected format!");
                        }

                        lowerLimit = (((upperLimit - lowerLimit) / 2) + lowerLimit) + 1;
                    }
                }

                row = lowerLimit;
                return lowerLimit;
            }
        }

        public int Column
        {
            get
            {
                if (column != null)
                {
                    return (int)column;
                }
                string colInfo = SpacePartitioning.Substring(7);
                int lowerLimit = 0;
                int upperLimit = 7;
                foreach (char c in colInfo)
                {
                    if (c == 'L')
                    {
                        upperLimit = ((upperLimit - lowerLimit) / 2) + lowerLimit;
                    }
                    else
                    {
                        if (c != 'R')
                        {
                            throw new FormatException("Unexpected format!");
                        }

                        lowerLimit = (((upperLimit - lowerLimit) / 2) + lowerLimit) + 1;
                    }
                }

                column = lowerLimit;
                return lowerLimit;
            }
        }

        public int SeatId 
        {
            get 
            {
                if (seatId != null)
                {
                    return (int)seatId;
                }
                seatId = Row * 8 + Column;
                return (int)seatId;
            }
        }

        internal static List<BoardingPass> Convert(List<string> input)
        {
            List<BoardingPass> passes = new List<BoardingPass>();
            foreach (string line in input)
            {
                BoardingPass pass = new BoardingPass { SpacePartitioning = line };
                passes.Add(pass);
            }
            return passes;
        }
    }
}