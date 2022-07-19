using Poker.Contracts.Models;

namespace Poker.Contracts.Interfaces
{
    /// <summary>
    /// Card service interface
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Returns a card
        /// </summary>
        /// <returns>Card entity</returns>
        Card GetCard();
    }
}
