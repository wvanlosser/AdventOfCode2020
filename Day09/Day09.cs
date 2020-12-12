using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day09
{
    public class Day09 : DayBase
    {
        private List<long> _numbers;

        public Day09() : base(9, "Encoding Error")
        {
            _numbers = new List<long>();
        }

        protected override void BeforeSolve()
        {
            _numbers.Clear();
            _numbers.AddRange(Input.Select(x => long.Parse(x)).ToArray());
        }

        // Correct Answer: 15353384
        protected override void SolvePartOne()
        {
            var preambleCount = 25;
            var indexOffset = 0;
            var found = false;

            while (!found && (indexOffset+preambleCount < _numbers.Count))
            {
                var preamble = GetPreamble(indexOffset, preambleCount);
                var requestedNumber = _numbers[preambleCount + indexOffset];
                var result = FindMatch(preamble, requestedNumber);
                if (!result)
                {
                    Console.WriteLine($"Answer: {requestedNumber}");
                    break;
                } 
                else
                {
                    indexOffset++;
                }
            }
        }

        // Correct Answer: 2466556
        protected override void SolvePartTwo()
        {
            long invalidNumber = 15353384;
            var contiguousSet = FindContiguousSet(invalidNumber);
            if (contiguousSet.Count >= 2)
            {
                var min = contiguousSet.Min();
                var max = contiguousSet.Max();
                Console.WriteLine($"Answer: {min} + {max} = {min+max}");
            }
        }

        private List<long> GetPreamble(int offset, int count)
        {
            var preamble = new long[count];

            if (offset + count <= _numbers.Count)
            {
                _numbers.CopyTo(offset, preamble, 0, count);
            }
            
            return new List<long>(preamble);
        }
        private bool FindMatch(List<long> preamble, long requestedNumber)
        {
            var found = false;
            long first = 0;
            long second = 0;

            for (int iSecond = preamble.Count-1; iSecond > 0; iSecond--)
            {
                second = preamble[iSecond];

                for (int iFirst = 0; iFirst < iSecond; iFirst++)
                {
                    first = preamble[iFirst];

                    found = (first != second) && (first + second == requestedNumber);
                    if (found)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<long> FindContiguousSet(long requestedNumber)
        {
            var set = new List<long>() { _numbers[0], _numbers[1]};
            var sum = set.Sum();
            var index = 2;
            var done = false;

            while (!done && index < _numbers.Count)
            {
                done = sum == requestedNumber;
                
                if (sum < requestedNumber && index < _numbers.Count)
                {
                    set.Add(_numbers[index]);
                    sum += _numbers[index];
                    index++;
                }
                else if (sum > requestedNumber)
                {
                    sum -= set[0];
                    set.RemoveAt(0);
                }
            }

            if (!done) set.Clear();
            return set;
        }
    }
}
