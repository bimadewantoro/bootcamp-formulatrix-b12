using Moq;
using UnoGame.Models;
using UnoGame.Interfaces;

namespace UnoGame.Tests.Controllers
{
    public class GameControllerTestBase
    {
        protected GameController _gameController;
        protected Mock<IDeck> _mockDeck;
        protected Mock<IDisplay> _mockDisplay;
        protected List<IPlayer> _players;
        protected Mock<IPlayer> _player1;
        protected Mock<IPlayer> _player2;
        protected Mock<IPlayer> _player3;
        protected Mock<ICard> _mockCard;

        [SetUp]
        public virtual void Setup()
        {
            _mockDeck = new Mock<IDeck>();
            _mockDisplay = new Mock<IDisplay>();
            _mockCard = new Mock<ICard>();

            _player1 = new Mock<IPlayer>();
            _player2 = new Mock<IPlayer>();
            _player3 = new Mock<IPlayer>();

            _player1.Setup(p => p.Name).Returns("Player1");
            _player2.Setup(p => p.Name).Returns("Player2");
            _player3.Setup(p => p.Name).Returns("Player3");

            _players = new List<IPlayer> { _player1.Object, _player2.Object, _player3.Object };

            _mockDeck.Setup(d => d.Draw()).Returns(_mockCard.Object);

            _gameController = new GameController(_players, _mockDeck.Object, _mockDisplay.Object);
        }
    }
}