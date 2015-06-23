using System;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            return numbers
                .Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .Sum();
        }
    }
}
