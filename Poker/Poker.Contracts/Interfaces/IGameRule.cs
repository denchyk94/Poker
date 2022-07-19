using Poker.Contracts.Models;
using System.Collections.Generic;

namespace Poker.Contracts.Interfaces
{
    /// <summary>
    /// Game rule. Used to determine winners.
    /// </summary>
    public interface IGameRule
    {
        /// <summary>
        /// Processes the game to get winners.
        /// </summary>
        /// <param name="game">Game with players.</param>
        void Process(Game game);

        /// <summary>
        /// Determines if game has a winners.
        /// </summary>
        /// <returns>True if there are winners. Otherwise false.</returns>
        bool HasWinner();

        /// <summary>
        /// Returnes list of winners if any.
        /// </summary>
        /// <returns>IEnumerable of Player.</returns>
        IEnumerable<Player> GetWinners();
    }
}
