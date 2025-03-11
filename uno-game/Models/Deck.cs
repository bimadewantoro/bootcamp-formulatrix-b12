using UnoGame.Enums;
using UnoGame.Interfaces;

namespace UnoGame.Models
{
    public class Deck : IDeck
    {
        private List<ICard> _drawableCards;
        private List<ICard> _discardedCards;
        private Random _random;

        public Deck()
        {
            _drawableCards = new List<ICard>();
            _discardedCards = new List<ICard>();
            _random = new Random();
            GenerateCards();
            Shuffle();
        }

        public void GenerateCards()
        {
            _drawableCards.Clear();
            _discardedCards.Clear();

            foreach (Color color in Enum.GetValues(typeof(Color)).Cast<Color>().Where(c => c != Color.Wild))
            {
                _drawableCards.Add(new Card(color, Effect.NoEffect, Score.Number0));

                for (int i = 1; i <= 9; i++)
                {
                    Score score = (Score)i;
                    _drawableCards.Add(new Card(color, Effect.NoEffect, score));
                    _drawableCards.Add(new Card(color, Effect.NoEffect, score));
                }

                for (int i = 0; i < 2; i++)
                {
                    _drawableCards.Add(new Card(color, Effect.DrawTwo, Score.DrawTwo));
                    _drawableCards.Add(new Card(color, Effect.Skip, Score.Skip));
                    _drawableCards.Add(new Card(color, Effect.Reverse, Score.Reverse));
                }
            }

            for (int i = 0; i < 4; i++)
            {
                _drawableCards.Add(new Card(Color.Wild, Effect.Wild, Score.Wild));
                _drawableCards.Add(new Card(Color.Wild, Effect.WildDrawFour, Score.WildDrawFour));
            }
        }

        public void Shuffle()
        {
            int n = _drawableCards.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                ICard value = _drawableCards[k];
                _drawableCards[k] = _drawableCards[n];
                _drawableCards[n] = value;
            }
        }

        public ICard Draw()
        {
            if (_drawableCards.Count == 0)
            {
                RecycleDiscarded();
                
                if (_drawableCards.Count == 0)
                {
                    return null;
                }
            }

            ICard card = _drawableCards[0];
            _drawableCards.RemoveAt(0);
            return card;
        }

        public void MoveCardToDiscarded(ICard card)
        {
            _discardedCards.Add(card);
        }

        public void RecycleDiscarded()
        {
            ICard? topCard = null;
            if (_discardedCards.Count > 0)
            {
                topCard = _discardedCards[_discardedCards.Count - 1];
                _discardedCards.RemoveAt(_discardedCards.Count - 1);
            }

            _drawableCards.AddRange(_discardedCards);
            _discardedCards.Clear();

            if (topCard != null)
            {
                _discardedCards.Add(topCard);
            }

            Shuffle();
        }
    }
}