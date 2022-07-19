using Poker.Contracts.Models;
using System;
using System.Collections.Generic;

namespace Poker.Contracts.Interfaces
{
    /// <summary>
    /// Game service interface.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Creates game object and inits players with cards.
        /// </summary>
        /// <param name="id">Game id</param>
        /// <param name="players">Players</param>
        /// <returns>Created game.</returns>
        Game StartGame(Guid id, IEnumerable<Player> players);

        /// <summary>
        /// Plays a game to found winner.
        /// </summary>
        /// <param name="game">Current game.</param>
        void PlayGame(Game game);
    }
}
