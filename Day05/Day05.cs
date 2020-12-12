using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2020.Day05
{
    public class Day05 : DayBase
    {
        private List<Seat> _seats;

        public Day05() : base(5, "Binary Boarding")
        {
            _seats = new List<Seat>();
        }

        protected override void BeforeSolve()
        {
            _seats.Clear();
            foreach (var line in Input)
            {
                var seat = DetermineSeat(line);
                _seats.Add(seat);
                //Console.WriteLine($"{line} - Row: {seat.Row}, Column: {seat.Column}, SeatID: {seat.ID}");
            }
            _seats.Sort();
        }

        // Correct Answer: 908
        protected override void SolvePartOne()
        {
            var seatID = _seats[_seats.Count-1].ID;
            Console.WriteLine($"Answer: {seatID}");
        }

        // Correct Answer: 619
        protected override void SolvePartTwo()
        {
            var seatID = _seats[0].ID;
            foreach (var seat in _seats)
            {
                if (seat.ID != seatID) break;
                seatID++;
            }

            Console.WriteLine($"Answer: {seatID}");
        }


        private Seat DetermineSeat(string boardingPass)
        {
            var row = boardingPass.Substring(0, 7);
            var column = boardingPass.Substring(7);

            return new Seat
            {
                Row = FindIndex(row, 'F', 'B'),
                Column = FindIndex(column, 'L', 'R')
            };
        }

        private int FindIndex(string pattern, char left, char right)
        {
            var max = (int) Math.Pow(2, pattern.Length);
            var index = 0;

            foreach (var c in pattern)
            {
                max /= 2;
                if (c == right) index += max;
            }

            return index;
        }
    }

    internal class Seat : IComparable<Seat>
    {
        public int ID { get => Row * 8 + Column; }
        public int Row { get; set; }
        public int Column{ get; set; }

        public int CompareTo([AllowNull] Seat other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}
