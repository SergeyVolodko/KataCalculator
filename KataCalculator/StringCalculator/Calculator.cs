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

            return numbersOnly
                .Split(delimeters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .Sum();
        }
    }
}
