using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day08
{
    public class Day08 : DayBase
    {
        private List<Tuple<string, int>> _instructions;

        public Day08() : base(8, "Handheld Halting")
        {
            _instructions = new List<Tuple<string, int>>();
        }

        protected override void BeforeSolve()
        {
            _instructions.Clear();
            _instructions.AddRange(Input.Select(x => new Tuple<string, int>(x.Substring(0, x.IndexOf(" ")), int.Parse(x.Substring(x.IndexOf(" ") + 1))))
                                        .ToArray());
        }

        // Correct Answer: 1814
        protected override void SolvePartOne()
        {
            var result = RunProgram(_instructions);
            Console.WriteLine($"Answer: {result.Item2}");
        }

        // Correct Answer: 1056
        protected override void SolvePartTwo()
        {
            var done = false;
            var index = 0;
            var accumulator = 0;

            while(!done)
            {
                var alteredResult = AlterInstructions(index);
                if (alteredResult.Item1 < _instructions.Count)
                {
                    index = alteredResult.Item1;

                    var result = RunProgram(alteredResult.Item2);
                    if (!result.Item1)
                    {
                        accumulator = result.Item2;
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
            }

            Console.WriteLine($"Answer: {accumulator}");
        }

        private Tuple<bool, int> RunProgram(List<Tuple<string, int>> instructions)
        {
            var visited = new List<int>();
            var index = 0;
            var accumulator = 0;
            var infiniteLoopDetected = false;
            var done = false;

            while (!done)
            {
                var operation = instructions[index].Item1;
                var argument = instructions[index].Item2;

                accumulator += (operation == "acc") ? argument : 0;
                index += (operation == "jmp") ? argument : 1;

                infiniteLoopDetected = visited.Contains(index);
                done = infiniteLoopDetected || index >= instructions.Count;
                if (!done) visited.Add(index);
            }

            return new Tuple<bool, int>(infiniteLoopDetected, accumulator);
        }

        private Tuple<int, List<Tuple<string, int>>> AlterInstructions(int indexOffset)
        {
            var alteredInstructions = new List<Tuple<string, int>>();
            alteredInstructions.AddRange(_instructions);

            int i = indexOffset + 1;
            while (i < alteredInstructions.Count)
            {
                var instruction = alteredInstructions[i];
                if (instruction.Item1 == "nop" || instruction.Item1 == "jmp")
                {
                    alteredInstructions[i] = new Tuple<string, int>(instruction.Item1 == "nop" ? "jmp" : "nop", instruction.Item2);
                    break;
                } 
                else
                {
                    i++;
                }
            }

            return new Tuple<int, List<Tuple<string, int>>>(i, alteredInstructions);
        }
    }
}
