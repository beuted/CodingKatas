using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class DrinkingGame
    {
        private static readonly Dictionary<int, string> MatchingTable =
            new Dictionary<int, string> {
                { 3, "fizz" },
                { 5, "buzz" }
            };

        public string Evaluate(int input)
        {
            var res = "";
            foreach (var match in MatchingTable)
            {
                if (input.IsDivisibleBy(match.Key)) res += match.Value;
            }

            return string.IsNullOrEmpty(res) ? input.ToString() : res;
        }
    }

    public static class IntegerExtension
    {
        public static bool IsDivisibleBy(this int input, int divisor)
        {
            return input % divisor == 0;
        }
    }

    [TestClass]
    public class DrinkingGameTest
    {
        [TestCase(2, "2")]
        [TestCase(4, "4")]
        public void Should_return_the_number_if_not_divisible_by_3_or_5(int input, string output)
        {
            var drinkingGame = new DrinkingGame();

            var actual = drinkingGame.Evaluate(input);

            Check.That(actual).Equals(output);
        }

        [TestCase(3)]
        [TestCase(6)]
        public void Should_return_fizz_if_the_input_is_divisible_by_3(int input)
        {
            var drinkingGame = new DrinkingGame();

            Check.That(drinkingGame.Evaluate(input)).Equals("fizz");
        }

        [TestCase(10)]
        [TestCase(20)]
        public void Should_return_buzz_if_the_input_is_divisible_by_5(int input)
        {
            var drinkingGame = new DrinkingGame();

            Check.That(drinkingGame.Evaluate(input)).Equals("buzz");
        }

        [TestCase(30)]
        [TestCase(15)]
        public void Should_return_fizzbuzz_if_the_input_is_divisible_by_5_and_3(int input)
        {
            var drinkingGame = new DrinkingGame();

            Check.That(drinkingGame.Evaluate(input)).Equals("fizzbuzz");
        }
    }
}
