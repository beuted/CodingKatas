using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            return string.IsNullOrEmpty(input) ? 0 : input.Split(',').Sum(value => int.Parse(value));
        }
    }

    [TestClass]
    public class StringCalculatorTests
    {
        [Test]
        public void Should_return_zero_when_string_is_empty()
        {
            var stringCalculator = new StringCalculator();

            var res = stringCalculator.Add(string.Empty);

            Check.That(res).Equals(0);
        }

        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void Should_return_same_number_when_input_contain_number(string input, int result)
        {
            var stringCalculator = new StringCalculator();

            var res = stringCalculator.Add(input);

            Check.That(res).Equals(result);
        }


        [TestCase("1,2", 3)]
        [TestCase("1,2,3", 6)]
        [TestCase("2,3", 5)]
        public void Should_return_sum_of_numbers_when_input_contain_several_numbers(string input, int result)
        {
            var stringCalculator = new StringCalculator();

            var res = stringCalculator.Add(input);

            Check.That(res).Equals(result);
        }
    }
}
