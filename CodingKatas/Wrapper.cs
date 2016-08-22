using System;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class Wrapper
    {
        public static string Wrap(string input, int colMax)
        {
            if (input.Length == 0) return "";

            var lines = input.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            string finalText = "";
            foreach (var line in lines)
            {
                var currentLine = "";
                var words = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var lineLength = 0;
                foreach (var word in words)
                {
                    if (lineLength + word.Length <= colMax)
                    {
                        lineLength += word.Length;
                        currentLine += word + ' ';
                    }
                    else
                    {
                        if (currentLine.Length != 0)
                        {
                            finalText += currentLine.Substring(0, currentLine.Length - 1) + '\n';
                            currentLine = word + " "; // What if it is end of line ?
                            lineLength = word.Length + 1;
                        }
                    }
                }
                if (currentLine.Length != 0) finalText += currentLine.Substring(0, currentLine.Length - 1) + '\n';
            }

            return finalText.Substring(0, finalText.Length - 1);
        }
    }

    [TestFixture]
    public class WrapperTest
    {
        [TestCase("", 5)]
        [TestCase("hello", 5)]
        [TestCase("hello", 10)]
        [TestCase("hello\nben", 10)]
        public void Should_return_original_string_if_length_inferior_to_max_nb_column(string input, int colMax)
        {
            Check.That(Wrapper.Wrap(input, colMax)).Equals(input);
        }

        [TestCase("hi sir how are you", 10, "hi sir how\nare you")]
        [TestCase("hi sir how comes", 10, "hi sir how\ncomes")]
        public void Should_insert_a_linebreak_if_line_length_between_max_nb_column_and_2_times_max_nb_column(
            string input,
            int colMax,
            string expected)
        {
            Check.That(Wrapper.Wrap(input, colMax)).Equals(expected);
        }

        [TestCase]
        public void Should_return_text_with_lines_inferior_to_max_nb_column()
        {

        }

        [TestCase("aaaaa\naaaaa", 6)]
        public void Should_not_insert_line_breaks_if_they_are_already_present(string input, int colMax)
        {

        }
    }
}
