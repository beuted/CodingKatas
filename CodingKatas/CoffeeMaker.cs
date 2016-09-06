using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    using NSubstitute;

    public class CoffeeMakerTests
    {
        private IEmailNotifier _emailNotifier;
        private IBeverageQuantityChecker _beverageQuantityChecker;
        private CoffeeMakerLogic _coffeeMakerLogic;

        [SetUp]
        public void Setup()
        {
            _emailNotifier = Substitute.For<IEmailNotifier>();
            _beverageQuantityChecker = Substitute.For<IBeverageQuantityChecker>();
            _coffeeMakerLogic = new CoffeeMakerLogic(_emailNotifier, _beverageQuantityChecker);

            _beverageQuantityChecker.IsEmpty(Arg.Any<string>()).Returns(false);
        }

        [Test]
        public void Should_command_a_coffee_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, MoneyGiven = 1};
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("C::");
        }

        [Test]
        public void Should_command_a_tea_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder {Drink = DrinkType.Tea, MoneyGiven = 1};
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("T::");
        }

        [Test]
        public void Should_command_a_hot_chocolate_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.HotChocolate, MoneyGiven = 1};
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("H::");
        }

        [Test]
        public void Should_command_an_orangejuice_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.OrangeJuice, MoneyGiven = 0.6 };
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("O::");
        }

        [Test]
        public void Should_command_coffee_with_one_sugar_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, NbSugar = 1 , MoneyGiven = 1};
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("C:1:0");
        }

        [Test]
        public void Should_command_coffee_with_enough_sugar_when_more_than_one_sugar()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, NbSugar = 10 , MoneyGiven = 1 };
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("C:10:0");
        }

        [Test]
        public void Should_command_an_extrahotcoffee_when_ordered()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, ExtraHot = true, NbSugar = 1, MoneyGiven = 0.6 };
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("Ch:1:0");
        }

        [Test]
        public void Should_forward_message_when_not_a_command()
        {
            var coffeeOrder = new CoffeeOrder { Message = "My message" };
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("M:My message");
        }

        [Test]
        public void Should_not_command_coffee_when_not_enought_money_is_given()
        {
            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, NbSugar = 1, MoneyGiven = 0.4 };
            var command = _coffeeMakerLogic.Interpret(coffeeOrder);
            Check.That(command).IsEqualTo("M:Missing 0,2 Euros");
        }

        [Test]
        public void Should_command_drink_when_missing()
        {
            _beverageQuantityChecker.IsEmpty("C").Returns(true);

            var coffeeOrder = new CoffeeOrder { Drink = DrinkType.Coffee, ExtraHot = true, NbSugar = 1, MoneyGiven = 0.6 };
            _coffeeMakerLogic.Interpret(coffeeOrder);

            _emailNotifier.Received().NotifyMissingDrink("C");
            _emailNotifier.DidNotReceive().NotifyMissingDrink("T");
            _emailNotifier.DidNotReceive().NotifyMissingDrink("H");
            _emailNotifier.DidNotReceive().NotifyMissingDrink("O");
        }
    }

    public class CoffeeOrder
    {
        public DrinkType Drink { get; set; }
        public bool ExtraHot { get; set; }
        public int NbSugar { get; set; }
        public string Message { get; set; }
        public double MoneyGiven { get; set; }
    }

    public enum DrinkType
    {
        Coffee = 1,
        Tea = 2,
        HotChocolate = 3,
        OrangeJuice = 4
    }

    public class DrinkProperty
    {
        public string CommandLetter { get; set; }
        public double Price { get; set; }
    }

    public interface IEmailNotifier
    {
        void NotifyMissingDrink(string drink);
    }

    public interface IBeverageQuantityChecker
    {
        bool IsEmpty(string drink);
    }

    public class CoffeeMakerLogic
    {
        private readonly IEmailNotifier _emailNotifier;
        private readonly IBeverageQuantityChecker _brevageQuantityChecker;

        private static readonly Dictionary<DrinkType, DrinkProperty> DrinkMapping =
            new Dictionary<DrinkType, DrinkProperty>
            {
                {
                    DrinkType.Coffee,
                    new DrinkProperty { CommandLetter = "C", Price = 0.6 }
                },
                {
                    DrinkType.HotChocolate,
                    new DrinkProperty { CommandLetter = "H", Price = 0.5 }
                },
                {
                    DrinkType.Tea,
                    new DrinkProperty { CommandLetter = "T", Price = 0.4 }
                },
                {
                    DrinkType.OrangeJuice,
                    new DrinkProperty { CommandLetter = "O", Price = 0.6 }
                }
            };

        public CoffeeMakerLogic(
            IEmailNotifier emailNotifier,
            IBeverageQuantityChecker brevageQuantityChecker)
        {
            _emailNotifier = emailNotifier;
            _brevageQuantityChecker = brevageQuantityChecker;
        }

        public string Interpret(CoffeeOrder coffeeOrder)
        {
            if (coffeeOrder.Message != null)
                return string.Format("M:{0}", coffeeOrder.Message);

            if (DrinkMapping[coffeeOrder.Drink].Price > coffeeOrder.MoneyGiven)
                return string.Format("M:Missing {0} Euros", DrinkMapping[coffeeOrder.Drink].Price - coffeeOrder.MoneyGiven);

            if (_brevageQuantityChecker.IsEmpty(DrinkMapping[coffeeOrder.Drink].CommandLetter))
                _emailNotifier.NotifyMissingDrink(DrinkMapping[coffeeOrder.Drink].CommandLetter);

            return string.Format(
                "{0}{3}:{1}:{2}",
                DrinkMapping[coffeeOrder.Drink].CommandLetter,
                SugarAsString(coffeeOrder.NbSugar),
                StickAsString(coffeeOrder),
                coffeeOrder.ExtraHot ? "h" : "");
        }

        private static string StickAsString(CoffeeOrder coffeeOrder)
        {
            return coffeeOrder.NbSugar > 0 ? "0" : string.Empty;
        }

        private static string SugarAsString(int nbSugar)
        {
            return nbSugar > 0 ? nbSugar.ToString() : string.Empty;
        }
    }
}
