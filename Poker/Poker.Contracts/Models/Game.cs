using System;
using System.Collections.Generic;

namespace Poker.Contracts.Models
{
    public class Game
    {
        private readonly Guid m_id;
        private IEnumerable<Player> m_players;
        private IEnumerable<Player> m_winners;

        public Guid Id { get { return m_id; } }
        public IEnumerable<Player> Players { get { return m_players; } }
        public IEnumerable<Player> Winners { get { return m_winners; } }

        public Game(Guid id, IEnumerable<Player> players)
        {
            m_id = id;
            m_players = players;
            m_winners = new List<Player>();
        }

        public void SetWinners(IEnumerable<Player> winners) 
        {
            m_winners = winners;
        }
    }
}
