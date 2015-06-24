using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimeters = new[] {",", "\n"};
            var numbersOnly = numbers;

            if (numbers.StartsWith("//"))
            {
                if (numbers.StartsWith("//["))
                    delimeters = GetDelimeters(numbers);
                else
                    delimeters = new[] { numbers.Skip(2).First().ToString() };

                numbersOnly = 
                    new string(numbers.SkipWhile(c => c != '\n').ToArray());
            }

            var integers =  numbersOnly
                .Split(delimeters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToList();

            var negatives = integers.Where(i => i < 0).ToArray();
            if(negatives.Any())
                throw new ArgumentOutOfRangeException(
                    "numbers",
                    string.Format(
                        "Negatives not allowed. Found: {0}.",
                        string.Join(", ", negatives)));

            return integers.Where(i => i <= 1000).Sum();
        }

        private static string[] GetDelimeters(string numbers)
        {
            var envelope = numbers.Substring(2, numbers.IndexOf("]\n") - 1);
            var s = envelope;
            var delimeters = new List<string>();
            while (s != "")
            {
                var closingBracketPosition = s.IndexOf("]");
                delimeters.Add(s.Substring(1, closingBracketPosition - 1));
                s = s.Substring(closingBracketPosition + 1);
            }

            return delimeters.ToArray();
        }
    }
}
