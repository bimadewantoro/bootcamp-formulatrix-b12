using UnoGame.Enums;
using UnoGame.Interfaces;

namespace UnoGame.Models
{
    public class Card : ICard
    {
        public Color Color { get; }
        public Effect Effect { get; }
        public Score Score { get; }

        public Card(Color color, Effect effect, Score score)
        {
            Color = color;
            Effect = effect;
            Score = score;
        }

        public override string ToString()
        {
            string colorName = Color.ToString();

            if (Effect == Effect.NoEffect)
            {
                return $"{colorName} {(int)Score}";
            }
            else
            {
                return $"{colorName} {Effect}";
            }
        }
    }
}