using System;

namespace AdventOfCode2018.Day13
{
    internal class Cart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public Direction? LastTurn { get; set; }

        public Cart(int x, int y, Direction d)
        {
            X = x;
            Y = y;
            Direction= d;
            LastTurn = Direction.Right;
        }

        internal void Turn()
        {
            //left straight right, repeat
            switch (LastTurn)
            {
                case null:
                    LastTurn = Direction.Right;
                    //Turn right
                    Direction = GetRightDirection();
                    break;
                case Direction.Right:
                    LastTurn = Direction.Left;
                    //Turn left
                    Direction = GetLeftDirection();
                    break;
                case Direction.Left:
                    LastTurn = null;
                    //Straight
                    break;
            }
        }

        private Direction GetRightDirection()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                    break;
                case Direction.Down:
                    return Direction.Left;
                    break;
                case Direction.Left:
                    return Direction.Up;
                    break;
                default:
                    break;
            }
            throw new Exception("No direction possible!");
        }

        private Direction GetLeftDirection()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return Direction.Left;
                case Direction.Right:
                    return Direction.Up;
                    break;
                case Direction.Down:
                    return Direction.Right;
                    break;
                case Direction.Left:
                    return Direction.Down;
                    break;
                default:
                    break;
            }
            throw new Exception("No direction possible!");
        }

        internal void Move(char[][] map)
        {
            switch (Direction)
            {
                case Direction.Up:
                    Y--;
                    switch (map[Y][X])
                    {
                        case '+':
                            Turn();
                            break;
                        case '/':
                            Direction = Direction.Right;
                            break;
                        case '\\':
                            Direction = Direction.Left;
                            break;
                    }
                    break;
                case Direction.Right:
                    X++;
                    switch (map[Y][X])
                    {
                        case '+':
                            Turn();
                            break;
                        case '/':
                            Direction = Direction.Up;
                            break;
                        case '\\':
                            Direction = Direction.Down;
                            break;
                    }
                    break;
                case Direction.Down:
                    Y++;
                    switch (map[Y][X])
                    {
                        case '+':
                            Turn();
                            break;
                        case '/':
                            Direction = Direction.Left;
                            break;
                        case '\\':
                            Direction = Direction.Right;
                            break;
                    }
                    break;
                case Direction.Left:
                    X--;
                    switch (map[Y][X])
                    {
                        case '+':
                            Turn();
                            break;
                        case '/':
                            Direction = Direction.Down;
                            break;
                        case '\\':
                            Direction = Direction.Up;
                            break;
                    }
                    break;
            }
        }
    }
}