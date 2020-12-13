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

        // Correct Answer: 906332393333683
        // Used help from Reddit
        protected override void SolvePartTwo()
        {
            long earliestTime = 0;
            long inc = _busses[0];

            for (int i = 1; i < _busses.Count; i++)
            {
                var nextBus = _busses[i];
                var nextBusOffset = _busOffsets[nextBus];

                while(true)
                {
                    earliestTime += inc;
                    if ((earliestTime + nextBusOffset) % nextBus == 0)
                    {
                        inc *= nextBus;
                        break;
                    }
                }
            }

            Console.WriteLine($"Answer: {earliestTime}");
        }

        private bool DoesBusDepart(long time, long bus)
        {
            return time % bus == 0;
        }
    }
}
