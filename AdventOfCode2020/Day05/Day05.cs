using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Day05
{
    internal class Day05 : DayBase
    {
        public override string Title => "--- Day 5: Binary Boarding ---";
        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {
            //OO classic approach
            //List<BoardingPass> passes = BoardingPass.Convert(input);
            //return $"The seat with the highest seat ID: {passes.Max(p => p.SeatId)}";

            //binary approach
            input = input.Select(p => p.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1')).OrderBy(i => i).ToList();
            return $"The seat with the highest seat ID: {Convert.ToInt32(input.Last(), 2)}";

        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            if (isTestRun) { return "does not work for testinput"; };

            //oo classic approach
            /*
            List<BoardingPass> passes = BoardingPass.Convert(input);
            //Grouping by row
            var g = passes.GroupBy(p => p.Row);
            //Counting the items per group
            var result = g.Select(p => new {
                Id = p.Key,
                Quantity = p.Sum(x => 1)
            });
            //The group with exactly one missing seat
            var row = result.Where(r => r.Quantity == 7);
            //The rowId where my seat is
            int rowID = row.First().Id;
            //put all seats in there, and then remove the ones already taken
            List<int> seats = new List<int>() { 0, 2, 3, 4, 5, 6, 7 };
            foreach (BoardingPass p in passes.Where(p => p.Row.Equals(rowID)))
            {
                seats.Remove(p.Column);
            }

            return $"My seat has the seatID: {rowID * 8 + seats.First()}";
            */

            //binary approach
            input = input.Select(p => p.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1')).OrderBy(i => i).ToList();
            for (int i = 0; i < input.Count(); i++)
            {
                if (Convert.ToInt32(input[i + 1], 2) - Convert.ToInt32(input[i], 2) != 1)
                {
                    return $"My seat has the seatID: {Convert.ToInt32(input[i], 2)+1}";
                }
            }
            return $"If you see this, there was no solution!";
        }
    }
}
