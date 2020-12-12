using AdventOfCode2020.Shared;
using System;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    public class Day02 : DayBase
    {
        public Day02() : base(2, "Password Philosophy")
        {
        }

        private void ParseLine(string line, out Policy policy, out string password)
        {
            var segments = line.Split(": ");
            var policyStr = segments[0];
            password = segments[1];

            segments = policyStr.Split('-', ' ');
            policy = new Policy()
            {
                FirstParameter = int.Parse(segments[0]),
                SecondParameter = int.Parse(segments[1]),
                RequiredContent = segments[2][0]
            };
        }

        // Correct Answer = 607
        protected override void SolvePartOne()
        {
            Console.WriteLine($"Trying to find valid passwords using multi-occurrence policy...");
            var nrValid = 0;
            foreach (var line in Input)
            {
                ParseLine(line, out var policy, out var password);
                if (policy.IsValid(password, PolicyValidationMethod.MultipleOccurrence)) nrValid++;
            }

            Console.WriteLine($"Answer: {nrValid} valid passwords");
        }

        // Correct Answer = 321
        protected override void SolvePartTwo()
        {
            Console.WriteLine($"Trying to find valid passwords using single-occurrence policy...");
            var nrValid = 0;
            foreach (var line in Input)
            {
                ParseLine(line, out var policy, out var password);
                if (policy.IsValid(password, PolicyValidationMethod.SingleOccurrence)) nrValid++;
            }

            Console.WriteLine($"Answer: {nrValid} valid passwords");
        }
    }

    internal class Policy
    {
        public char RequiredContent { get; set; }
        public int FirstParameter { get; set; }
        public int SecondParameter { get; set; }

        public bool IsValid(string password, PolicyValidationMethod method)
        {
            if (method == PolicyValidationMethod.MultipleOccurrence)
            {
                var count = password.Count(c => c == RequiredContent);
                return count >= FirstParameter && count <= SecondParameter;
            }
            else if (method == PolicyValidationMethod.SingleOccurrence)
            {
                var charFirst = password[FirstParameter - 1];
                var charSecond = password[SecondParameter - 1];
                return (charFirst != charSecond) && ((charFirst == RequiredContent) || (charSecond == RequiredContent));
            }

            return false;
        }
    }

    internal enum PolicyValidationMethod
    {
        MultipleOccurrence,
        SingleOccurrence
    }
}
