using UnoGame.Interfaces;

namespace UnoGame.Models
{
    public class Player : IPlayer
    {
        public int Id { get; }
        public string Name { get; }

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Player {Id}: {Name}";
        }
    }
}