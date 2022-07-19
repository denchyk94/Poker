using Poker.Contracts.Interfaces;
using Poker.Contracts.Models;
using System;
using System.Collections.Generic;

namespace Poker.Application.Services
{
    /// <summary>
    /// Game service.
    /// </summary>
    public class GameService : IGameService
    {
        private readonly IEnumerable<IGameRule> m_rules;

        public GameService(IEnumerable<IGameRule> rules)
        {
            m_rules = rules;
        }

        /// <summary>
        /// Creates game object and inits players with cards.
        /// </summary>
        /// <param name="id">Game id</param>
        /// <param name="players">Players</param>
        /// <returns>Created game.</returns>
        public Game StartGame(Guid id, IEnumerable<Player> players)
        {
            var game = new Game(id, players);
            return game;
        }

        /// <summary>
        /// Plays a game to found winner.
        /// </summary>
        /// <param name="game">Current game.</param>
        public void PlayGame(Game game)
        {
            // Operate thought all rules and find the winner.
            foreach(var rule in m_rules)
            {
                // Process rule.
                rule.Process(game);
                // Check is any winner available.
                if (rule.HasWinner())
                {
                    game.SetWinners(rule.GetWinners());
                    break;
                }
            }            
        }
    }
}
