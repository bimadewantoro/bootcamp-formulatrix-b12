using Moq;
using UnoGame.Enums;
using UnoGame.Interfaces;
using UnoGame.Models;

namespace uno_game.Tests.Controllers
{
    [TestFixture]
    public class DistributeCardsTests : GameControllerTestBase
    {
        // Override setup to avoid automatic card distribution
        public override void Setup()
        {
            _mockDeck = new Mock<IDeck>();
            _mockDisplay = new Mock<IDisplay>();

            _players = new List<IPlayer>
            {
                new Mock<IPlayer>().Object,
                new Mock<IPlayer>().Object
            };

            Mock.Get(_players[0]).Setup(p => p.Name).Returns("Player1");
            Mock.Get(_players[1]).Setup(p => p.Name).Returns("Player2");

            _redFive = new Card(Color.Red, Effect.NoEffect, (Score)5);
            _blueEight = new Card(Color.Blue, Effect.NoEffect, (Score)8);
            _yellowSkip = new Card(Color.Yellow, Effect.Skip, Score.Skip);
            _greenReverse = new Card(Color.Green, Effect.Reverse, Score.Reverse);
            _redDrawTwo = new Card(Color.Red, Effect.DrawTwo, Score.DrawTwo);
            _wildCard = new Card(Color.Wild, Effect.Wild, Score.Wild);
            _wildDrawFourCard = new Card(Color.Wild, Effect.WildDrawFour, Score.WildDrawFour);

            _mockDeck.Setup(d => d.GenerateCards()).Verifiable();
            _mockDeck.Setup(d => d.Shuffle()).Verifiable();
            _mockDeck.Setup(d => d.MoveCardToDiscarded(It.IsAny<ICard>())).Verifiable();

            _controller = new GameController(_players, _mockDeck.Object, _mockDisplay.Object);
        }

        [Test]
        public void DistributeCards_WithValidDeck_GivesEachPlayerSevenCards()
        {
            // Arrange
            var testCards = new Queue<ICard>(new[]
            {
                _redFive, _blueEight, _yellowSkip, _greenReverse,
                _redDrawTwo, _wildCard, _redFive, _blueEight,
                _yellowSkip, _greenReverse, _redDrawTwo, _wildCard,
                _redFive, _blueEight, _yellowSkip,
                _redFive
            });

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            // Act
            _controller.DistributeCards();

            // Assert
            foreach (var player in _players)
            {
                Assert.That(_controller.GetPlayerCards(player).Count, Is.EqualTo(7),
                    $"Player {player.Name} should have 7 cards");
            }
        }

        [Test]
        public void DistributeCards_WithValidDeck_SetsLastPlayedCard()
        {
            // Arrange
            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_redFive);
            }

            testCards.Enqueue(_blueEight);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            // Act
            _controller.DistributeCards();

            // Assert
            Assert.That(_controller.LastPlayedCard, Is.Not.Null, "LastPlayedCard should be set");
            Assert.That(_controller.LastPlayedCard, Is.EqualTo(_blueEight), "LastPlayedCard should be the drawn card");
        }

        [Test]
        public void DistributeCards_WithWildDrawFourAsFirstCard_RecyclesAndTriesAgain()
        {
            // Arrange
            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_redFive);
            }

            testCards.Enqueue(_wildDrawFourCard);
            testCards.Enqueue(_blueEight);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            _mockDeck.Setup(d => d.RecycleDiscarded()).Verifiable();

            // Act
            _controller.DistributeCards();

            // Assert
            Assert.That(_controller.LastPlayedCard, Is.EqualTo(_blueEight),
                "LastPlayedCard should be the regular card after Wild Draw Four");

            _mockDeck.Verify(d => d.MoveCardToDiscarded(_wildDrawFourCard), Times.Once,
                "Wild Draw Four should be moved to discarded");
            _mockDeck.Verify(d => d.RecycleDiscarded(), Times.Once,
                "Discard pile should be recycled after Wild Draw Four");
        }

        [Test]
        public void DistributeCards_WithSkipCardAsFirstCard_SkipsFirstPlayer()
        {
            // Arrange
            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_redFive);
            }

            testCards.Enqueue(_yellowSkip);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            // Act
            _controller.DistributeCards();

            // Assert
            var firstPlayer = _controller.GetCurrentPlayer();
            Assert.That(firstPlayer, Is.EqualTo(_players[1]),
                "First player should be skipped due to Skip card effect");
        }

        [Test]
        public void DistributeCards_WithReverseCardAsFirstCard_ReversesPlayDirection()
        {
            // Arrange
            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_redFive);
            }

            testCards.Enqueue(_greenReverse);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            // Act
            _controller.DistributeCards();

            // Assert
            Assert.That(_controller.GetTurnDirection(), Is.EqualTo(-1),
                "Turn direction should be reversed");
        }

        [Test]
        public void DistributeCards_WhenCalled_TriggersRoundStartEvent()
        {
            // Arrange
            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_redFive);
            }

            testCards.Enqueue(_blueEight);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            bool eventWasFired = false;
            _controller.OnRoundStart += () => eventWasFired = true;

            // Act
            _controller.DistributeCards();

            // Assert
            Assert.That(eventWasFired, Is.True, "OnRoundStart event should be triggered");
        }

        [Test]
        public void DistributeCards_WithExistingCards_ClearsAndDealsNewCards()
        {
            // Arrange
            foreach (var player in _players)
            {
                _controller.AddCardToHand(player, _redFive);
                _controller.AddCardToHand(player, _blueEight);
            }

            var testCards = new Queue<ICard>();

            for (int i = 0; i < 14; i++)
            {
                testCards.Enqueue(_yellowSkip);
            }

            testCards.Enqueue(_blueEight);

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            // Act
            _controller.DistributeCards();

            // Assert
            foreach (var player in _players)
            {
                Assert.That(_controller.GetPlayerCards(player).Count, Is.EqualTo(7),
                    "Existing cards should be cleared before distributing new ones");

                Assert.That(_controller.GetPlayerCards(player).All(c => c.Color == Color.Yellow && c.Effect == Effect.Skip),
                    Is.True, "All cards should be the newly distributed yellow skip cards");
            }
        }
    }
}