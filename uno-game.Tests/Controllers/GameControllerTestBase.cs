using NUnit.Framework;
using Moq;
using UnoGame.Models;
using UnoGame.Interfaces;
using UnoGame.Enums;
using System.Collections.Generic;

namespace UnoGame.Tests.Controllers.GameControllerTests
{
    public class GameControllerTestBase
    {
        // Common mocks
        protected Mock<IDeck> _mockDeck;
        protected Mock<IDisplay> _mockDisplay;
        protected List<IPlayer> _players;
        protected GameController _controller;

        // Test cards
        protected ICard _redFive;
        protected ICard _blueEight;
        protected ICard _yellowSkip;
        protected ICard _greenReverse;
        protected ICard _redDrawTwo;
        protected ICard _wildCard;
        protected ICard _wildDrawFourCard;

        [SetUp]
        public virtual void Setup()
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

            var testCards = new Queue<ICard>(new[]
            {
                _redFive, _blueEight, _yellowSkip, _greenReverse,
                _redDrawTwo, _wildCard, _redFive, _blueEight,
                _yellowSkip, _greenReverse, _redDrawTwo, _wildCard,
                _redFive, _blueEight, _yellowSkip, _redFive
            });

            _mockDeck.Setup(d => d.Draw()).Returns(() =>
                testCards.Count > 0 ? testCards.Dequeue() : null);

            _mockDeck.Setup(d => d.GenerateCards()).Verifiable();
            _mockDeck.Setup(d => d.Shuffle()).Verifiable();
            _mockDeck.Setup(d => d.MoveCardToDiscarded(It.IsAny<ICard>())).Verifiable();

            _controller = new GameController(_players, _mockDeck.Object, _mockDisplay.Object);

            EnsurePlayersHaveCards();
        }

        protected void EnsurePlayersHaveCards()
        {
            _controller.StartNewRound();

            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, _redFive);
        }
    }
}