namespace UnoGame.Interfaces
{
    public interface IDeck
    {
        void GenerateCards();
        void Shuffle();
        ICard Draw();
        void MoveCardToDiscarded(ICard card);
        void RecycleDiscarded();
    }
}