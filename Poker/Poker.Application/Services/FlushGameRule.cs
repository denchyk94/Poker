using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Application.Services
{
    /// <summary>
    /// A flush is any hand with five cards of the same suit. 
    /// If two or more players hold a flush, the flush with the highest card wins.
    /// If more than one player has the same strength high card, 
    /// then the strength of the second highest card held wins.
    /// This continues through the five highest cards in the player's hands. 
    /// </summary>
    public class FlushGameRule : IGameRule
    {
        private IEnumerable<Player> m_winners;

        /// <summary>
        /// Processes the game to get winners.
        /// </summary>
        /// <param name="game">Game with players.</param>
        public void Process(Game game)
        {
            Player winner = null;
            foreach (var player in game.Players)
            {
                // Check if all cards have the same suit.
                var firstCard = player.Cards.First();
                var isFlush = player.Cards.All(c => c.Suit == firstCard.Suit);

                // Check if winner already present. 
                // Validate if current winner should be changed.
                if(winner != null)
                {
                    var winnerSumRank = winner.Cards.Sum(c => (int)c.Rank);
                    var currentPlayerSumRank = player.Cards.Sum(c => (int)c.Rank);

                    // Winner should have the highest summary rank.
                    if(currentPlayerSumRank > winnerSumRank)
                        winner = player;
                }
            }

            // Assing winners if any winner found.
            if (winner != null)
                m_winners = new List<Player> { winner };
        }

        /// <summary>
        /// Determines if game has a winners.
        /// </summary>
        /// <returns>True if there are winners. Otherwise false.</returns>
        public bool HasWinner()
        {
            return m_winners != null && m_winners.Any();
        }

        /// <summary>
        /// Returnes list of winners if any.
        /// </summary>
        /// <returns>IEnumerable of Player.</returns>
        public IEnumerable<Player> GetWinners()
        {
            return m_winners;
        }
    }
}
