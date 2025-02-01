using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode._2023.Day04Loggers;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AdventOfCode._2023.Day04.Day04PartTwo;

namespace AdventOfCode._2023.Day04.Day04Answers
{
    public class Day04Answer
    {
        public Day04InputFormatter day04InputFormatter { get; set; }
        private readonly Day04Logger _day04Logger;

        public Day04Part2 day04Part2 { get; set; }

        // List of winning numbers & list of actual numbers.
        // One card worth 1 point.
        // Every subsequent match doubles the score from this point. 


        // Can create classes for winning numbers & match numbers, used this approach last time so will try something different for this one.
        // If we can find a way to differentiate the winning numbers from actual numbers with a bool value etc we can then look for duplicates in the full list of numbers.
        // Simplest approach is probably to create a dict of winning numbers and check each number against this.


        public Day04Answer(Day04InputFormatter inputFormat, Day04Logger d4Logger, Day04Part2 part2)
        {
            day04InputFormatter = inputFormat;
            _day04Logger = d4Logger;
            day04Part2 = part2;
        }



        /// <summary>
        /// Main solution method for Part 1
        /// Processes all scratchcards and calculates total points
        /// </summary>
        public void Answer()
        {
            _day04Logger.Info("Starting Day 4 Part 1 solution...");

            // Get input data
            string[] cards = day04InputFormatter.GenerateFullSplitInput();
            _day04Logger.Info($"Processing {cards.Length} scratchcards");

            // Calculate total points for all cards
            int totalPoints = GetTotalMatches(cards);

            _day04Logger.Info($"Part 1 solution completed. Total points: {totalPoints}");
            Console.WriteLine($"Part 1 Answer: {totalPoints}");
        }

        /// <summary>
        /// Calculates total points across all scratchcards
        /// Each card's points are based on number of matches:
        /// First match = 1 point, each subsequent match doubles the points
        /// </summary>
        public int GetTotalMatches(string[] cardGameString)
        {
            int gameTotal = 0;
            _day04Logger.Info("Beginning card match calculations...");

            var cardValues = day04InputFormatter.ExtractCardValues(cardGameString);

            foreach (var game in cardValues)
            {
                // Extract game data
                var gameNumber = game.Key;
                var winningNumbers = game.Value["WinningNumbers"];
                var actualNumbers = game.Value["ActualNumbers"];

                // Find matching numbers
                var matchingNumbers = winningNumbers.Intersect(actualNumbers).ToList();
                int matchCount = matchingNumbers.Count;

                // Calculate score for this card
                int cardScore = CalculateCardScore(matchCount);
                gameTotal += cardScore;

                _day04Logger.Info($"Card {gameNumber}: {matchCount} matches, Score: {cardScore}");
            }

            _day04Logger.Info($"Total score across all cards: {gameTotal}");
            return gameTotal;
        }

        /// <summary>
        /// Calculates points for a single card based on number of matches
        /// 0 matches = 0 points
        /// 1 match = 1 point
        /// Each additional match doubles the points
        /// </summary>
        private int CalculateCardScore(int matchCount)
        {
            // Handle base cases
            if (matchCount == 0) return 0;
            if (matchCount == 1) return 1;

            // Calculate doubled points for additional matches
            int score = 1;
            for (int i = 1; i < matchCount; i++)
            {
                score *= 2;
            }

            return score;
        }
    }
}