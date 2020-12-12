using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day03
{
    public class Day03 : DayBase
    {
        public Day03() : base(3, "Toboggan Trajectory")
        {
        }

        private int Traverse(int moveX, int moveY)
        {
            var nrOfTrees = 0;
            var x = 0;
            var y = 0;

            while (y < InputCount)
            {
                var line = InputLine(y);
                var c = line[x];
                if (c == '#') nrOfTrees++;

                x += moveX;
                y += moveY;

                if (x >= line.Length) x -= line.Length;
            }

            return nrOfTrees;
        }

        // Correct Answer = 189
        protected override void SolvePartOne()
        {
            var nrOfTrees = Traverse(3, 1);

            Console.WriteLine($"Answer: {nrOfTrees} number of trees");
        }

        // Correct Answer = 1718180100
        protected override void SolvePartTwo()
        {
            var movements = new List<Tuple<int, int>>()
            {
                new Tuple<int,int>(1, 1 ),
                new Tuple<int,int>(3, 1 ),
                new Tuple<int,int>(5, 1 ),
                new Tuple<int,int>(7, 1 ),
                new Tuple<int,int>(1, 2 )
            };

            var answer = 1;
            foreach (var movement in movements)
            {
                var nrOfTrees = Traverse(movement.Item1, movement.Item2);
                Console.WriteLine($"Trees: {nrOfTrees} encountered with movement ({movement.Item1}, {movement.Item2})");

                answer *= nrOfTrees;
            }

            Console.WriteLine($"Answer: {answer}");
        }
    }
}
