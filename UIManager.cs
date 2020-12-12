using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020
{
    public class UIManager
    {
        private List<DayBase> _days;
        private bool _done;

        public UIManager()
        {
            _days = new List<DayBase>();
            InitDays();
        }

        public void Start()
        {
            _done = false;

            while (!_done)
            {
                PrintMenu();
                HandleUserInput();
            }
        }

        private void InitDays()
        {
            foreach (var t in Assembly.GetAssembly(typeof(DayBase))
                                      .GetTypes()
                                      .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(DayBase))))
            {
                _days.Add((DayBase)Activator.CreateInstance(t));
            }

            _days.Sort();
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("***************************************************");
            Console.WriteLine("**        Welcome to Advent of Code 2020         **");
            Console.WriteLine("***************************************************");
            Console.WriteLine();

            foreach (var day in _days)
            {
                Console.WriteLine($"  {day.GetDayNr():00}: Solve Day {day.GetDayNr():00} - {day.GetDescription()}");
            }
            Console.WriteLine("   q: Exit program");
            Console.WriteLine();
            Console.WriteLine("***************************************************");
            
        }

        private void HandleUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Please select a valid menu option");
            
            var input = Console.ReadLine();
            if (input == "q")
            {
                _done = true;
            }
            else if (int.TryParse(input, out var dayNr))
            {
                var day = _days.Where(x => x.GetDayNr() == dayNr).FirstOrDefault();
                if (day != null)
                {
                    day.SolvePuzzles();
                }
                else
                {
                    Console.WriteLine("Invalid day selected");
                    HandleUserInput();
                }
            }
            else
            {
                Console.WriteLine("Invalid menu option");
                HandleUserInput();
            }
        }
    }
}
