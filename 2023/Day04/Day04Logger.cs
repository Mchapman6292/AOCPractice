using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOCPractice.BaseLoggers;

namespace AdventOfCode._2023.Day04
{
    public class Day04Logger : BaseLogger
    {

        public void LogScratchCardGames(Dictionary<int, Dictionary<string, List<int>>> cardGames, List<int> cardNumbers)
        {
            if(cardGames == null || cardGames.Count == 0)
            {
                _logger.Information($"CardGames is empty for {nameof(LogScratchCardGames)}.");
                return;
            }

            foreach(int cardNumber in cardNumbers)
            {
                if(cardGames.TryGetValue(cardNumber, out var game))
                {
                    _logger.Information($" Card: {cardNumber}");
                    foreach (var (category, numbers) in game)
                    {
                        _logger.Information($"    {category}: [{string.Join(", ", numbers)}]");
                    }
                }
                else
                {
                    _logger.Information($"Card {cardNumber} not found in games dictionary");
                }
            }
        }

    }
}
