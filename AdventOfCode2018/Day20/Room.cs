using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day20
{
    internal class Room
    {
        private Hashtable map;
        public List<string> RoutesToRoom { get; set; }
        public Room(ref Hashtable map, int x, int y)
        {
            ShortestPath = int.MaxValue;
            RoutesToRoom = new List<string>();
            DirectionsStartingInThisRoom = new HashSet<Directions>();
            AdjascentRooms = new List<Room>();
            X = x;
            Y = y;
            this.map = map;
            map.Add(this.Key, this);
        }

        public static Room CreateRoom(ref Hashtable map, int x, int y, string route)
        {
            //Only create and add if not already on map
            if (!map.ContainsKey(GenerateKey(x,y)))
            {
                Room newRoom = new Room(ref map, x, y);
                newRoom.RoutesToRoom.Add(route);
                //TODO Distinct? But definitely not here. ...performance
                return newRoom;
            }
            else
            {
                Room roomFromMap = ((Room)map[GenerateKey(x, y)]);
                if (!roomFromMap.RoutesToRoom.Contains(route))
                {
                    roomFromMap.RoutesToRoom.Add(route);
                }
                ////Check if there is a shorter path, from direct neighbor
                //string shortestPath = CalculateShortestPath(roomFromMap);
                //if (shortestPath.Length < roomFromMap.RoutesToRoom.OrderBy(r => r.Length).First().Length)
                //{
                //    roomFromMap.RoutesToRoom.Add(shortestPath);
                //}
                ////TODO should also be done to direct neighbors?
                return roomFromMap;
            }
        }

        internal int Dijikstra(Room roomWithLongestPath, List<Room> roomList)
        {
            Room currentRoom = this;
            currentRoom.ShortestPath = 0;

            do
            {
                currentRoom.Visited = true;
                //Update cost of  adjascent rooms
                foreach (Room adjascentRoom in currentRoom.AdjascentRooms.Where(r => !r.Visited))
                {
                    int calculatedCost = currentRoom.ShortestPath + 1;
                    if (calculatedCost < adjascentRoom.ShortestPath)
                    {
                        adjascentRoom.ShortestPath = calculatedCost;
                    }
                }
                //pick cheapest one
                currentRoom = roomList.Where(r => !r.Visited).OrderBy(r => r.ShortestPath).FirstOrDefault();
            } while (!roomWithLongestPath.Visited);
            return roomWithLongestPath.ShortestPath;
        }

        //private static string CalculateShortestPath(Room toRoom)
        //{
        //    Room adjascentRoomWithShortestPath = toRoom.AdjascentRooms.OrderBy(a => a.RoutesToRoom.Min(r => r.Length)).First();
        //    string shortestPath = adjascentRoomWithShortestPath.RoutesToRoom.OrderBy(a => a.Length).First();

        //    if (toRoom.X > adjascentRoomWithShortestPath.X)
        //    {
        //        return shortestPath + 'E';
        //    }
        //    else if (toRoom.X < adjascentRoomWithShortestPath.X)
        //    {
        //        return shortestPath + 'W';
        //    }
        //    else if (toRoom.Y > adjascentRoomWithShortestPath.Y)
        //    {
        //        return shortestPath + 'S';
        //    }
        //    else if (toRoom.Y < adjascentRoomWithShortestPath.Y)
        //    {
        //        return shortestPath + 'N';
        //    }
        //    throw new Exception("No possible direction found!");
        //}

        private static string GenerateKey(int x, int y)
        {
            return $"{x},{y}";
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string Key
        {
            get
            {
                return GenerateKey(X,Y);
            }
        }

        public List<Room> AdjascentRooms { get; set; }
        public HashSet<Directions> DirectionsStartingInThisRoom { get; set; }
        public bool Visited { get; private set; }
        public int ShortestPath { get; private set; }

        internal Room AddRoom(char c, string routeToRoom)
        {
            int x = GetX(c);
            int y = GetY(c);
            Room r = CreateRoom(ref map, x, y, routeToRoom);
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