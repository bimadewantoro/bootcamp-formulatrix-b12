using UnoGame.Models;
using UnoGame.Enums;

namespace UnoGame.Tests.Controllers.GameControllerTests
{
    [TestFixture]
    public class IsCardPlayableTests : GameControllerTestBase
    {
        [Test]
        public void IsCardPlayable_NullCard_ReturnsFalse()
        {
            // Act
            bool result = _controller.IsCardPlayable(null!);

            // Assert
            Assert.That(result, Is.False, "IsCardPlayable should return false for a null card");
        }

        [Test]
        public void IsCardPlayable_NullLastPlayedCard_ReturnsFalse()
        {
            // Arrange
            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, null);

            // Act
            bool result = _controller.IsCardPlayable(_redFive);

            // Assert
            Assert.That(result, Is.False, "IsCardPlayable should return false when LastPlayedCard is null");
        }

        [Test]
        public void IsCardPlayable_WildCard_ReturnsTrue()
        {
            // Act
            bool result = _controller.IsCardPlayable(_wildCard);

            // Assert
            Assert.That(result, Is.True, "Wild cards should always be playable");
        }

        [Test]
        public void IsCardPlayable_WildDrawFourCard_ReturnsTrue()
        {
            // Act
            bool result = _controller.IsCardPlayable(_wildDrawFourCard);

            // Assert
            Assert.That(result, Is.True, "Wild Draw Four cards should always be playable");
        }

        [Test]
        public void IsCardPlayable_SameColorCard_ReturnsTrue()
        {
            // Arrange
            var redNine = new Card(Color.Red, Effect.NoEffect, (Score)9);

            // Act
            bool result = _controller.IsCardPlayable(redNine);

            // Assert
            Assert.That(result, Is.True, "Cards with the same color as LastPlayedCard should be playable");
        }

        [Test]
        public void IsCardPlayable_DifferentColorSameNumber_ReturnsTrue()
        {
            // Arrange
            var blueFive = new Card(Color.Blue, Effect.NoEffect, (Score)5);

            // Act
            bool result = _controller.IsCardPlayable(blueFive);

            // Assert
            Assert.That(result, Is.True, "Cards with the same number as LastPlayedCard should be playable");
        }

        [Test]
        public void IsCardPlayable_DifferentColorSameEffect_ReturnsTrue()
        {
            // Arrange
            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, _yellowSkip);

            var blueSkip = new Card(Color.Blue, Effect.Skip, Score.Skip);

            // Act
            bool result = _controller.IsCardPlayable(blueSkip);

            // Assert
            Assert.That(result, Is.True, "Cards with the same effect as LastPlayedCard should be playable");
        }

        [Test]
        public void IsCardPlayable_DifferentColorAndValue_ReturnsFalse()
        {
            // Arrange
            var blueSeven = new Card(Color.Blue, Effect.NoEffect, (Score)7);

            // Act
            bool result = _controller.IsCardPlayable(blueSeven);

            // Assert
            Assert.That(result, Is.False, "Cards with different color and value should not be playable");
        }

        [Test]
        public void IsCardPlayable_DifferentColorAndEffect_ReturnsFalse()
        {
            // Arrange
            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, _yellowSkip);

            var blueReverse = new Card(Color.Blue, Effect.Reverse, Score.Reverse);

            // Act
            bool result = _controller.IsCardPlayable(blueReverse);

            // Assert
            Assert.That(result, Is.False, "Cards with different color and effect should not be playable");
        }

        [Test]
        public void IsCardPlayable_NoEffectCardWithSameColor_ReturnsTrue()
        {
            // Arrange
            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, _yellowSkip);

            var yellowSeven = new Card(Color.Yellow, Effect.NoEffect, (Score)7);

            // Act
            bool result = _controller.IsCardPlayable(yellowSeven);

            // Assert
            Assert.That(result, Is.True, "A card with NoEffect but same color should be playable");
        }
    }
}