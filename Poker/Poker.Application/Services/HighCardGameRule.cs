using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Application.Services
{
    /// <summary>
    /// When no player has even a pair, then the highest card wins.
    /// When both players have identical high cards, the next highest card wins, 
    /// and so on until five cards have been used.
    /// In the unusual circumstance that two players hold the identical five cards, 
    /// the pot would be split.
    /// </summary>
    public class HighCardGameRule : IGameRule
    {
        private IEnumerable<Player> m_winners;

        /// <summary>
        /// Processes the game to get winners.
        /// </summary>
        /// <param name="game">Game with players.</param>
        public void Process(Game game)
        {
            Player winner = null;

            // Find players with the highest rank
            foreach (var player in game.Players)
            {
                var summaryRank = GetSummaryRank(player);
                var winnerSummaryRank = winner != null ? GetSummaryRank(player) : 0;

                if (summaryRank > winnerSummaryRank)
                    winner = player;
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

        /// <summary>
        /// Get total rank for a player
        /// </summary>
        /// <param name="player">Player</param>
        /// <returns>Summary rank</returns>
        private int GetSummaryRank(Player player)
        {
            int sum = 0;
            foreach(var card in player.Cards)
            {
                sum += (int)card.Rank;
            }

            return sum;
        }
    }
}
