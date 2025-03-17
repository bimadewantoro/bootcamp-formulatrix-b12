using UnoGame.Models;
using UnoGame.Enums;

namespace uno_game.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            Color expectedColor = Color.Red;
            Effect expectedEffect = Effect.Skip;
            Score expectedScore = Score.Skip;

            var card = new Card(expectedColor, expectedEffect, expectedScore);

            Assert.Multiple(() =>
            {
                Assert.That(card.Color, Is.EqualTo(expectedColor));
                Assert.That(card.Effect, Is.EqualTo(expectedEffect));
                Assert.That(card.Score, Is.EqualTo(expectedScore));
            });
        }

        [Test]
        public void ToString_ForNumberCard_ReturnsColorAndNumber()
        {
            var card = new Card(Color.Blue, Effect.NoEffect, Score.Number3);

            string result = card.ToString();

            Assert.That(result, Is.EqualTo("Blue 3"));
        }

        [Test]
        public void ToString_ForEffectCard_ReturnsColorAndEffect()
        {
            var card = new Card(Color.Green, Effect.Skip, Score.Skip);

            string result = card.ToString();

            Assert.That(result, Is.EqualTo("Green Skip"));
        }

        [Test]
        public void ToString_ForWildCard_ReturnsWildAndEffect()
        {
            var card = new Card(Color.Wild, Effect.WildDrawFour, Score.WildDrawFour);

            string result = card.ToString();

            Assert.That(result, Is.EqualTo("Wild WildDrawFour"));
        }
    }
}