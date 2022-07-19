using System;
using System.Collections.Generic;

namespace Poker.Contracts.Models
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Cards { get; } = new List<Card>();

        public Player(string name)
        {
            Name = name;
        }

        public void AddCard(Card card)
        {
            if(Cards.Count > 5)
            {
                throw new InvalidOperationException("Only 5 cards allowed for each player.");
            }

            Cards.Add(card);
        }
    }
}
