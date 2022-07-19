using Poker.Contracts.Constants;
using Poker.Contracts.Enums;
using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Application.Services
{
    /// <summary>
    /// If two or more players hold a single pair, then highest pair wins.
    /// If the pairs are of the same value, the highest kicker card determines the winner.
    /// A second and even third kicker can be used if necessary.
    /// </summary>
    public class OnePairGameRule : IGameRule
    {
        private IEnumerable<Player> m_winners;

        /// <summary>
        /// Processes the game to get winners.
        /// </summary>
        /// <param name="game">Game with players.</param>
        public void Process(Game game)
        {
            Player winner = null;
            Rank? winnerRank = null;

            foreach (var player in game.Players)
            {
                var cards = player.Cards;
                if (cards.Count != Constants.UserCardsAmount)
                    throw new InvalidOperationException($"Cards amount should be {Constants.UserCardsAmount}");

                // Find if there is one pair
                var ranks = cards.GroupBy(x => x.Rank)
                  .Where(g => g.Count() == Constants.OnePairGameRuleCardsAmount)
                  .Select(y => new { Element = y.Key, Counter = y.Count() })
                  .ToList();

                if (ranks.Any())
                {
                    // Find the highest pair rank
                    Rank? highestRank = null;
                    foreach (var rank in ranks)
                    {
                        var maxRank = cards.Where(c => c.Rank == rank.Element)
                            .Max(x => x.Rank);
                        if (highestRank != null && (int)highestRank < (int)maxRank)
                            highestRank = maxRank;
                    }

                    if (winner != null && winnerRank < highestRank)
                    {
                        winner = player;
                        winnerRank = highestRank;
                    }
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
