using AdventOfCode2020.Shared;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day04
{
    public class Day04 : DayBase
    {
        private List<Passport> _passports;

        public Day04() : base(4, "Passport Processing")
        {
            _passports = new List<Passport>();
        }

        protected override void BeforeSolve()
        {
            _passports.Clear();

            Passport p = null;
            foreach (var line in Input)
            {
                if (line.Trim() == "")
                {
                    if (p != null)
                    {
                        _passports.Add(p);
                        p = null;
                    }
                }
                else
                {
                    if (p == null) p = new Passport();

                    var fields = line.Split(' ');
                    foreach (var field in fields)
                    {
                        var segments = field.Split(':');
                        if (segments.Length == 2)
                        {
                            p.Add(segments[0], segments[1]);
                        }
                    }
                }
            }

            if (p != null) _passports.Add(p);
        }

        // Correct Answer = 182
        protected override void SolvePartOne()
        {
            var validator = new PassportValidator();
            validator.SetRequiredFields("byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid");
            validator.SetOptionalFields("cid");

            var nrValid = 0;
            foreach (var p in _passports)
            {
                if (validator.IsValid(p)) nrValid++;
            }

            Console.WriteLine($"Answer: {nrValid} valid passports");
        }

        // Correct Answer = 109
        protected override void SolvePartTwo()
        {
            var validator = new PassportValidator();
            validator.ClearRequiredFields();

            // Birthday - four digits; at least 1920 and at most 2002.
            validator.AddRequiredField("byr", "^(19[2-9]\\d)|(200[012])$");

            // Issue Year - four digits; at least 2010 and at most 2020.
            validator.AddRequiredField("iyr", "^20(1\\d|20)$");

            // Expiry Year - four digits; at least 2020 and at most 2030.
            validator.AddRequiredField("eyr", "^20(2\\d|30)$");

            // Height - a number followed by either cm or in: 
            // If cm, the number must be at least 150 and at most 193.
            // If in, the number must be at least 59 and at most 76.
            var pattern_cm = "1([5-8]\\d|9[0-3])cm";
            var pattern_in = "(59|6\\d|7[0-6])in";
            validator.AddRequiredField("hgt", $"^({pattern_cm})|({pattern_in})$");

            // Hair Colour - a # followed by exactly six characters 0-9 or a-f.
            validator.AddRequiredField("hcl", "^#[0-9a-f]{6}$");

            // Eye Colour - Exactly one of amb blu brn gry grn hzl oth
            validator.AddRequiredField("ecl", "^amb|blu|brn|gry|grn|hzl|oth$");

            // Passport ID - a nine-digit number, including leading zeroes.
            validator.AddRequiredField("pid", "^\\d{9}$");

            // Country - Optional
            validator.SetOptionalFields("cid");

            var nrValid = 0;
            foreach (var p in _passports)
            {
                if (validator.IsValid(p)) nrValid++;
            }

            Console.WriteLine($"Answer: {nrValid} valid passports");

        }
    }

    internal class Passport : Dictionary<string, string> { }
    internal class PassportValidator
    {
        private Dictionary<string, string> _requiredFields;
        private List<string> _optionalFields;

        public PassportValidator()
        {
            _requiredFields = new Dictionary<string,string>();
            _optionalFields = new List<string>();
        }

        public void SetRequiredFields(params string[] fields)
        {
            ClearRequiredFields();

            foreach (var field in fields)
            {
                AddRequiredField(field, "");
            }
        }

        public void ClearRequiredFields()
        {
            _requiredFields.Clear();
        }

        public void AddRequiredField(string field, string pattern)
        {
            _requiredFields.Add(field, pattern);
        }

        public void SetOptionalFields(params string[] fields)
        {
            _optionalFields.Clear();
            _optionalFields.AddRange(fields);
        }

        public bool IsValid(Passport p)
        {
            foreach (var field in _requiredFields.Keys)
            {
                if (!p.ContainsKey(field)) return false;

                var value = p[field];
                var pattern = _requiredFields[field];

                if (pattern != "")
                {
                    if (!Regex.IsMatch(value, pattern)) return false;
                }
            }

            foreach (var field in p.Keys)
            {
                if (!_requiredFields.ContainsKey(field) && !_optionalFields.Contains(field)) return false;
            }

            return true;
        }
    }

}
