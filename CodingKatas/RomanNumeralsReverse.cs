using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class RomanNumeralsReverseTest
    {
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        public void Should_convert_numerals_when_number_is_lower_than_four(string input, int expected)
        {
            Check.That(RomanNumeralsReverse.Convert(input)).IsEqualTo(expected);
        }

        [Test]
        public void Should_convert_numerals_when_input_is_five()
        {
            Check.That(RomanNumeralsReverse.Convert("V")).IsEqualTo(5);
        }

        [TestCase("VI", 6)]
        [TestCase("VII", 7)]
        [TestCase("VIII", 8)]
        public void Should_convert_numerals_when_input_is_between_six_and_eight(string input, int expected)
        {
            Check.That(RomanNumeralsReverse.Convert(input)).IsEqualTo(expected);
        }

        [Test]
        public void Should_convert_numerals_when_input_is_four()
        {
            Check.That(RomanNumeralsReverse.Convert("IV")).IsEqualTo(4);
        }

        [Test]
        public void Should_convert_numerals_when_input_is_nine()
        {
            Check.That(RomanNumeralsReverse.Convert("IX")).IsEqualTo(9);
        }

        [TestCase("X", 10)]
        [TestCase("XI", 11)]
        [TestCase("XIV", 14)]
        [TestCase("XV", 15)]
        [TestCase("XVI", 16)]
        [TestCase("XVIII", 18)]
        [TestCase("XIX", 19)]
        public void Should_convert_numerals_when_input_is_between_ten_and_nineteen(string input, int expected)
        {
            Check.That(RomanNumeralsReverse.Convert(input)).IsEqualTo(expected);
        }

        [TestCase("XX", 20)]
        [TestCase("XXXII", 32)]
        [TestCase("XXXIX", 39)]
        [TestCase("XLVII", 47)]
        [TestCase("XCIV", 94)]
        [TestCase("MCMIII", 1903)]
        public void Should_convert_numerals_when_input_is_above_twenty(string input, int expected)
        {
            Check.That(RomanNumeralsReverse.Convert(input)).IsEqualTo(expected);
        }
    }

    public class RomanNumeralsReverse
    {
        private static readonly Dictionary<string, int> SymboleValues = new Dictionary<string, int>
        {
            { "M", 1000 },
            { "CM", 900 },
            { "D", 500 },
            { "CD", 400 }, 
            { "C", 100 },
            { "XC", 90 },
            { "L", 50 },
            { "XL", 40 },
            { "X", 10 },
            { "IX", 9 },
            { "V", 5 },
            { "IV", 4 },
            { "I", 1 }
        };

        public static int Convert(string input)
        {
            var result = 0;
            var pos = 0;

            while (pos < input.Length)
            {
                int value;
                if (pos + 2 <= input.Length && SymboleValues.TryGetValue(input.Substring(pos, 2), out value))
                {
                    result += value;
                    pos += 2;
                }
                else if (SymboleValues.TryGetValue(input.Substring(pos, 1), out value))
                {
                    result += value;
                    pos ++;
                }
            }

            return result;
        }
    }
}