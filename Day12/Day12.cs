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
        private Tuple<int, int> _currentBoatPosition;
        private Tuple<int, int> _waypointPosition;
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

        // Correct Answer: 439
        protected override void SolvePartOne()
        {
            _startingPosition = new Tuple<int, int>(0, 0);
            _currentBoatPosition = new Tuple<int, int>(0, 0);
            _currentDirection = Direction.East;

            RunBoatSimulation();

            Console.WriteLine($"Answer: {Math.Abs(_currentBoatPosition.Item1) + Math.Abs(_currentBoatPosition.Item2)}");
        }

        // Correct Answer: 12385
        protected override void SolvePartTwo()
        {
            _startingPosition = new Tuple<int, int>(0, 0);
            _currentBoatPosition = new Tuple<int, int>(0, 0);
            _waypointPosition = new Tuple<int, int>(10, 1);
            _currentDirection = Direction.East;

            RunBoatWaypointSimulation();

            Console.WriteLine($"Answer: {Math.Abs(_currentBoatPosition.Item1) + Math.Abs(_currentBoatPosition.Item2)}");
        }

        private void PrintPosition(Tuple<int, int> position)
        {
            var xDir = position.Item1 >= 0 ? "east" : "west";
            var xVal = Math.Abs(position.Item1);
            var yDir = position.Item2 >= 0 ? "north" : "south";
            var yVal = Math.Abs(position.Item2);

            Console.WriteLine($"Position: {xDir} {xVal}, {yDir} {yVal}");
        }
        private void RunBoatSimulation()
        {
            foreach (var instruction in _instructions)
            {
                switch (instruction.Type)
                {
                    case InstructionType.North:
                    case InstructionType.Forward when _currentDirection == Direction.North:
                        _currentBoatPosition = MoveNorthSouth(_currentBoatPosition, instruction.Value);
                        break;
                    case InstructionType.South:
                    case InstructionType.Forward when _currentDirection == Direction.South:
                        _currentBoatPosition = MoveNorthSouth(_currentBoatPosition, instruction.Value * -1);
                        break;
                    case InstructionType.East:
                    case InstructionType.Forward when _currentDirection == Direction.East:
                        _currentBoatPosition = MoveEastWest(_currentBoatPosition, instruction.Value);
                        break;
                    case InstructionType.West:
                    case InstructionType.Forward when _currentDirection == Direction.West:
                        _currentBoatPosition = MoveEastWest(_currentBoatPosition, instruction.Value * -1);
                        break;
                    case InstructionType.Left:
                        TurnBoat(instruction.Value * 1);
                        break;
                    case InstructionType.Right:
                        TurnBoat(instruction.Value);
                        break;
                    default:
                        break;
                }

                Console.Write($"Current Direction: {_currentDirection}; ");
                PrintPosition(_currentBoatPosition);
            }
        }
        private void RunBoatWaypointSimulation()
        {
            foreach (var instruction in _instructions)
            {
                switch (instruction.Type)
                {
                    case InstructionType.North:
                        _waypointPosition = MoveNorthSouth(_waypointPosition, instruction.Value);
                        break;
                    case InstructionType.South:
                        _waypointPosition = MoveNorthSouth(_waypointPosition, instruction.Value * -1);
                        break;
                    case InstructionType.East:
                        _waypointPosition = MoveEastWest(_waypointPosition, instruction.Value);
                        break;
                    case InstructionType.West:
                        _waypointPosition = MoveEastWest(_waypointPosition, instruction.Value * -1);
                        break;
                    case InstructionType.Left:
                        TurnWaypointLeft(instruction.Value);
                        break;
                    case InstructionType.Right:
                        TurnWaypointRight(instruction.Value);
                        break;
                    case InstructionType.Forward:
                        _currentBoatPosition = MoveBoatToWaypoint(instruction.Value);
                        break;
                    default:
                        break;
                }

                Console.Write($"Waypoint ");
                PrintPosition(_waypointPosition);
                Console.Write($"Boat ");
                PrintPosition(_currentBoatPosition);
            }
        }
        private Tuple<int, int> MoveNorthSouth(Tuple<int, int> position, int units)
        {
            return new Tuple<int, int>(position.Item1, position.Item2 + units);
        }
        private Tuple<int, int> MoveEastWest(Tuple<int, int> position, int units)
        {
            return new Tuple<int, int>(position.Item1 + units, position.Item2);
        }
        private void TurnBoat(int degrees)
        {
            var newDirection = (int)_currentDirection + degrees;
            while (newDirection < 0) newDirection += 360;
            while (newDirection >= 360) newDirection -= 360;

            _currentDirection = (Direction)newDirection;
        }
        private void TurnWaypointLeft(int degrees)
        {
            var nrOfTurns = degrees / 90;

            for (int i = 0; i < nrOfTurns; i++)
            {
                _waypointPosition = new Tuple<int, int>(_waypointPosition.Item2 * -1, _waypointPosition.Item1);
            }
        }
        private void TurnWaypointRight(int degrees)
        {
            var nrOfTurns = degrees / 90;

            for (int i = 0; i < nrOfTurns; i++)
            {
                _waypointPosition = new Tuple<int, int>(_waypointPosition.Item2, _waypointPosition.Item1 * -1);
            }
        }
        private Tuple<int, int> MoveBoatToWaypoint(int nrOfTimes)
        {
            return new Tuple<int, int>(_currentBoatPosition.Item1 + (nrOfTimes * _waypointPosition.Item1), 
                                       _currentBoatPosition.Item2 + (nrOfTimes * _waypointPosition.Item2));
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
        North = 0,
        East  = 90,
        South = 180,
        West  = 270
    }
}
