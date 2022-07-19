using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System;
using System.Collections.Generic;

namespace Poker.Application.Services
{
    /// <summary>
    /// Player service
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private const int CardAmount = 5;
        private readonly ICardService m_cardService;

        public PlayerService(ICardService cardService)
        {
            m_cardService = cardService;
        }

        /// <summary>
        /// Get players for a game
        /// </summary>
        /// <param name="id">Game id</param>
        /// <param name="amount">Players amount</param>
        /// <returns>IEnumerable of Players</returns>
        public IEnumerable<Player> GetPlayers(Guid id, int amount = 3)
        {
            var players = new List<Player>();
            for(var i = 0; i < amount; i++)
            {
                var player = new Player($"Player {i}-{id.ToString()}");
                AddCards(player);
                players.Add(player);
            }

            return players;
        }

        private void AddCards(Player player)
        {
            for(var i = 0; i < CardAmount; i++)
            {
                var card = m_cardService.GetCard();
                player.AddCard(card);
            }
        }
    }
}
