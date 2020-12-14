using AdventOfCode2020.Shared;
using System;
using System.Collections;
using System.Linq;

namespace AdventOfCode2020.Day14
{
    public class Day14 : DayBase
    {
        private char[] _mask;
        private long[] _memory;

        public Day14() : base(14, "Docking Data")
        {
        }

        protected override void SolvePartOne()
        {
            InitMask();
            InitMemory();

            Console.WriteLine("TODO");
        }

        protected override void SolvePartTwo()
        {
            InitMask();
            InitMemory();

            Console.WriteLine("TODO");
        }

        private void InitMask()
        {
            _mask = Enumerable.Repeat('X', 36).ToArray();
        }

        private void InitMemory()
        {
            _memory = Enumerable.Repeat(0L, sizeof(long)).ToArray();
        }

        private BitArray ConvertToBits(long value)
        {
            return new BitArray(BitConverter.GetBytes(value));
        }

        private BitArray ApplyBitMask(BitArray bits)
        {
            var newBits = new BitArray(36);

            for (int i = 0; i < bits.Length; i++)
            {
                newBits[i] = bits[i];

                if (_mask[i] == '1')
                {
                    newBits[i] = true;
                }
                else if (_mask[i] == '0')
                {
                    newBits[i] = false;
                }
            }

            return newBits;
        }

        private long ConvertToValue(BitArray bits)
        {
            var value = 0L;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i]) value += (long)Math.Pow(2, i);
            }
            return value;
        }
    }
}
