using StringCalculator;
using Xunit;
using Xunit.Extensions;

namespace KataCalculator.Tests
{
    public class CalculatorTests
    {
        [Theory, CalculatorTestConventions]
        public void AddEmptyReturnsCorrectResult(Calculator sut)
        {
            var numbers = "";
            var actual = sut.Add(numbers);
            Assert.Equal(0, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddSingleNumberReturnsCorrectResult(
            Calculator sut, 
            int expected)
        {
            var numbers = expected.ToString();
            var actual = sut.Add(numbers);
            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddTwoNumbersReturnsCorrectResult(
            Calculator sut, 
            int x, 
            int y)
        {
            var numbers = string.Join(",", x, y);
            var actual = sut.Add(numbers);
            Assert.Equal(x + y, actual);
        }
    }
}
