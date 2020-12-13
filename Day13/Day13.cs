using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public class Day13 : DayBase
    {
        private long _startTime;
        private List<int> _busses;
        private Dictionary<int, int> _busOffsets;

        public Day13() : base(13, "Shuttle Search")
        {
            _busses = new List<int>();
            _busOffsets = new Dictionary<int, int>();
        }

        protected override void BeforeSolve()
        {
            var busses = InputLine(1).Split(',');
         
            _startTime = long.Parse(InputLine(0));
            _busses.Clear();
            _busses.AddRange(busses.Where(x => x != "x")
                                   .Select(x => int.Parse(x))
                                   .ToArray());

            _busOffsets.Clear();
            for (int i = 0; i < busses.Length; i++)
            {
                if (busses[i] != "x") _busOffsets.Add(int.Parse(busses[i]), i);
            }
        }

        // Correct Answer: 2382
        protected override void SolvePartOne()
        {
            var done = false;
            var earliestBus = 0;
            var earliestTime = _startTime;

            while (!done)
            {
                foreach (var bus in _busses)
                {
                    done = DoesBusDepart(earliestTime, bus);
                    if (done)
                    {
                        earliestBus = bus;
                        break;
                    }
                }

                if (!done) earliestTime++;
            }

            var diffMinutes = earliestTime - _startTime;
            Console.WriteLine($"Earliest departure: {earliestTime} using bus {earliestBus} (Waittime is {diffMinutes} minutes)");
            Console.WriteLine($"Answer: {diffMinutes * earliestBus}");
        }

        protected override void SolvePartTwo()
        {
            var matchingBusses = FindMatchingBusses();
            if (matchingBusses == null) throw new Exception("No matching busses found!");

            var bus = matchingBusses.Item1;
            var busOffset = _busses.IndexOf(bus);

            var busTest = matchingBusses.Item2;
            var busTestOffset = _busses.IndexOf(busTest);

            Console.WriteLine("TODO");
        }

        private bool DoesBusDepart(long time, long bus)
        {
            return time % bus == 0;
        }

        private Tuple<int, int> FindMatchingBusses()
        {
            for (int i = 0; i < _busses.Count-1; i++)
            {
                var bus = _busses[i];
                for (int j = i+1; j < _busses.Count; j++)
                {
                    var busTest = _busses[j];
                    if (_busOffsets[busTest] == bus) return new Tuple<int, int>(bus, busTest);
                }

            }

            return null;
        }

        private long FindLCM(int bus1, int bus2, long offset = 0)
        {
            var bus = bus1 > bus2 ? bus1 : bus2;
            var busTest = bus1 > bus2 ? bus2 : bus1;

            if (bus % busTest == 0) return busTest;

            var multiplier = offset / bus;
            var done = false;
            long lcm = 0;

            while(!done)
            {
                multiplier++;
                long testLCM = bus * multiplier;

                done = testLCM % busTest == 0;
                if (done) lcm = testLCM;
            }

            return lcm;
        }
    }
}
