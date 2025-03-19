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
        private IPlayer? _roundWinner;
        private bool _isTurnEnded;
        private int _turnDirection;
        private bool _lastWildDrawFourWasLegal;
        private IPlayer? _lastWildDrawFourPlayer;
        private bool _canChallengeWildDrawFour;
        private bool _successfulChallengeJustHandled = false;

        public ICard? LastPlayedCard { get; private set; }

        public event Action OnRoundStart = delegate { };
        public event Action<ICard> OnCardPlay = delegate { };

        public const int WinScore = 500;

        public GameController(List<IPlayer> players, IDeck deck, IDisplay display)
        {
            _players = players;
            _deck = deck;
            _display = display;

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

        public List<IPlayer> GetPlayers() => new List<IPlayer>(_players);

        public IPlayer GetCurrentPlayer() => _currentPlayer;

        public List<ICard> GetPlayerCards(IPlayer player)
        {
            if (player != null && _cardInHands.ContainsKey(player))
            {
                return new List<ICard>(_cardInHands[player]);
            }
            return new List<ICard>();
        }

        public int GetTurnDirection() => _turnDirection;

        public void StartNewRound()
        {
            RoundCount++;
            _deck.GenerateCards();
            _deck.Shuffle();
            DistributeCards();
        }

        public bool IsGameOver()
        {
            return _playerScores.Any(ps => ps.Value >= WinScore);
        }

        public IPlayer GetGameWinner()
        {
            return _playerScores.OrderByDescending(ps => ps.Value).First().Key;
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
                    ICard? drawnCard = _deck.Draw();
                    if (drawnCard != null)
                    {
                        AddCardToHand(player, drawnCard);
                    }
                }
            }

            ICard? firstCard;
            do
            {
                firstCard = _deck.Draw();
                if (firstCard == null)
                {
                    continue;
                }

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

        public void RecycleDiscardedCards()
        {
            _deck.RecycleDiscarded();
        }

        public void EndTurn()
        {
            _isTurnEnded = true;
        }

        public bool NextTurn()
        {
            if (_successfulChallengeJustHandled)
            {
                _successfulChallengeJustHandled = false;
                _isTurnEnded = false;
                return true;
            }

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
            ICard? card = _deck.Draw();
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
                    if (card.Effect == Effect.WildDrawFour)
                    {
                        _lastWildDrawFourWasLegal = IsWildDrawFourPlayLegal(player);
                        _lastWildDrawFourPlayer = player;
                        _canChallengeWildDrawFour = true;
                    }
                    else
                    {
                        _canChallengeWildDrawFour = false;
                    }

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

        public bool HasPlayableCard(IPlayer player)
        {
            if (player == null || !_cardInHands.ContainsKey(player))
            {
                return false;
            }

            return _cardInHands[player].Any(IsCardPlayable);
        }

        public bool IsCardPlayable(ICard card)
        {
            if (card == null || LastPlayedCard == null)
                return false;

            if (card.Effect == Effect.Wild || card.Effect == Effect.WildDrawFour)
                return true;

            if (card.Color == LastPlayedCard.Color)
                return true;

            bool isNumberCard = (int)card.Score >= 0 && (int)card.Score <= 9;
            bool isLastCardNumber = (int)LastPlayedCard.Score >= 0 && (int)LastPlayedCard.Score <= 9;
            if (isNumberCard && isLastCardNumber && card.Score == LastPlayedCard.Score)
                return true;

            if (card.Effect != Effect.NoEffect && card.Effect == LastPlayedCard.Effect)
                return true;

            return false;
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
            if (_display is Display enhancedDisplay)
            {
                enhancedDisplay.DisplayMessage("\nSelect a color:", ConsoleColor.Cyan);
                enhancedDisplay.DisplayMessage("1. Red", ConsoleColor.Red);
                enhancedDisplay.DisplayMessage("2. Green", ConsoleColor.Green);
                enhancedDisplay.DisplayMessage("3. Blue", ConsoleColor.Blue);
                enhancedDisplay.DisplayMessage("4. Yellow", ConsoleColor.Yellow);

                int choice = enhancedDisplay.GetNumericInput(1, 4);

                switch (choice)
                {
                    case 1: return Color.Red;
                    case 2: return Color.Green;
                    case 3: return Color.Blue;
                    case 4: return Color.Yellow;
                    default: return Color.Red;
                }
            }

            return Color.Red;
        }

        public void HandleCardEffect(ICard card)
        {
            switch (card.Effect)
            {
                case Effect.DrawTwo:
                    int nextPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    IPlayer nextPlayer = _players[nextPlayerIndex];
                    ForcedDraw(nextPlayer, 2);
                    if (_display is Display enhancedDisplay)
                    {
                        enhancedDisplay.DisplayMessage($"\n{nextPlayer.Name} draws 2 cards and loses their turn!", ConsoleColor.Magenta);
                    }
                    EndTurn();
                    NextTurn();
                    EndTurn();
                    break;

                case Effect.Reverse:
                    ReverseTurnOrder();
                    if (_display is Display enhancedDisplay2)
                    {
                        string direction = _turnDirection > 0 ? "clockwise" : "counter-clockwise";
                        enhancedDisplay2.DisplayMessage($"\nDirection changed to {direction}!", ConsoleColor.Magenta);
                    }
                    break;

                case Effect.Skip:
                    int skipPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    IPlayer skippedPlayer = _players[skipPlayerIndex];
                    if (_display is Display enhancedDisplay3)
                    {
                        enhancedDisplay3.DisplayMessage($"\n{skippedPlayer.Name}'s turn is skipped!", ConsoleColor.Magenta);
                    }
                    EndTurn();
                    NextTurn();
                    EndTurn();
                    break;

                case Effect.Wild:
                    Color selectedColor = SelectColor();
                    LastPlayedCard = new Card(selectedColor, Effect.Wild, Score.Wild);
                    if (_display is Display enhancedDisplay4)
                    {
                        enhancedDisplay4.DisplayMessage($"\nColor changed to {selectedColor}!", ConsoleColor.Magenta);
                    }
                    break;

                case Effect.WildDrawFour:
                    Color selectedColor2 = SelectColor();
                    LastPlayedCard = new Card(selectedColor2, Effect.WildDrawFour, Score.WildDrawFour);

                    int drawFourPlayerIndex = (_players.IndexOf(_currentPlayer) + _turnDirection + _players.Count) % _players.Count;
                    IPlayer drawFourPlayer = _players[drawFourPlayerIndex];

                    if (_display is Display enhancedDisplay5)
                    {
                        enhancedDisplay5.DisplayMessage($"\nColor changed to {selectedColor2}!", ConsoleColor.Magenta);
                    }

                    if (!_canChallengeWildDrawFour)
                    {
                        ForcedDraw(drawFourPlayer, 4);
                        if (_display is Display enhancedDisplay6)
                        {
                            enhancedDisplay6.DisplayMessage($"{drawFourPlayer.Name} draws 4 cards and loses their turn!", ConsoleColor.Magenta);
                        }
                        EndTurn();
                        NextTurn();
                        EndTurn();
                    }
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

        private bool IsWildDrawFourPlayLegal(IPlayer player)
        {
            foreach (var card in GetPlayerCards(player))
            {
                if (card.Effect == Effect.WildDrawFour) continue;

                if (LastPlayedCard != null && card.Color == LastPlayedCard.Color)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ChallengeOnDrawFour(IPlayer challenger)
        {
            if (!_canChallengeWildDrawFour || _lastWildDrawFourPlayer == null)
            {
                if (_display is Display enhancedDisplay)
                {
                    enhancedDisplay.DisplayMessage("\nNo Wild Draw Four to challenge!", ConsoleColor.Red);
                }
                return false;
            }

            _canChallengeWildDrawFour = false;

            if (!_lastWildDrawFourWasLegal)
            {
                ForcedDraw(_lastWildDrawFourPlayer, 4);

                _currentPlayer = challenger;
                _isTurnEnded = false;
                _successfulChallengeJustHandled = true;

                if (_display is Display enhancedDisplay)
                {
                    enhancedDisplay.DisplayMessage($"\nChallenge successful! {_lastWildDrawFourPlayer.Name} had a matching card and must draw 4 cards.", ConsoleColor.Green);
                    enhancedDisplay.DisplayMessage($"{challenger.Name} keeps their turn!", ConsoleColor.Green);
                }
                return true;
            }
            else
            {
                ForcedDraw(challenger, 6);
                EndTurn();

                if (_display is Display enhancedDisplay)
                {
                    enhancedDisplay.DisplayMessage($"\nChallenge failed! {_lastWildDrawFourPlayer.Name} had no matching cards. {challenger.Name} must draw 6 cards instead of 4.", ConsoleColor.Red);
                }
                return false;
            }
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

        public IPlayer? GetRoundWinner()
        {
            return _roundWinner;
        }

        public bool IsRoundOver()
        {
            return _roundWinner != null;
        }

        public void ResetRoundWinner()
        {
            _roundWinner = null;
        }
    }
}