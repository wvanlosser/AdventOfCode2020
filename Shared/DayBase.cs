using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2020.Shared
{
    public abstract class DayBase : IComparable<DayBase>
    {
        private int _dayNr;
        private string _description;
        private List<string> _input;

        public DayBase(int dayNr, string description)
        {
            _dayNr = dayNr;
            _description = description;
            _input = new List<string>();
        }

        public int GetDayNr() => _dayNr;
        public string GetDescription() => _description;
        public IEnumerable<string> Input => _input;
        public int InputCount => _input.Count;
        public string InputLine(int index) => _input[index];

        public void SolvePuzzles()
        {
            Stopwatch sw;

            Console.Clear();
            Console.WriteLine("***************************************************");
            Console.WriteLine($"**     Solving Day {_dayNr:00}                  **");
            Console.WriteLine("***************************************************");
            Console.WriteLine();

            ReadInput();
            Console.WriteLine($"Input contains of {_input.Count} lines");
            Console.WriteLine();

            sw = Stopwatch.StartNew();
            BeforeSolve();
            sw.Stop();
            var bsMs = sw.ElapsedMilliseconds;

            Console.WriteLine("Part One:");
            Console.WriteLine("------------------------------------------------");

            sw = Stopwatch.StartNew();
            SolvePartOne();
            sw.Stop();
            var p1Ms = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine("Part Two:");
            Console.WriteLine("------------------------------------------------");

            sw = Stopwatch.StartNew();
            SolvePartTwo();
            sw.Stop();
            var p2Ms = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine("Durations:");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"BeforeSolve : {bsMs} ms");
            Console.WriteLine($"Part One    : {p1Ms} ms");
            Console.WriteLine($"Part Two    : {p2Ms} ms");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
        }

        private void ReadInput()
        {
            var fileName = $".\\Day{_dayNr:00}\\input.txt";
            var lines = File.ReadLines(fileName);
            _input.Clear();
            _input.AddRange(lines);
        }

        protected virtual void BeforeSolve() { }
        protected abstract void SolvePartOne();
        protected abstract void SolvePartTwo();

        public int CompareTo(DayBase day)
        {
            return this._dayNr.CompareTo(day.GetDayNr());
        }
    }
}
