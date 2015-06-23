using System.Linq;
using Ploeh.AutoFixture;
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

        [Theory, CalculatorTestConventions]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut, 
            int count,
            Generator<int> generator)
        {
            var integers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", integers);
            
            var actual = sut.Add(numbers);

            var expected = integers.Sum();

            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddWithLineBreakAndComasAsDelimitersRetunsCorrectResult(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Format("{0}\n{1},{2}", x, y, z);

            var actual = sut.Add(numbers);

            Assert.Equal(x + y + z, actual);
        }
    }
}
