using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day20
{
    internal class Room
    {
        private Hashtable map;

        public Room(ref Hashtable map, int x, int y)
        {
            AdjascentRooms = new List<Room>();
            X = x;
            Y = y;
            this.map = map;
            map.Add(this.Key, this);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Key
        {
            get
            {
                return $"{X},{Y}";
            }
        }

        public List<Room> AdjascentRooms { get; set; }

        internal Room AddRoom(char c)
        {
            int x = GetX(c);
            int y = GetY(c);
            Room r;
            if (!map.ContainsKey($"{x},{y}"))
            {
                r = new Room(ref map, x, y);
            }
            else
            {
                r = ((Room)map[$"{x},{y}"]);
            }
            if (!AdjascentRooms.Contains(r))
            {
                AdjascentRooms.Add(r);
                r.AdjascentRooms.Add(this);
            }
            return r;
        }

        private int GetY(char c)
        {
            if (c.Equals('N'))
            {
                return Y - 1;
            }
            else if (c.Equals('S'))
            {
                return Y + 1;
            }
            return Y;
        }

        private int GetX(char c)
        {
            if (c.Equals('E'))
            {
                return X + 1;
            }
            else if (c.Equals('W'))
            {
                return X - 1;
            }
            return X;
        }
    }
}