using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDKata
{

    public class Kata
    {
        private Dictionary<int, string[]> _favoriteGreetings = new Dictionary<int, string[]>();
        private string _lastGreeting;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public string Greet(int id)
        {
            _favoriteGreetings.TryGetValue(id, out var names);
            return Greet(names);
        }
        public string Greet(string name)
        {
            name ??= "my friend";

            if (name == "repeat")
            {
                return _lastGreeting;
            }
            else if (name == name.ToUpper())
            {
                _lastGreeting = $"HELLO, {name}!";
            }
            else
            {
                _lastGreeting = $"Hello, {name}.";
            }
            return _lastGreeting;
        }
        public string Greet(string[] names)
        {
            var allNames = GetAllNames(names);

            var greetingString = string.Empty;
            greetingString += InterpolateLowerCaseName(allNames);
            greetingString += InterpolateUpperCaseNames(allNames);
            
            _lastGreeting = $"Hello,{greetingString}";
            return _lastGreeting;
        }
        public bool SaveFavoriteGreeting(int id, string[] names)
        {
            _favoriteGreetings.Add(id, names);
            return true;
        }
        private List<string> GetAllNames(string[] names)
        {
            var allNames = new List<string>();
            var namesToSplit = names.Where(name => name.Contains(','));
            var namesToKeep = names.Where(name => !name.Contains(','));

            allNames.AddRange(namesToKeep);
            if (namesToSplit.Any())
            {
                allNames.AddRange(SplitNamePairsOnComma(namesToSplit));
            }

            return allNames;
        }

        private string InterpolateLowerCaseName(List<string> allNames)
        {
            const string notationMark = ".";
            var lowerCaseNames = allNames.Where(name => name != name.ToUpper()).ToArray();
            return InterpolateNames(lowerCaseNames) + notationMark;
        }

        private string InterpolateUpperCaseNames(List<string> allNames)
        {
            const string notationMark = "!";
            var upperCaseNames = allNames.Where(name => name == name.ToUpper()).ToArray();
            if (upperCaseNames.Any())
            {
                return " AND HELLO" + InterpolateNames(upperCaseNames).ToUpper() + notationMark;
            }
            return string.Empty;
        }

        private string InterpolateNames(string[] names)
        {
            if (names.Length == 1)
                return $" {names[0]}";
            
            if (names.Length <= 2) 
                return $" {names[0]} and {names[1]}";

            var interpolatedNames = "";
            foreach (var name in names)
            {
                if (Array.IndexOf(names, name) == Array.IndexOf(names, names.Last()))
                {
                    interpolatedNames += $"and {name}";
                }
                else
                {
                    interpolatedNames += name + ", ";
                }
            }

            return $" {interpolatedNames}";
        }
        private string[] SplitNamePairsOnComma(IEnumerable<string> namesToSplit)
        {
            var splitNames = new List<string>();

            foreach (var name in namesToSplit)
            {
                splitNames = name.Split(",").ToList();
                for (var i = 0; i < splitNames.Count; i++)
                {
                    splitNames[i] = splitNames[i].Trim();
                }
            }
            return splitNames.ToArray();
        }


    }
}
