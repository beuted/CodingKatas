using NUnit.Framework;
using NFluent;

namespace CodingKatas
{
    public class BowlingGameTest
    {
        [Test]
        public void Score_should_return_0_when_no_pin_was_knocked_down()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(0);

            Check.That(bowlingGame.Score()).IsEqualTo(0);
        }

        [Test]
        public void Score_should_return_pins_knocked_down_when_no_bonus()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(2);
        }

        [Test]
        public void Score_should_return_score_when_one_spare()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(4);
            bowlingGame.Roll(6);
            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(14);
        }

        [Test]
        public void Score_should_return_score_when_two_spares()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(3);
            bowlingGame.Roll(7);

            bowlingGame.Roll(1);
            bowlingGame.Roll(0);

            bowlingGame.Roll(5);
            bowlingGame.Roll(5);

            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(26);
        }

        [Test]
        public void Score_should_return_score_when_two_consecutives_spares()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(3);
            bowlingGame.Roll(7);

            bowlingGame.Roll(5);
            bowlingGame.Roll(5);

            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(29);
        }

        [Test]
        public void Score_should_return_score_when_three_fives_in_a_row()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(5);
            bowlingGame.Roll(5);

            bowlingGame.Roll(5);
            bowlingGame.Roll(1);

            Check.That(bowlingGame.Score()).IsEqualTo(21);
        }

        [Test]
        public void Score_should_return_score_when_strike()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);


            bowlingGame.Roll(1);
            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(16);
        }

        [Test]
        public void Score_should_return_score_when_two_consecutive_strike()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);


            bowlingGame.Roll(10);
            bowlingGame.Roll(0);

            bowlingGame.Roll(3);
            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(40);
        }

        [Test]
        public void Score_should_return_score_when_consecutive_strike_and_spare()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);


            bowlingGame.Roll(7);
            bowlingGame.Roll(3);

            bowlingGame.Roll(3);
            bowlingGame.Roll(4);

            Check.That(bowlingGame.Score()).IsEqualTo(40);
        }

        [Test]
        public void Score_should_return_score_when_two_strikes()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);

            bowlingGame.Roll(10);
            bowlingGame.Roll(0);

            bowlingGame.Roll(2);

            Check.That(bowlingGame.Score()).IsEqualTo(34);
        }

        [Test, Ignore("WIP")]
        public void Score_should_return_score_when_complicated_game()
        {
            var bowlingGame = new BowlingGame();
            bowlingGame.Roll(1);
            bowlingGame.Roll(4);
            bowlingGame.Roll(4);
            bowlingGame.Roll(5);
            bowlingGame.Roll(6);
            bowlingGame.Roll(4);
            bowlingGame.Roll(5);
            bowlingGame.Roll(5);
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);
            bowlingGame.Roll(0);
            bowlingGame.Roll(1);
            bowlingGame.Roll(7);
            bowlingGame.Roll(3);
            bowlingGame.Roll(6);
            bowlingGame.Roll(4);
            bowlingGame.Roll(10);
            bowlingGame.Roll(0);
            bowlingGame.Roll(2);
            bowlingGame.Roll(8);
            bowlingGame.Roll(6);

            Check.That(bowlingGame.Score()).IsEqualTo(133);
        }
    }

    public class BowlingGame
    {
        private int score;

        private readonly int[] rolls = new int[23];

        private int currentRoll = 0;

        public void Roll(int knockedDownPins)
        {
            if (this.IsSpare())
            {
                this.score += 2 * knockedDownPins;
            }
            else if ((this.currentRoll >= 2 && this.rolls[this.currentRoll - 2] == 10) || (this.currentRoll >= 3 && this.rolls[this.currentRoll - 3] == 10))
            {
                this.score += 2 * knockedDownPins;
            }
            else
            {
                this.score += knockedDownPins;
            }

            this.rolls[this.currentRoll] = knockedDownPins;
            this.currentRoll++;
        }

        private bool IsSpare()
        {
            return this.currentRoll >= 2 && this.currentRoll % 2 == 0 && this.rolls[this.currentRoll - 1] + this.rolls[this.currentRoll - 2] == 10;
        }

        public int Score()
        {
            return this.score;
        }
    }
}
