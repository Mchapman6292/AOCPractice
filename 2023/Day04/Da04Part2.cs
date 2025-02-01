using AdventOfCode._2023.Day04.Day04Inputs;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using AdventOfCode._2023.Day04Loggers;
using System.Collections.Specialized;

namespace AdventOfCode._2023.Day04.Day04PartTwo
{
    public class Day04Part2
    {
        public Day04InputFormatter day04InputFormatter { get; set; }
        public Day04Input day04Input { get; set; }
        private readonly Day04Logger _day04Logger;

        // Tracks how many matches each card has (fixed value per card)
        private Dictionary<int, long> _cardMatchesDict { get; set; }
        private Dictionary<int, long> _cardCopiesDict { get; set; }
        private int _highestCardNumber;

        public Day04Part2(Day04InputFormatter inputFormat, Day04Input d04Input, Day04Logger d04Logger)
        {
            day04InputFormatter = inputFormat;
            _cardMatchesDict = new Dictionary<int, long>();
            _cardCopiesDict = new Dictionary<int, long>();
            day04Input = d04Input;
            _day04Logger = d04Logger;
        }

        public long CalculateTotalScratchcards()
        {
            _day04Logger.Info("Starting Day 4 Part 2 solution...");

            // Get and prepare input data
            string[] cards = day04InputFormatter.GenerateFullSplitInput();
            _day04Logger.Info($"Processing {cards.Length} original scratchcards");

            // Initialize data structures
            InitializeCardData(cards);
            ProcessAllCardMatches(cards);

            // Calculate and return final total
            long totalCards = _cardCopiesDict.Values.Sum(x => (long)x);
            _day04Logger.Info($"Final scratchcard count: {totalCards}");

            return totalCards;
        }

        /// <summary>
        /// Sets up initial state:
        /// - Calculates matches for each card
        /// - Sets initial card copies to 1 (originals)
        /// - Determines highest card number
        /// </summary>
        private void InitializeCardData(string[] cards)
        {
            _day04Logger.Info("Initializing card data...");

            // Set up original cards and their match counts
            foreach (int cardNumber in day04InputFormatter.ExtractAllCardNumbers(cards))
            {
                // Each card starts with 1 copy (the original)
                _cardCopiesDict.Add(cardNumber, 1);

                // Calculate and store number of matches for this card
                long matches = GetMatchCountFromCard(cards[cardNumber - 1]);
                _cardMatchesDict.Add(cardNumber, matches);

                _day04Logger.Info($"Card {cardNumber}: {matches} matches");
            }

            // Store highest card number for range validation
            _highestCardNumber = _cardMatchesDict.Keys.Max();
            _day04Logger.Info($"Highest card number: {_highestCardNumber}");
        }

        /// <summary>
        /// Processes each card and its copies to generate new copies
        /// Cards are processed in sequence to handle the cascading effect correctly
        /// </summary>
        private void ProcessAllCardMatches(string[] cards)
        {
            _day04Logger.Info("Processing card matches and generating copies...");

            foreach (string card in cards)
            {
                int cardNumber = day04InputFormatter.ExtractCardNumber(card);
                long matchCount = _cardMatchesDict[cardNumber];
                long currentCopies = _cardCopiesDict[cardNumber];

                // Update subsequent cards based on matches
                UpdateCardCopies(cardNumber, matchCount, currentCopies);

                _day04Logger.Info($"Processed Card {cardNumber}: {currentCopies} copies generated {matchCount} matches each");
            }
        }

        /// <summary>
        /// Updates copy counts for subsequent cards based on current card's matches
        /// For each copy of the current card, adds one copy to each of the next [matchCount] cards
        /// </summary>
        private void UpdateCardCopies(int currentCard, long matchCount, long copyCount)
        {
            long range = CalculateValidRange(currentCard, matchCount);

            for (int nextCard = currentCard + 1; nextCard <= currentCard + range; nextCard++)
            {
                _cardCopiesDict[nextCard] += copyCount;
                _day04Logger.Info($"Card {nextCard} updated to {_cardCopiesDict[nextCard]} copies");
            }
        }

        /// <summary>
        /// Calculates match count for a single card by comparing winning and actual numbers
        /// </summary>
        private long GetMatchCountFromCard(string cardData)
        {
            List<int> winningNumbers = day04InputFormatter.ExtractWinNumbers(cardData);
            List<int> actualNumbers = day04InputFormatter.ExtractActualNumbers(cardData);
            return winningNumbers.Intersect(actualNumbers).Count();
        }

        /// <summary>
        /// Ensures we don't try to copy cards beyond the highest card number
        /// </summary>
        private long CalculateValidRange(int cardNumber, long matchCount)
        {
            if (cardNumber + matchCount <= _highestCardNumber)
            {
                return matchCount;
            }
            return _highestCardNumber - cardNumber;
        }
    }
}