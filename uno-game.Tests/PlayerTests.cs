using UnoGame.Models;

namespace uno_game.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Constructor_SetsPropertiesCorrectly()
        {
            int expectedId = 1;
            string expectedName = "TestPlayer";

            var player = new Player(expectedId, expectedName);

            Assert.Multiple(() =>
            {
                Assert.That(player.Id, Is.EqualTo(expectedId));
                Assert.That(player.Name, Is.EqualTo(expectedName));
            });
        }

        [Test]
        public void ToString_ReturnsPlayerNameAndId()
        {
            var player = new Player(42, "Alice");

            string result = player.ToString();
            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo("Player 42: Alice"));
        }

        [Test]
        public void Equals_WithDifferentId_ReturnsFalse()
        {
            var player1 = new Player(1, "Player One");
            var player2 = new Player(2, "Player One");

            Assert.That(player1.Equals(player2), Is.False);
        }
    }
}