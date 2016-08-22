using System;
using System.Collections.Generic;

namespace CodingKatas
{
    using NFluent;

    using NUnit.Framework;

    public interface IOperator
    {
        Dictionary<string, Func<int, int, int>> GetOperators();
    }

    public class Operator : IOperator
    {
        private static readonly Dictionary<string, Func<int, int, int>> DefaultOperators = new Dictionary
            <string, Func<int, int, int>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y }
        };

        public Dictionary<string, Func<int, int, int>> GetOperators()
        {
            return DefaultOperators;
        }
    }

    public class RpnCalculatorTests
    {
        private RpnCalculator rpnCalculator;
​
        [SetUp]
        public void Setup()
        {
            this.rpnCalculator = new RpnCalculator();
        }
​
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        public void Should_return_number_when_expression_is_a_number(string expression, int expected)
        {
            Check.That(this.rpnCalculator.Compute(expression)).IsEqualTo(expected);
        }
​
        [Test]
        public void Should_sum_numbers_when_expression_is_simple_addition()
        {
            Check.That(this.rpnCalculator.Compute("1 2 +")).IsEqualTo(3);
        }
​
        [Test]
        public void Should_substract_numbers_when_expression_is_simple_substraction()
        {
            Check.That(this.rpnCalculator.Compute("2 1 -")).IsEqualTo(1);
        }
​
        [Test]
        public void Should_multiply_numbers_when_expression_is_simple_multiplication()
        {
            Check.That(this.rpnCalculator.Compute("2 2 *")).IsEqualTo(4);
        }
​
        [Test]
        public void Should_divide_numbers_when_expression_is_simple_division()
        {
            Check.That(this.rpnCalculator.Compute("4 2 /")).IsEqualTo(2);
        }
​
        [TestCase("4 2 + 3 -", 3)]
        [TestCase("3 5 8 * 7 + *", 141)]
        public void Should_evaluate_expression_when_contains_several_operators(string expression, int expected)
        {
            Check.That(this.rpnCalculator.Compute(expression)).IsEqualTo(expected);
        }
    }
​
    public class RpnCalculator
    {
        private readonly IOperator _operator;
​
        public RpnCalculator(IOperator op)
        {
            _operator = op;
        }
​
        public RpnCalculator() : this(new Operator())
        {
        }
​
        public int Compute(string expression)
        {
            var tokens = expression.Split(' ');
            var numberStack = new Stack<int>();
            foreach (var token in tokens)
            {
                int number;
                if (int.TryParse(token, out number))
                {
                    numberStack.Push(number);
                }
                else
                {
                    var number2 = numberStack.Pop();
                    var number1 = numberStack.Pop();
                    numberStack.Push(_operator.GetOperators()[token](number1, number2));
                }
            }
            return numberStack.Pop();
        }
    }
}
