using UnoGame.Enums;

namespace UnoGame.Interfaces
{
    public interface ICard
    {
        Color Color { get; }
        Score Score { get; }
        Effect Effect { get; }
    }
}