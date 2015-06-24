using System;
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

            return integers.Sum();
        }
    }
}
