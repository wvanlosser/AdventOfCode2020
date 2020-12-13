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

        public Day13() : base(13, "Shuttle Search")
        {
            _busses = new List<int>();
        }

        protected override void BeforeSolve()
        {
            _startTime = long.Parse(InputLine(0));
            _busses.Clear();
            _busses.AddRange(InputLine(1).Split(',')
                                         .Where(x => x != "x")
                                         .Select(x => int.Parse(x))
                                         .ToArray());
        }

        // Correct Answer: 2382
        protected override void SolvePartOne()
        {
            var done = false;
            var earliestBus = 0;
            var earliestTime = _startTime;

            while(!done)
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

        private bool DoesBusDepart(long time, long bus)
        {
            return time % bus == 0;
        }

        protected override void SolvePartTwo()
        {
            Console.WriteLine("TODO");
        }
    }
}
