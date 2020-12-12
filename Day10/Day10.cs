using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Day10
{
    public class Day10 : DayBase
    {
        private List<int> _adapters;

        public Day10() : base(10, "Adapter Array")
        {
            _adapters = new List<int>();
        }

        protected override void BeforeSolve()
        {
            _adapters.Clear();
            _adapters.AddRange(Input.Select(x => int.Parse(x)).ToArray());
            _adapters.Add(0);
            _adapters.Add(_adapters.Max() + 3);
            _adapters.Sort();
        }

        //Correct Answer = 2414
        protected override void SolvePartOne()
        {
            var jolts = new int[4] { 0, 0, 0, 0 };
            var currentJolt = 0;

            for (int i = 0; i < _adapters.Count; i++)
            {
                var nextJolt = _adapters[i];
                var joltDiff = nextJolt - currentJolt;
                jolts[joltDiff]++;

                currentJolt = nextJolt;
            }

            var answer = jolts[1] * jolts[3];
            Console.WriteLine($"Answer: {answer}");
        }

        // Correct Answer: 21156911906816
        protected override void SolvePartTwo()
        {
            var paths = new long[_adapters.Count];
            paths[0] = 1;
            for (int i = 1; i < paths.Length; i++)
            {
                paths[i] = 0;
            }

            for (int i = 0; i < _adapters.Count-1; i++)
            {
                var adapter = _adapters[i];

                for (int j = 1; j < 4; j++)
                {
                    var nextAdapter = adapter + j;

                    if (_adapters.Contains(nextAdapter)) paths[_adapters.IndexOf(nextAdapter)] += paths[i];
                }
            }

            Console.WriteLine($"Answer: {paths[paths.Length-1]}");
        }
    }
}
