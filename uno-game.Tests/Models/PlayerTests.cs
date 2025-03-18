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
            var player = new Player(1, "Alice");

            string result = player.ToString();

            Assert.That(result, Is.EqualTo("Player 1: Alice"));
        }
    }
}