using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class RomanNumeralsTests
    {
        [TestCase(1,"I")]
        [TestCase(2,"II")]
        [TestCase(3,"III")]
        public void Should_convert_number_when_number_is_lower_than_four(int number, string expected)
        {
            Check.That(RomanNumeralsConverter.Convert(number)).IsEqualTo(expected);
        }

        [Test]
        public void Should_number_when_input_is_five()
        {
            Check.That(RomanNumeralsConverter.Convert(5)).IsEqualTo("V");
        }

        [TestCase(6, "VI")]
        [TestCase(7, "VII")]
        [TestCase(8, "VIII")]
        public void Should_number_when_input_is_between_six_and_eight(int number, string expected)
        {
            Check.That(RomanNumeralsConverter.Convert(number)).IsEqualTo(expected);
        }

        [Test]
        public void Should_convert_number_when_input_is_four()
        {
            Check.That(RomanNumeralsConverter.Convert(4)).IsEqualTo("IV");
        }

        [Test]
        public void Should_convert_number_when_input_is_nine()
        {
            Check.That(RomanNumeralsConverter.Convert(9)).IsEqualTo("IX");
        }

        [TestCase(10, "X")]
        [TestCase(11, "XI")]
        [TestCase(14, "XIV")]
        [TestCase(15, "XV")]
        [TestCase(16, "XVI")]
        [TestCase(18, "XVIII")]
        [TestCase(19, "XIX")]
        public void Should_convert_number_when_input_is_between_ten_and_nineteen(int number, string expected)
        {
            Check.That(RomanNumeralsConverter.Convert(number)).IsEqualTo(expected);
        }

        [TestCase(20, "XX")]
        [TestCase(32, "XXXII")]
        [TestCase(39, "XXXIX")]
        [TestCase(47, "XLVII")]
        [TestCase(94, "XCIV")]
        [TestCase(1903, "MCMIII")]
        public void Should_convert_number_when_input_is_above_twenty(int number, string expected)
        {
            Check.That(RomanNumeralsConverter.Convert(number)).IsEqualTo(expected);
        }
    }

    public class RomanNumeralsConverter
    {
        private static readonly Dictionary<int, string> symboles = new Dictionary<int, string>
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" }, 
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" }
        };

        public static string Convert(int input)
        {
            var result = "";

            foreach (var symbole in symboles)
            {
                while (input >= symbole.Key)
                {
                    input -= symbole.Key;
                    result += symbole.Value;
                }
            }

            return result;
        }
    }
}