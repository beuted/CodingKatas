using NFluent;
using NUnit.Framework;

namespace CodingKatas
{
    class BowlingGameV2Test
    {
        [Test]
        public void Should_return_score_0_when_no_pins_knocked_down()
        {
            var game = new BowlingGameV2();
            Check.That(game.Score()).IsEqualTo(0);
        }

        [Test]
        public void Should_return_score_when_no_bonuses()
        {
            var game = new BowlingGameV2();

            game.Roll(4);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(6);
        }

        [Test]
        public void Should_return_score_when_spare_is_done()
        {
            var game = new BowlingGameV2();

            game.Roll(4);
            game.Roll(6);

            game.Roll(1);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(14);
        }

        [Test]
        public void Should_return_score_when_strike_is_done()
        {
            var game = new BowlingGameV2();

            game.Roll(10);
            game.Roll(0);

            game.Roll(1);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(16);
        }

        [Test]
        public void Should_return_score_when_consecutive_strikes()
        {
            var game = new BowlingGameV2();

            game.Roll(10);
            game.Roll(0);

            game.Roll(10);
            game.Roll(0);

            game.Roll(1);
            game.Roll(3);

            Check.That(game.Score()).IsEqualTo(39);
        }

        [Test]
        public void Should_return_score_when_strike_at_the_end()
        {
            var game = new BowlingGameV2();
            for (var i = 0; i < 18; i++)
            {
                game.Roll(0);
            }
            
            game.Roll(10);
            game.Roll(2);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(14);
        }

        [Test]
        public void Should_return_score_when_spare_at_the_end()
        {
            var game = new BowlingGameV2();
            for (var i = 0; i < 18; i++)
            {
                game.Roll(0);
            }

            game.Roll(5);
            game.Roll(5);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(12);
        }

        [Test]
        public void Should_return_score_when_two_strikes_at_the_end()
        {
            var game = new BowlingGameV2();
            for (var i = 0; i < 18; i++)
            {
                game.Roll(0);
            }

            game.Roll(10);
            game.Roll(10);
            game.Roll(2);

            Check.That(game.Score()).IsEqualTo(22);
        }
    }

    public class BowlingGameV2
    {
        private int _currentFrame;
        private readonly Frame[] _frames = new Frame[10];

        public BowlingGameV2()
        {
            for (var i = 0; i < _frames.Length; i++)
            {
                _frames[i] = new Frame();
            }
        }

        public int Score()
        {
            var score = 0;
            for (var i = 0; i < _frames.Length; i++)
            {
                if (!_frames[i].IsComplete(i))
                    break;

                score += _frames[i].Roll1;

                if (IsFinalFrame(i) || !_frames[i].IsStrike())
                    score += _frames[i].Roll2;

                if (_frames[i].IsStrike())
                {
                    if (IsFinalFrame(i))
                    {
                        score += _frames[i].Roll2 + _frames[i].Roll3;
                        break;
                    }

                    score += _frames[i + 1].Roll1;

                    if (_frames[i + 1].IsStrike())
                        score += _frames[i + 2].Roll1;
                    else
                        score += _frames[i + 1].Roll2;
                }
                else if (_frames[i].IsSpare())
                    if (IsFinalFrame(i))
                        score += _frames[i].Roll3;
                    else
                        score += _frames[i + 1].Roll1;
            }

            return score;

        }

        public void Roll(int pinsKnocked)
        {
            if (_frames[_currentFrame].Roll1 == -1)
            {
                _frames[_currentFrame].Roll1 = pinsKnocked;
            }
            else if (_frames[_currentFrame].Roll2 == -1)
            {
                _frames[_currentFrame].Roll2 = pinsKnocked;
                if (IsFinalFrame(_currentFrame)) _currentFrame++;
            }
            else
            {
                _frames[_currentFrame].Roll3 = pinsKnocked;
            }
        }

        private static bool IsFinalFrame(int frame)
        {
            return frame == 9;
        }
    }

    internal class Frame
    {
        public int Roll1 = -1;
        public int Roll2 = -1;
        public int Roll3 = -1;

        public bool IsStrike()
        {
            return Roll1 == 10;
        }

        public bool IsSpare()
        {
            return Roll1 + Roll2 == 10;
        }

        public bool IsComplete(int currentFrame)
        {
            return Roll1 != -1 && Roll2 != -1 && (currentFrame != 9 || Roll3 != -1);
        }
    }
}
