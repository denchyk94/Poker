using Poker.Contracts.Models;
using System;
using System.Collections.Generic;

namespace Poker.Contracts.Interfaces
{
    /// <summary>
    /// Player service interface
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Get players for a game
        /// </summary>
        /// <param name="id">Game id</param>
        /// <param name="amount">Players amount</param>
        /// <returns>IEnumerable of Players</returns>
        IEnumerable<Player> GetPlayers(Guid id, int amount = 3);
    }
}
