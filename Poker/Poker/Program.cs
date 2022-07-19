using Microsoft.Extensions.DependencyInjection;
using Poker.Application.Services;
using Poker.Contracts.Interfaces;
using System;
using System.Linq;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Register services
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICardService, CardService>()
                .AddSingleton<IPlayerService, PlayerService>()
                .AddSingleton<IGameService, GameService>()
                .AddSingleton<IGameRule, FlushGameRule>()
                .AddSingleton<IGameRule, ThreeOfAKindGameRule>()
                .AddSingleton<IGameRule, OnePairGameRule>()
                .AddSingleton<IGameRule, HighCardGameRule>()
                .BuildServiceProvider();

            // Play game
            Guid gameId = Guid.NewGuid();

            // Fetch players with cards
            var playersService = serviceProvider.GetService<IPlayerService>();
            var players = playersService.GetPlayers(gameId);

            // Process game
            var gameService = serviceProvider.GetService<IGameService>();
            var game = gameService.StartGame(gameId, players);
            gameService.PlayGame(game);

            // Display players with cards
            Console.WriteLine("--------------------");
            Console.WriteLine("Players: ");
            foreach (var winner in game.Players)
            {
                Console.WriteLine("Name: " + winner.Name);
                Console.WriteLine("Cards: ");
                foreach (var card in winner.Cards)
                {
                    Console.WriteLine(card.ToString());
                }

                Console.WriteLine("--------------------");
            }

            Console.WriteLine("--------------------");

            // Show result
            Console.WriteLine("Winners: ");
            if (!game.Winners.Any())
            {
                Console.WriteLine("There are no winners.");
            }
            
            foreach (var winner in game.Winners)
            {
                Console.WriteLine("Name: " + winner.Name);
                Console.WriteLine("Cards: ");
                foreach(var card in winner.Cards)
                {
                    Console.WriteLine(card.ToString());
                }

                Console.WriteLine("--------------------");
            }

            Console.Read();
        }
    }
}
