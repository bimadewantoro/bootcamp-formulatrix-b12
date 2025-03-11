using UnoGame.Enums;
using UnoGame.Interfaces;

namespace UnoGame.Models
{
    public class GameController
    {
        private readonly IDeck _deck;
        private readonly Dictionary<IPlayer, List<ICard>> _cardInHands;
        private readonly Dictionary<IPlayer, int> _playerScores;
        private readonly IDisplay _display;
        private readonly List<IPlayer> _players;
        
        public int RoundCount { get; private set; }
        private IPlayer _currentPlayer;
        private IPlayer _roundWinner;
        private bool _isTurnEnded;
        private int _turnDirection;
        
        public ICard LastPlayedCard { get; private set; }
        
        public event Action OnRoundStart;
        public event Action<ICard> OnCardPlay;

        public GameController(List<IPlayer> players, IDeck deck, IDisplay display)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            _deck = deck ?? throw new ArgumentNullException(nameof(deck));
            _display = display ?? throw new ArgumentNullException(nameof(display));
            
            _cardInHands = new Dictionary<IPlayer, List<ICard>>();
            _playerScores = new Dictionary<IPlayer, int>();
            
            foreach (var player in players)
            {
                _cardInHands[player] = new List<ICard>();
                _playerScores[player] = 0;
            }
            
            RoundCount = 0;
            _turnDirection = 1;
            
            _currentPlayer = players.First();
        }

        public void DistributeCards()
        {
            foreach (var player in _players)
            {
                _cardInHands[player].Clear();
            }
            
            const int initialHandSize = 7;
            for (int i = 0; i < initialHandSize; i++)
            {
                foreach (var player in _players)
                {
                    AddCardToHand(player, _deck.Draw());
                }
            }
            
            ICard firstCard;
            do
            {
                firstCard = _deck.Draw();
                
                if (firstCard.Effect == Effect.WildDrawFour)
                {
                    _deck.MoveCardToDiscarded(firstCard);
                    _deck.RecycleDiscarded();
                    continue;
                }
                
                break;
            } while (true);
            
            _deck.MoveCardToDiscarded(firstCard);
            LastPlayedCard = firstCard;
            
            if (firstCard.Effect != Effect.NoEffect && 
                firstCard.Effect != Effect.Wild &&
                firstCard.Effect != Effect.WildDrawFour)
            {
                HandleCardEffect(firstCard);
            }
            
            OnRoundStart?.Invoke();
        }

        public void EndTurn()
        {
            _isTurnEnded = true;
        }

        public bool NextTurn()
        {
            if (_cardInHands[_currentPlayer].Count == 0)
            {
                _roundWinner = _currentPlayer;
                return false;
            }
            
            int currentIndex = _players.IndexOf(_currentPlayer);
            
            int nextIndex = (currentIndex + _turnDirection + _players.Count) % _players.Count;
            
            _currentPlayer = _players[nextIndex];
            _isTurnEnded = false;
            
            return true;
        }

        public void PassTurn()
        {
            EndTurn();
        }

        public bool SayUno()
        {
            if (_cardInHands[_currentPlayer].Count == 1)
            {
                return true;
            }
            return false;
        }

        public bool DrawCard(IPlayer player)
        {
            ICard card = _deck.Draw();
            if (card != null)
            {
                return AddCardToHand(player, card);
            }
            return false;
        }

        public bool AddCardToHand(IPlayer player, ICard card)
        {
            if (player != null && card != null && _cardInHands.ContainsKey(player))
            {
                _cardInHands[player].Add(card);
                return true;
            }
            return false;
        }

        public bool PlayCard(IPlayer player, ICard card)
        {
            if (player != null && card != null && _cardInHands.ContainsKey(player))
            {
                if (HasCardInHand(player, card) && IsCardPlayable(card))
                {
                    RemoveCardFromhand(player, card);
                    _deck.MoveCardToDiscarded(card);
                    LastPlayedCard = card;
                    
                    HandleCardEffect(card);
                    
                    NotifyCardPlayed(card);
                    
                    return true;
                }
            }
            return false;
        }

        public bool IsCardPlayable(ICard card)
        {
            if (card == null) return false;
            
            if (card.Effect == Effect.Wild || card.Effect == Effect.WildDrawFour)
            {
                return true;
            }
            
            return card.Color == LastPlayedCard.Color || 
                   card.Effect == LastPlayedCard.Effect || 
                   card.Score == LastPlayedCard.Score;
        }

        public bool RemoveCardFromhand(IPlayer player, ICard card)
        {
            if (player != null && card != null && _cardInHands.ContainsKey(player))
            {
                return _cardInHands[player].Remove(card);
            }
            return false;
        }

        public bool HasCardInHand(IPlayer player, ICard card)
        {
            if (player != null && card != null && _cardInHands.ContainsKey(player))
            {
                return _cardInHands[player].Contains(card);
            }
            return false;
        }

        public int CountCardInHand(IPlayer player)
        {
            if (player != null && _cardInHands.ContainsKey(player))
            {
                return _cardInHands[player].Count;
            }
            return 0;
        }

        public int GetPlayerScore(IPlayer player)
        {
            if (player != null && _playerScores.ContainsKey(player))
            {
                return _playerScores[player];
            }
            return 0;
        }

        public void AddPlayerScore(IPlayer player, int amount)
        {
            if (player != null && _playerScores.ContainsKey(player))
            {
                _playerScores[player] += amount;
            }
        }

        public void ReverseTurnOrder()
        {
            _turnDirection *= -1;
        }

        public bool ForcedDraw(IPlayer player, int amount)
        {
            if (player == null || !_cardInHands.ContainsKey(player) || amount <= 0)
            {
                return false;
            }
            
            bool allCardsDrawn = true;
            for (int i = 0; i < amount; i++)
            {
                if (!DrawCard(player))
                {
                    allCardsDrawn = false;
                }
            }
            
            return allCardsDrawn;
        }

        public bool SkipTurn(IPlayer player)
        {
            if (player != null && player.Equals(_currentPlayer))
            {
                EndTurn();
                return true;
            }
            return false;
        }

        public Color SelectColor()
        {
            return Color.Red;
        }

        public void HandleCardEffect(ICard card)
        {
            switch (card.Effect)
            {
                case Effect.DrawTwo:
                    int nextPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    ForcedDraw(_players[nextPlayerIndex], 2);
                    SkipTurn(_players[nextPlayerIndex]);
                    break;
                    
                case Effect.Reverse:
                    ReverseTurnOrder();
                    break;
                    
                case Effect.Skip:
                    int skipPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    SkipTurn(_players[skipPlayerIndex]);
                    break;
                    
                case Effect.Wild:
                    break;
                    
                case Effect.WildDrawFour:
                    int drawFourPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    ForcedDraw(_players[drawFourPlayerIndex], 4);
                    SkipTurn(_players[drawFourPlayerIndex]);
                    break;
            }
        }

        public void NotifyCardPlayed(ICard card)
        {
            OnCardPlay?.Invoke(card);
        }

        public int GetCardAmountInHand(IPlayer player)
        {
            return CountCardInHand(player);
        }

        public IPlayer GetPreviousPlayer()
        {
            int currentIndex = _players.IndexOf(_currentPlayer);
            int previousIndex = (currentIndex - _turnDirection + _players.Count) % _players.Count;
            return _players[previousIndex];
        }

        public bool ChallengeOnDrawFour(IPlayer player)
        {
            return false;
        }

        public int CountRoundScore()
        {
            if (_roundWinner == null) return 0;
            
            int score = 0;
            foreach (var player in _players)
            {
                if (player.Equals(_roundWinner)) continue;
                
                foreach (var card in _cardInHands[player])
                {
                    score += (int)card.Score;
                }
            }
            
            AddPlayerScore(_roundWinner, score);
            return score;
        }
    }
}