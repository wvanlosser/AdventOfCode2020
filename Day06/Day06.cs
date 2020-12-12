using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day06
{
    public class Day06 : DayBase
    {
        private List<Group> _groups;

        public Day06() : base(6, "Custom Customs")
        {
            _groups = new List<Group>();
        }

        protected override void BeforeSolve()
        {
            _groups.Clear();

            Group group = null;
            foreach (var line in Input)
            {
                if (line.Trim() == "")
                {
                    if (group != null)
                    {
                        _groups.Add(group);
                        group = null;
                    }
                }
                else
                {
                    if (group == null) group = new Group();

                    group.AddPersonAnswer(line);
                }
            }
            if (group != null) _groups.Add(group);
        }

        //  Correct Answer = 6686
        protected override void SolvePartOne()
        {
            var sum = 0;
            foreach (var group in _groups)
            {
                sum += group.UniqueAnswersCount;
            }
            Console.WriteLine($"Answer: {sum}");
        }

        // Correct Answer = 3476??
        protected override void SolvePartTwo()
        {
            var sum = 0;
            foreach (var group in _groups)
            {
                sum += group.ByAllAnswersCount;
            }
            Console.WriteLine($"Answer: {sum}");
        }
    }

    internal class Group
    {
        private List<string> _personAnswers;
        private List<char> _uniqueAnswers;
        private List<char> _byAllAnswers;

        public Group()
        {
            _personAnswers = new List<string>();
            _uniqueAnswers = new List<char>();
            _byAllAnswers = new List<char>();
        }

        public void AddPersonAnswer(string answer)
        {
            var appendAll = _personAnswers.Count == 0;

            _personAnswers.Add(answer);

            foreach (var c in answer)
            {
                if (!_uniqueAnswers.Contains(c)) _uniqueAnswers.Add(c);
                if (appendAll) _byAllAnswers.Add(c); 
            }

            var i = 0;
            while(i < _byAllAnswers.Count)
            {
                var c = _byAllAnswers[i];
                if (answer.Contains(c))
                {
                    i++;
                }
                else
                {
                    _byAllAnswers.RemoveAt(i);
                }
            }
        }

        public int UniqueAnswersCount { get => _uniqueAnswers.Count; }
        public int ByAllAnswersCount { get => _byAllAnswers.Count; }
    }
}
