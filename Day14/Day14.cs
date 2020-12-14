using AdventOfCode2020.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day14
{
    public class Day14 : DayBase
    {
        private char[] _mask;
        private Dictionary<long, long> _memory;

        public Day14() : base(14, "Docking Data")
        {
            _memory = new Dictionary<long, long>();
        }

        // Correct Answer: 14862056079561
        protected override void SolvePartOne()
        {
            InitMask();
            InitMemory();
            ProcessLinesVersion1();

            Console.WriteLine($"Answer: {_memory.Values.Sum()}");
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
            _memory.Clear();
        }

        private void ProcessLinesVersion1()
        {
            foreach (var line in Input)
            {
                if (line.StartsWith("mask"))
                {
                    _mask = line.Remove(0, 7).Reverse().ToArray();
                }
                else
                {
                    var posOB = line.IndexOf("[");
                    var posCB = line.IndexOf("]");

                    var address = long.Parse(line.Substring(posOB + 1, posCB-posOB-1));
                    var value = long.Parse(line.Substring(posCB + 3));

                    var bits = ConvertToBits(value);
                    var bitsWithMask = ApplyBitMask(bits, _mask);

                    _memory[address] = ConvertToValue(bitsWithMask);
                }
            }
        }
        private void ProcessLinesVersion2()
        {
            foreach (var line in Input)
            {
                if (line.StartsWith("mask"))
                {
                    _mask = line.Remove(0, 7).Reverse().ToArray();
                }
                else
                {
                    var posOB = line.IndexOf("[");
                    var posCB = line.IndexOf("]");

                    var address = long.Parse(line.Substring(posOB + 1, posCB - posOB - 1));
                    var value = long.Parse(line.Substring(posCB + 3));

                    var bits = ConvertToBits(address);
                    var addresses = DecodeAddresses(bits);
                    foreach (var addr in addresses)
                    {
                        _memory[addr] = value;
                    }
                }
            }
        }

        private BitArray ConvertToBits(long value)
        {
            return new BitArray(BitConverter.GetBytes(value));
            
        }
        private BitArray ApplyBitMask(BitArray bits, char[] mask)
        {
            var newBits = new BitArray(bits.Length);

            for (int i = 0; i < mask.Length; i++)
            {
                newBits[i] = bits[i];

                if (mask[i] == '1') newBits[i] = true;
                if (mask[i] == '0') newBits[i] = false;
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
        private List<long> DecodeAddresses(BitArray bits)
        {
            var addresses = new List<long>();

            // OUT OF IDEAS..... :(

            return addresses;
        }
    }
}
