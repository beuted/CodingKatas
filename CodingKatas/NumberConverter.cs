using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    using System.Collections.Generic;

    public class NumberConverter
    {
        private static readonly Dictionary<int, string> BaseNumbersRepresentations = new Dictionary<int, string>
                                                                         {
                                                                             { 0, "zero"},
                                                                             { 1, "one" },
                                                                             { 2, "two" },
                                                                             { 3, "three" },
                                                                             { 4, "four" },
                                                                             { 5, "five" },
                                                                             { 6, "six" },
                                                                             { 7, "seven" },
                                                                             { 8, "height" },
                                                                             { 9, "nine" },
                                                                             { 10, "ten"},
                                                                             { 11, "eleven" },
                                                                             { 12, "twelve" },
                                                                             { 13, "thirteen" },
                                                                             { 14, "fourteen" },
                                                                             { 15, "fiveteen" },
                                                                             { 16, "sixteen" },
                                                                             { 17, "seventeen" },
                                                                             { 18, "heighteen" },
                                                                             { 19, "nineteen" }
                                                                         };

        private static readonly Dictionary<int, string> ComplexNumberRepresentations = new Dictionary<int, string>
                                                                         {
                                                                             { 1, "ten"},
                                                                             { 2, "twenty" },
                                                                             { 3, "thirty" },
                                                                             { 4, "forty" },
                                                                             { 5, "fifty" },
                                                                             { 6, "sixty" },
                                                                             { 7, "seventy" },
                                                                             { 8, "heighty" },
                                                                             { 9, "ninety" }
                                                                         };


        public string Convert(int n)
        {
            var nMod1000 = n % 1000;
            var nCoef1000 = (n - nMod1000) / 1000;

            var nMod1000000 = n % 1000000;
            var nCoef1000000 = (n - nMod1000000) / 1000000;

            if (n < 1000)
                return ConvertLowerThanThousand(n);

            return ConvertLowerThanThousand(nCoef1000) + " thousand" + (nMod1000 != 0 ? (" " + ConvertLowerThanThousand(nMod1000)) : "");
        }

        private static string ConvertLowerThanThousand(int n)
        {
            var nMod100 = n % 100;
            var nCoef100 = (n - nMod100) / 100;
            var nMod100String = ConvertLowerThanHundred(nMod100);

            if (nCoef100 == 0)
                return nMod100String;

            return BaseNumbersRepresentations[nCoef100] + " hundred" + (nMod100 != 0 ? (" and " + nMod100String) : "");
        }

        private static string ConvertLowerThanHundred(int n)
        {
            if (n < 20)
                return BaseNumbersRepresentations[n];

            var nMod10 = n % 10;
            var nCoef10 = (n - n % 10) / 10;

            if (nMod10 == 0)
                return ComplexNumberRepresentations[nCoef10];

            return ComplexNumberRepresentations[nCoef10] + (nMod10 != 0 ? ("-" + BaseNumbersRepresentations[nMod10]) : "");
        }
    }

    public class NumberConverterTest
    {
        [TestCase(1, "one")]
        [TestCase(2, "two")]
        public void Convert_should_convert_to_words_when_given_a_numeral(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

        [TestCase(10, "ten")]
        [TestCase(12, "twelve")]
        public void Convert_should_convert_to_words_when_given_below_20_numbers(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

        [TestCase(30, "thirty")]
        [TestCase(34, "thirty-four")]
        [TestCase(69, "sixty-nine")]
        public void Convert_should_convert_to_words_when_given_two_digit_numbers(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

        [TestCase(300, "three hundred")]
        [TestCase(340, "three hundred and forty")]
        [TestCase(691, "six hundred and ninety-one")]
        [TestCase(601, "six hundred and one")]
        public void Convert_should_convert_to_words_when_given_three_digit_numbers(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

        [TestCase(3000, "three thousand")]
        [TestCase(4400, "four thousand four hundred")]
        [TestCase(6910, "six thousand nine hundred and ten")]
        [TestCase(6931, "six thousand nine hundred and thirty-one")]
        [TestCase(6031, "six thousand thirty-one")]
        [TestCase(6001, "six thousand one")]
        public void Convert_should_convert_to_words_when_given_four_digit_numbers(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

        [TestCase(300000000, "three hundred million")]
        [TestCase(440000000, "four hundred and forty million")]
        [TestCase(691000000, "six hundred ninety-one million")]
        [TestCase(693234215, "six hundred ninety-three million two hundred thirty-four thousand two hundred fiveteen")]
        public void Convert_should_convert_to_words_when_given_nine_digits_numbers(int i, string expected)
        {
            var numberConverter = new NumberConverter();
            Check.That(numberConverter.Convert(i)).IsEqualTo(expected);
        }

    }
}
