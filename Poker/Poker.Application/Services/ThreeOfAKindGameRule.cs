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
    ///If more than one player holds three of a kind, 
    ///then the higher value of the cards used to make the three of kind determines 
    ///the winner. If two or more players have the same three of a kind, 
    ///then a fourth card (and a fifth if necessary) can be used as kickers 
    ///to determine the winner.
    /// </summary>
    public class ThreeOfAKindGameRule : IGameRule
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
                if(cards.Count != Constants.UserCardsAmount)
                    throw new InvalidOperationException($"Cards amount should be {Constants.UserCardsAmount}");

                // Find if there are three of a kind 
                var rank = cards.GroupBy(x => x.Rank)
                  .Where(g => g.Count() == Constants.ThreeOfAKindGameRuleCardsAmount)
                  .Select(y => new { Element = y.Key, Counter = y.Count() })
                  .FirstOrDefault();

                if(rank != null)
                {
                    // Check remaining cards to find winner
                    var remainingCards = cards.Where(x => x.Rank != rank.Element)
                        .OrderByDescending(x => x.Rank);
                    var firstCardRank = remainingCards.ElementAt(0).Rank;
                    var secondCardRank = remainingCards.ElementAt(1).Rank;

                    // Remaining cards should have different ranks.
                    if (firstCardRank != secondCardRank)
                    {
                        if(winner != null)
                        {
                            // Compare remaining cards ranks if current three ranks are similar.
                            if(winnerRank == rank.Element)
                            {
                                var currentWinnerCards = winner.Cards
                                    .Where(x => x.Rank != winnerRank);
                                var firstCurrentWinnerCardRank = remainingCards.ElementAt(0).Rank;
                                var secondCurrentWinnerCardRank = remainingCards.ElementAt(1).Rank;

                                // Compare the fourth card rank
                                if(firstCardRank > firstCurrentWinnerCardRank)
                                {
                                    winner = player;
                                    winnerRank = rank.Element;
                                }
                                else if(firstCardRank == firstCurrentWinnerCardRank)
                                {
                                    // Compare fifth card rank
                                    if(secondCardRank > secondCurrentWinnerCardRank)
                                    {
                                        winner = player;
                                        winnerRank = rank.Element;
                                    }
                                }
                            }
                        }
                        else
                        {
                            winner = player;
                            winnerRank = rank.Element;
                        }
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
