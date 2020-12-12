using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day01
{
    public class Day01 : DayBase
    {
        private List<int> _numbers;
        private int _requestedSum;
        
        public Day01() : base(1, "Report Repair")
        {
            _numbers = new List<int>();
            _requestedSum = 2020;
        }

        protected override void BeforeSolve()
        {
            Console.WriteLine("Reading numbers...");
            ReadNumbers();
        }

        // Correct Answer = 713184
        protected override void SolvePartOne()
        {
            Console.WriteLine($"Trying to find two entries with a sum of {_requestedSum}...");

            if (TryFindTwoEntries(out var entry1, out var entry2))
            {
                Console.WriteLine($"Entry1: {entry1}");
                Console.WriteLine($"Entry2: {entry2}");
                Console.WriteLine($"Answer: {entry1} * {entry2} = {entry1 * entry2}");
            }
            else
            {
                Console.WriteLine($"Couldn't find two entries with a sum of {_requestedSum} :(");
            }
        }

        // Correct Answer = 261244452
        protected override void SolvePartTwo()
        {
            var nrOfEntries = 3;

            Console.WriteLine($"Trying to find {nrOfEntries} entries with a sum of {_requestedSum}...");

            var entries = new List<int>();
            if (TryRecursiveFindNrOfEntries(nrOfEntries, entries)) 
            {
                decimal answer = 1;
                for (int i = 0; i < entries.Count; i++)
                {
                    answer = answer * entries[i];
                    Console.WriteLine($"Entry{i+1}: {entries[i]}");
                }
                Console.WriteLine($"Answer: {answer}");
            }
            else
            {
                Console.WriteLine($"Couldn't find {nrOfEntries} entries with a sum of {_requestedSum} :(");
            }
        }

        private void ReadNumbers()
        {
            _numbers.Clear();
            _numbers.AddRange(Input.Select(x => int.Parse(x)));
            _numbers.Sort();
        }

        private bool TryFindTwoEntries(out int entry1, out int entry2)
        {
            entry1 = 0;
            entry2 = 0;

            if (_numbers.Count >= 2)
            {
                for (int i = 0; i < _numbers.Count-1; i++)
                {
                    var e1 = _numbers[i];
                    for (int j = i+1; j < _numbers.Count; j++)
                    {
                        var e2 = _numbers[j];

                        if (e1 + e2 == 2020)
                        {
                            entry1 = e1;
                            entry2 = e2;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool TryRecursiveFindNrOfEntries(int nrOfEntries, List<int> entries, int index = 0)
        {
            if (index >= InputCount) return false;

            entries.Add(_numbers[index]);

            var sum = entries.Sum();
            var sumFound = (sum == _requestedSum) && (entries.Count == nrOfEntries);

            if (!sumFound && (sum < _requestedSum) && (entries.Count < nrOfEntries))
            {
                sumFound = TryRecursiveFindNrOfEntries(nrOfEntries, entries, index + 1);
            } 

            if (sumFound)
            {
                return true;
            }
            else
            {
                entries.RemoveAt(entries.Count - 1);
                return TryRecursiveFindNrOfEntries(nrOfEntries, entries, index + 1);
            }
        }
    }
}
