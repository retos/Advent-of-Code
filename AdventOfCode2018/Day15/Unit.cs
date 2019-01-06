using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day15
{
    internal class Unit
    {
        private List<Unit> units;
        public char Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Hitpoints { get; set; }
        public int Attackpower { get; set; }
        public bool Dead { get; internal set; }

        public Unit(int x, int y, char t, ref System.Collections.Generic.List<Unit> units, int attackPower)
        {
            this.units = units;
            X = x;
            Y = y;
            Type= t;
            Hitpoints = 200;
            Attackpower = attackPower;
        }

        internal int Distance(Unit otherUnit)
        {
            return Math.Abs(otherUnit.X - X) + Math.Abs(otherUnit.Y - Y);
        }

        internal Coordinates GetCoordinates()
        {
            return new Coordinates() { X = this.X, Y = this.Y };
        }

        internal void Step(Coordinates newPosition)
        {
            X = newPosition.X;
            Y = newPosition.Y;
        }

        internal bool IsInAttachRange()
        {
            return units.Any(e => !e.Type.Equals(this.Type) && !e.Dead && e.Distance(this) <= 1);
        }

        internal IEnumerable<Unit> GetEnemies()
        {
            return units.Where(u => !u.Dead).Where(u => !u.Type.Equals(this.Type));
        }

        internal IEnumerable<Unit> GetEnemiesInRange()
        {
            return GetEnemies().Where(e => e.Distance(this) == 1);
        }
    }
}