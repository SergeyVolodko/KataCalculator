using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        [Theory, CalculatorTestConventions]
        public void AddLineWithCustomDelimeterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            int dummy;
            var delimeter = charGenerator
                .Where(c => !int.TryParse(c.ToString(), out dummy))
                .Where(c => c != '-')
                .First();
            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//{0}\n{1}",
                delimeter,
                string.Join(delimeter.ToString(), integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();

            Assert.Equal(expected, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddLineWithNegativeNumbersThrowsCorrectException(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Join(",", -x, y, -z);

            var e = Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.Add(numbers));

            Assert.True(e.Message.StartsWith("Negatives not allowed."));
            Assert.Contains((-x).ToString(), e.Message);
            Assert.Contains((-z).ToString(), e.Message);
        }

        [Theory, CalculatorTestConventions]
        public void AddIgnoresBigNumber(
            Calculator sut,
            int smallSeed,
            int bigSeed)
        {
            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            var numbers = string.Join(",", x, y);

            var actual = sut.Add(numbers);

            Assert.Equal(x, actual);
        }

        [Theory, CalculatorTestConventions]
        public void AddLineWithCustomDelimeterStringReturnsCorrectResult(
            Calculator sut,
            string delimeter,
            int count,
            Generator<int> intGenerator)
        {
            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//[{0}]\n{1}",
                delimeter,
                string.Join(delimeter, integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();

            Assert.Equal(expected, actual);
        }
    }
}
