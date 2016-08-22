using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    public class PrimeFactors
    {
        public static IEnumerable<int> Generate(int n)
        {
            var remainingNumber = n;
            var factor = 2;
            var primeFactors = new List<int>();

            while (factor * factor <= remainingNumber)
            {
                if (CanBePrime(factor) && remainingNumber % factor == 0)
                {
                    remainingNumber = remainingNumber / factor;
                    primeFactors.Add(factor);
                }
                else
                {
                    factor++;
                }
            }

            if (factor != 1)
            {
                primeFactors.Add(remainingNumber);
            }

            return primeFactors;
        }

        private static bool CanBePrime(int n)
        {
            int[] primeLastDigits = { 1, 3, 7, 9 };
            return n <= 7 || primeLastDigits.Contains(n % 10);
        }
    }

    public class PrimeFactorsTests
    {
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(11)]
        public void Generate_should_return_the_number_when_it_is_prime(int input)
        {
            Check.That(PrimeFactors.Generate(input)).ContainsExactly(input);
        }

        [TestCase(4, new[] { 2, 2 })]
        [TestCase(6, new[] { 2, 3 })]
        [TestCase(8, new[] { 2, 2, 2 })]
        [TestCase(9, new[] { 3, 3 })]
        [TestCase(12, new[] { 2, 2, 3 })]
        [TestCase(121, new[] { 11, 11 })]
        public void Generate_should_return_the_prime_decomposition_of_the_number_when_it_is_not_prime(int input, int[] decomposition)
        {
            Check.That(PrimeFactors.Generate(input)).ContainsExactly(decomposition);
        }
    }
}
