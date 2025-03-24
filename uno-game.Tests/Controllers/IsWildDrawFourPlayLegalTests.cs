using System.Reflection;
using UnoGame.Models;

namespace uno_game.Tests.Controllers
{
    [TestFixture]
    public class IsWildDrawFourPlayLegalTests : GameControllerTestBase
    {
        private MethodInfo _isWildDrawFourPlayLegalMethod = null!;

       [SetUp]
       public override void Setup()
       {
           base.Setup();
       
           _isWildDrawFourPlayLegalMethod = typeof(GameController).GetMethod(
               "IsWildDrawFourPlayLegal",
               BindingFlags.NonPublic | BindingFlags.Instance) 
               ?? throw new InvalidOperationException("IsWildDrawFourPlayLegal method not found on GameController");
       }

        [Test]
        public void IsWildDrawFourPlayLegal_PlayerHasNoMatchingColorCards_ReturnsTrue()
        {
            // Arrange
            var player = _players[0];
            
            _controller.GetPlayerCards(player).ForEach(card => _controller.RemoveCardFromhand(player, card));
            _controller.AddCardToHand(player, _blueEight);
            _controller.AddCardToHand(player, _yellowSkip);
            _controller.AddCardToHand(player, _greenReverse);
            _controller.AddCardToHand(player, _wildDrawFourCard);
            
            // Act
            bool result = (bool)(_isWildDrawFourPlayLegalMethod!.Invoke(_controller, new object[] { player })!);
            
            // Assert
            Assert.That(result, Is.True, "IsWildDrawFourPlayLegal should return true when player has no cards matching LastPlayedCard's color");
        }

        [Test]
        public void IsWildDrawFourPlayLegal_PlayerHasMatchingColorCard_ReturnsFalse()
        {
            // Arrange
            var player = _players[0];
            
            _controller.GetPlayerCards(player).ForEach(card => _controller.RemoveCardFromhand(player, card));
            _controller.AddCardToHand(player, _blueEight);
            _controller.AddCardToHand(player, _redDrawTwo);
            _controller.AddCardToHand(player, _yellowSkip);
            _controller.AddCardToHand(player, _wildDrawFourCard);
            
            // Act
            bool result = (bool)(_isWildDrawFourPlayLegalMethod!.Invoke(_controller, new object[] { player })!);
            
            // Assert
            Assert.That(result, Is.False, "IsWildDrawFourPlayLegal should return false when player has cards matching LastPlayedCard's color");
        }

        [Test]
        public void IsWildDrawFourPlayLegal_PlayerHasOnlyWildDrawFour_ReturnsTrue()
        {
            // Arrange
            var player = _players[0];
            
            _controller.GetPlayerCards(player).ForEach(card => _controller.RemoveCardFromhand(player, card));
            _controller.AddCardToHand(player, _wildDrawFourCard);
            
            // Act
            bool result = (bool)(_isWildDrawFourPlayLegalMethod!.Invoke(_controller, new object[] { player })!);
            
            // Assert
            Assert.That(result, Is.True, "IsWildDrawFourPlayLegal should return true when player has only Wild Draw Four cards");
        }

        [Test]
        public void IsWildDrawFourPlayLegal_AfterColorChange_ReturnsFalseWhenHasNewColorMatch()
        {
            // Arrange
            var player = _players[0];
            
            typeof(GameController)
                .GetProperty("LastPlayedCard")
                ?.SetValue(_controller, _blueEight);
                
            _controller.GetPlayerCards(player).ForEach(card => _controller.RemoveCardFromhand(player, card));
            _controller.AddCardToHand(player, _blueEight);
            _controller.AddCardToHand(player, _yellowSkip);
            _controller.AddCardToHand(player, _wildDrawFourCard);
            
            // Act
            bool result = (bool)(_isWildDrawFourPlayLegalMethod!.Invoke(_controller, new object[] { player })!);
            
            // Assert
            Assert.That(result, Is.False, "IsWildDrawFourPlayLegal should return false when player has cards matching the new LastPlayedCard's color");
        }
    }
}