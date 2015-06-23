using System;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            return numbers
                .Split(new [] {",", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .Sum();
        }
    }
}
