using Poker.Contracts.Enums;

namespace Poker.Contracts.Models
{
    public class Card
    {
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Rank} {Suit}";
        }
    }
}
