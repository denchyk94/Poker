using Poker.Contracts.Enums;
using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Application.Services
{
    /// <summary>
    /// Card service
    /// </summary>
    public class CardService : ICardService
    {
        private List<Card> m_cards = new List<Card>();
        private List<Card> m_selectedCards = new List<Card>();

        public CardService()
        {
            m_cards = InitializeCards().ToList();
        }

        // Get random card
        public Card GetCard()
        {
            var random = new Random();

            bool isFound = false;
            while (!isFound)
            {
                var index = random.Next(m_cards.Count());
                var randomCard = m_cards.ElementAt(index);

                if (!m_selectedCards.Contains(randomCard))
                {
                    m_selectedCards.Add(randomCard);
                    isFound = true;

                    return randomCard;
                }
            }

            throw new InvalidOperationException("Card not found.");
        }

        // Init default cards
        private IEnumerable<Card> InitializeCards()
        {
            var cards = new List<Card>();
            foreach(Rank rank in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    var card = new Card(rank, suit);
                    cards.Add(card);
                }
            }

            return cards;
        }
    }
}
