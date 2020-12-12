using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day12
{
    public class Day12 : DayBase
    {
        private List<NavInstruction> _instructions;
        private Dictionary<char, InstructionType> _instructionTypeMapping;

        private Tuple<int, int> _startingPosition;
        private Tuple<int, int> _currentPosition;
        private Direction _currentDirection;

        public Day12() : base(12, "Rain Risk")
        {
            _instructions = new List<NavInstruction>();
            _instructionTypeMapping = new Dictionary<char, InstructionType>()
            {
                { 'N', InstructionType.North },
                { 'S', InstructionType.South },
                { 'E', InstructionType.East },
                { 'W', InstructionType.West },
                { 'L', InstructionType.Left },
                { 'R', InstructionType.Right },
                { 'F', InstructionType.Forward },
            };
        }

        protected override void BeforeSolve()
        {
            _instructions.Clear();
            _instructions.AddRange(
                Input.Select(x =>
                             {
                                 var instructionType = InstructionType.Unknown;
                                 _instructionTypeMapping.TryGetValue(x[0], out instructionType);

                                 var instructionValue = int.Parse(x.Substring(1));

                                 return new NavInstruction
                                 {
                                     Type = instructionType,
                                     Value = instructionValue
                                 };
                             })
                .ToArray());
        }

        protected override void SolvePartOne()
        {
            _startingPosition = new Tuple<int, int>(0, 0);
            _currentPosition = new Tuple<int, int>(0, 0);
            _currentDirection = Direction.East;

            RunSimulation();
        }

        protected override void SolvePartTwo()
        {
            throw new System.NotImplementedException();
        }


        private void RunSimulation()
        {
            foreach (var instruction in _instructions)
            {
                switch (instruction.Type)
                {
                    case InstructionType.North:
                    case InstructionType.South:
                        MoveNorthSouth(instruction.Value);
                        break;
                    case InstructionType.East:
                    case InstructionType.West:
                        MoveEastWest(instruction.Value);
                        break;
                    case InstructionType.Left:
                        TurnBoatLeft(instruction.Value);
                        break;
                    case InstructionType.Right:
                        TurnBoatRight(instruction.Value);
                        break;
                    case InstructionType.Forward:
                        switch (_currentDirection)
                        {
                            case Direction.North:
                            case Direction.South:
                                MoveNorthSouth(instruction.Value);
                                break;
                            case Direction.East:
                            case Direction.West:
                                MoveEastWest(instruction.Value);
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveNorthSouth(int units)
        {
            _currentPosition = new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 + units);
        }

        private void MoveEastWest(int units)
        {
            _currentPosition = new Tuple<int, int>(_currentPosition.Item1 + units, _currentPosition.Item2);
        }

        private void TurnBoatLeft(int degrees)
        {
            var clicks = degrees / 90;
            var newDirection = (((int)_currentDirection) - clicks) % 3;

            _currentDirection = (Direction)newDirection;
        }

        private void TurnBoatRight(int degrees)
        {
            var clicks = degrees / 90;
            var newDirection = (((int)_currentDirection) + clicks) % 3;

            _currentDirection = (Direction)newDirection;
        }
    }

    internal class NavInstruction
    {
        public InstructionType Type { get; set; }
        public int Value { get; set; }
    }
    internal enum InstructionType
    {
        Unknown,
        North,
        South,
        East,
        West,
        Left,
        Right,
        Forward
    }
    internal enum Direction
    {
        North,
        South,
        East,
        West
    }
}
