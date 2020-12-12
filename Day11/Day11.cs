using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    public class Day11 : DayBase
    {
        private List<string> _seats;
        private int _maxNrOfOccupiedAdjacentSeats;
        private bool _extendedSearch;

        private const char FLOOR = '.';
        private const char EMPTY_SEAT = 'L';
        private const char OCCUPIED_SEAT = '#';

        public Day11() : base(11, "Seating System")
        {
        }

        // Correct Answer: 2338
        protected override void SolvePartOne()
        {
            _maxNrOfOccupiedAdjacentSeats = 4;
            _extendedSearch = false;

            RunSimulation();
        }

        // Correct Answer: 2134
        protected override void SolvePartTwo()
        {
            _maxNrOfOccupiedAdjacentSeats = 5;
            _extendedSearch = true;

            RunSimulation();
        }

        private void RunSimulation()
        {
            _seats = new List<string>(Input);

            var done = false;
            var passCount = 0;
            var print = false;

            if (print)
            {
                Console.WriteLine("INITIAL SEATINGS:");
                PrintSeats();
            }

            while (!done)
            {
                passCount++;
                var seatingsChanged = ApplySeatingRules();

                if (print)
                {
                    Console.WriteLine($"PASS {passCount} SEATINGS:");
                    PrintSeats();
                }

                done = !seatingsChanged;
            }

            var occupiedSeats = GetNrOfOccupiedSeats();

            Console.WriteLine($"Nr of passes: {passCount}");
            Console.WriteLine($"Answer: {occupiedSeats}");
        }

        private bool ApplySeatingRules()
        {
            var newSeats = new List<string>(_seats);
            var changed = false;

            for (int iRow = 0; iRow < _seats.Count; iRow++)
            {
                var seatRow = _seats[iRow];
                var newSeatRow = seatRow.ToCharArray();

                for (int iSeat = 0; iSeat < seatRow.Length; iSeat++)
                {
                    var seat = seatRow[iSeat];
                    var nrOfAdjacentOccupiedSeats = GetNrOfAdjacentOccupiedSeats(iRow, iSeat);

                    if (seat == EMPTY_SEAT && nrOfAdjacentOccupiedSeats == 0)
                    {
                        newSeatRow[iSeat] = OCCUPIED_SEAT;
                        changed = true;
                    }
                    else if (seat == OCCUPIED_SEAT && nrOfAdjacentOccupiedSeats >= _maxNrOfOccupiedAdjacentSeats)
                    {
                        newSeatRow[iSeat] = EMPTY_SEAT;
                        changed = true;
                    }
                }

                newSeats[iRow] = new string(newSeatRow);
            }

            _seats.Clear();
            _seats.AddRange(newSeats);

            return changed;
        }

        private int GetNrOfAdjacentOccupiedSeats(int row, int col)
        {
            var count = 0;

            if (HasAjacentOccupiedSeat(row, col, -1,  0)) count++; // LEFT
            if (HasAjacentOccupiedSeat(row, col, +1,  0)) count++; // RIGHT
            if (HasAjacentOccupiedSeat(row, col,  0, -1)) count++; // TOP
            if (HasAjacentOccupiedSeat(row, col,  0, +1)) count++; // DOWN
            if (HasAjacentOccupiedSeat(row, col, -1, -1)) count++; // TOP - LEFT
            if (HasAjacentOccupiedSeat(row, col, -1, +1)) count++; // TOP - RIGHT
            if (HasAjacentOccupiedSeat(row, col, +1, -1)) count++; // DOWN - LEFT
            if (HasAjacentOccupiedSeat(row, col, +1, +1)) count++; // DOWN - RIGHT

            return count;
        }

        private bool HasAjacentOccupiedSeat(int row, int col, int rowOffset, int colOffset)
        {
            var result = false;
            var done = false;

            var newRow = row;
            var newCol = col;

            while (!done)
            {
                newRow += rowOffset;
                newCol += colOffset;

                done = true;

                if (newRow >= 0 && newRow < _seats.Count && newCol >= 0 && newCol < _seats[row].Length)
                {
                    var adjacent = _seats[newRow][newCol];
                    result = adjacent == OCCUPIED_SEAT;

                    if (_extendedSearch && adjacent == FLOOR)
                    {
                        done = false;
                    }
                }
            }

            return result;
        }

        private int GetNrOfOccupiedSeats()
        {
            var count = 0;

            foreach (var line in _seats)
            {
                count += line.Count(x => x == OCCUPIED_SEAT);
            }

            return count;
        }

        private void PrintSeats()
        {
            foreach (var line in _seats)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}
