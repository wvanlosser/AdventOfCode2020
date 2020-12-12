using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day07
{
    public class Day07 : DayBase
    {
        private Dictionary<string, Bag> _bags;

        public Day07() : base(7, "Handy Haversacks")
        {
            _bags = new Dictionary<string, Bag>();
        }

        protected override void BeforeSolve()
        {
            _bags.Clear();
            foreach (var rule in Input)
            {
                var segments = rule.Replace("bags", "").Replace("bag", "").Replace(".", "").Split("contain");
                if (segments.Length == 2)
                {
                    var color = segments[0].Trim();
                    var bag = GetOrAddBag(color);
                    
                    var contents = segments[1].Split(",")
                        .Select(x => x.Trim())
                        .Where(x => x != "no other")
                        .Select(x => new Tuple<string, int>(x.Substring(x.IndexOf(" ")).Trim(), int.Parse(x.Substring(0, x.IndexOf(" ")))));

                    foreach (var contentTuple in contents)
                    {
                        var subBag = GetOrAddBag(contentTuple.Item1);
                        bag.AddContent(subBag, contentTuple.Item2);
                    }
                }
            }
        }

        private Bag GetOrAddBag(string color)
        {
            if (!_bags.ContainsKey(color))
            {
                _bags.Add(color, new Bag(color));
            }

            return _bags[color];
        }

        // Correct Answer = 155
        protected override void SolvePartOne()
        {
            var bag = _bags["shiny gold"];
            var uniqueBags = new List<Bag>();

            FindUniqueBagsWithBagIsPartOf(bag, uniqueBags);
            Console.WriteLine($"Answer: {uniqueBags.Count}");
        }

        private void FindUniqueBagsWithBagIsPartOf(Bag bag, List<Bag> uniqueBags)
        {
            foreach (var parentBag in bag.PartOf)
            {
                if (!uniqueBags.Contains(parentBag))
                {
                    uniqueBags.Add(parentBag);
                    FindUniqueBagsWithBagIsPartOf(parentBag, uniqueBags);
                }
            }
        }

        // Correct Answer = 54803
        protected override void SolvePartTwo()
        {
            var bag = _bags["shiny gold"];
            var nrOfBags = FindTotalNrOfBags(bag);

            Console.WriteLine($"Answer: {nrOfBags}");
        }

        private int FindTotalNrOfBags(Bag bag, int modifier = 1)
        {
            var nrOfBags = 0;

            foreach (var content in bag.Contents)
            {
                var subBag = content.Item1;
                var quantity = content.Item2;

                var newModifier = modifier * quantity;
                nrOfBags += newModifier + FindTotalNrOfBags(subBag, newModifier);
            }

            return nrOfBags;
        }
    }

    internal class Bag
    {
        private string _color;
        private List<Tuple<Bag,int>> _contents;
        private List<Bag> _partOf;

        public Bag(string color)
        {
            _color = color;
            _contents = new List<Tuple<Bag,int>>();
            _partOf = new List<Bag>();
        }

        public string Color { get => _color; }
        
        public void AddContent(Bag bag, int quantity)
        {
            _contents.Add(new Tuple<Bag, int>(bag, quantity));
            bag.AddPartOf(this);
        }

        public void AddPartOf(Bag bag)
        {
            _partOf.Add(bag);
        }

        public IEnumerable<Tuple<Bag, int>> Contents => _contents;
        public IEnumerable<Bag> PartOf => _partOf;
    }
}
