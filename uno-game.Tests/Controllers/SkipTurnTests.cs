namespace uno_game.Tests.Controllers
{
    [TestFixture]
    public class SkipTurnTests : GameControllerTestBase
    {
        [Test]
        public void SkipTurn_CurrentPlayer_ReturnsTrue()
        {
            // Arrange
            var currentPlayer = _controller.GetCurrentPlayer();

            // Act
            bool result = _controller.SkipTurn(currentPlayer);

            // Assert
            Assert.That(result, Is.True, "SkipTurn should return true when skipping the current player's turn");
        }

        [Test]
        public void SkipTurn_NotCurrentPlayer_ReturnsFalse()
        {
            // Arrange
            var currentPlayer = _controller.GetCurrentPlayer();
            var otherPlayer = _players.First(p => !p.Equals(currentPlayer));

            // Act
            bool result = _controller.SkipTurn(otherPlayer);

            // Assert
            Assert.That(result, Is.False, "SkipTurn should return false when trying to skip a non-current player's turn");
        }

        [Test]
        public void SkipTurn_NullPlayer_ReturnsFalse()
        {
            // Act
            bool result = _controller.SkipTurn(null!);

            // Assert
            Assert.That(result, Is.False, "SkipTurn should return false when passed a null player");
        }

        [Test]
        public void SkipTurn_CurrentPlayer_EndsTurn()
        {
            // Arrange
            var currentPlayer = _controller.GetCurrentPlayer();

            // Act
            _controller.SkipTurn(currentPlayer);

            // The turn should be ended, so trying to play a card should fail
            bool canPlayCard = _controller.PlayCard(
                currentPlayer,
                _controller.GetPlayerCards(currentPlayer).First()
            );

            // Assert
            Assert.That(canPlayCard, Is.False, "After SkipTurn, the player should not be able to play cards");
        }
    }
}