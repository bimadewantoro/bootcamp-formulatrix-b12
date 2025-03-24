using Moq;
using UnoGame.Interfaces;

namespace UnoGame.Tests.Controllers.GameControllerTests
{
    [TestFixture]
    public class NextTurnTests : GameControllerTestBase
    {
        [Test]
        public void NextTurn_NormalCase_MoveToNextPlayer()
        {
            // Arrange
            var initialPlayer = _controller.GetCurrentPlayer();

            // Act
            bool result = _controller.NextTurn();

            // Assert
            Assert.That(result, Is.True, "NextTurn should return true when game continues");
            Assert.That(_controller.GetCurrentPlayer(), Is.Not.EqualTo(initialPlayer), "Current player should change");
        }

        [Test]
        public void NextTurn_PlayerHasNoCards_ReturnsFalse()
        {
            // Arrange
            var currentPlayer = _controller.GetCurrentPlayer();

            // Setup an empty hand for the current player
            Mock<IPlayer> playerMock = Mock.Get(currentPlayer);
            var cards = _controller.GetPlayerCards(currentPlayer);
            foreach (var card in cards)
            {
                _controller.RemoveCardFromhand(currentPlayer, card);
            }

            // Act
            bool result = _controller.NextTurn();

            // Assert
            Assert.That(result, Is.False, "NextTurn should return false when a player has no cards");
            Assert.That(_controller.GetRoundWinner(), Is.EqualTo(currentPlayer), "Player with no cards should be the round winner");
        }
    }
}