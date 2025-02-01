using DocumentFormat.OpenXml.Packaging;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode._2023.Day04Loggers;
using AdventOfCode._2023.Day04.Day04Inputs;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AdventOfCode._2023.Day04.Day04PuzzleInputs
{
    /// <summary>
    /// Handles all input processing and formatting for Day 4 puzzle
    /// Includes methods to extract card numbers, winning numbers, and actual numbers from input strings
    /// </summary>
    public class Day04InputFormatter
    {
        private readonly Day04Logger day04Logger;
        private readonly Day04Input _input;

        public Day04InputFormatter(Day04Logger logger, Day04Input input)
        {
            day04Logger = logger;
            _input = input;
        }

        // Input Generation Methods
        public string[] GenerateFullSplitInput()
        {
            string fullInput = _input.FullInput;
            var result = SplitStringByNewLine(fullInput);
            day04Logger.Info($"Generated full input with {result.Length} lines");
            return result;
        }

        public string[] GenerateTestString()
        {
            string testInput = _input.TestInput;
            var result = SplitStringByNewLine(testInput);
            day04Logger.Info($"Generated test input with {result.Length} lines");
            return result;
        }

        // Core Extraction Methods
        public Dictionary<int, Dictionary<string, List<int>>> ExtractCardValues(string[] cardGames)
        {
            if (cardGames.Length == 0)
            {
                throw new ArgumentNullException($"Input string is empty for {nameof(ExtractCardValues)}");
            }

            Dictionary<int, Dictionary<string, List<int>>> cardGamesDict = new();

            foreach (string game in cardGames)
            {
                int cardNumber = ExtractCardNumber(game);
                List<int> winNumbers = ExtractWinNumbers(game);
                List<int> actualNumbers = ExtractActualNumbers(game);

                // Store both winning and actual numbers for each card
                Dictionary<string, List<int>> cardValues = new()
                {
                    { "WinningNumbers", winNumbers },
                    { "ActualNumbers", actualNumbers}
                };

                cardGamesDict.Add(cardNumber, cardValues);
                day04Logger.Info($"Extracted values for Card {cardNumber}: {winNumbers.Count} winning numbers, {actualNumbers.Count} actual numbers");
            }

            return cardGamesDict;
        }

        public List<int> ExtractWinNumbers(string cardGame)
        {
            ValidateCardGameString(cardGame, nameof(ExtractWinNumbers));

            // Pattern matches numbers between ":" and "|"
            string winPattern = @":\s*((?:\d+\s*)+)\|";
            Match match = Regex.Match(cardGame, winPattern);

            if (!match.Success)
            {
                throw new FormatException($"Error extracting winNumbers for {cardGame}");
            }

            string winNumberString = match.Groups[1].Value;
            List<int> winNumbers = winNumberString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            day04Logger.Info($"Extracted {winNumbers.Count} winning numbers from Card {ExtractCardNumber(cardGame)}");
            return winNumbers;
        }

        public List<int> ExtractActualNumbers(string cardGame)
        {
            ValidateCardGameString(cardGame, nameof(ExtractActualNumbers));

            // Pattern matches numbers after "|"
            string actualPattern = @"\|\s*((?:\d+\s*)+)$";
            Match match = Regex.Match(cardGame, actualPattern);

            if (!match.Success)
            {
                throw new FormatException($"Error extracting actualNumbers for {cardGame}");
            }

            string actualNumberString = match.Groups[1].Value;
            List<int> actualNumbers = actualNumberString
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            day04Logger.Info($"Extracted {actualNumbers.Count} actual numbers from Card {ExtractCardNumber(cardGame)}");
            return actualNumbers;
        }

        public int ExtractCardNumber(string cardGame)
        {
            ValidateCardGameString(cardGame, nameof(ExtractCardNumber));

            string cardRegex = @"Card\s+(\d+)";
            Match match = Regex.Match(cardGame, cardRegex);

            if (!match.Success)
            {
                throw new FormatException($"Could not find card number in: {cardGame}");
            }

            return int.Parse(match.Groups[1].Value);
        }

        public List<int> ExtractAllCardNumbers(string[] cardGames)
        {
            if (cardGames == null || cardGames.Length < 1)
            {
                throw new ArgumentNullException($"cardGame string is null or empty for {nameof(ExtractAllCardNumbers)}");
            }

            List<int> results = cardGames.Select(ExtractCardNumber).ToList();
            day04Logger.Info($"Extracted {results.Count} card numbers");
            return results;
        }

        // Helper Methods
        private string[] SplitStringByNewLine(string inputString)
        {
            return inputString.Split(
                new[] { Environment.NewLine, "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
        }

        private void ValidateCardGameString(string cardGame, string methodName)
        {
            if (string.IsNullOrEmpty(cardGame))
            {
                throw new ArgumentNullException($"cardGame string is null or empty for {methodName}");
            }
        }

        // Word doc was used when working from other pc. 
        public string ExtractInputFromWordDoc(string filepath = "C:\\Users\\mchap\\source\\repos\\AOCPractice\\InputWordDocs\\Day4.docx")
        {
            StringBuilder day04String = new();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filepath, false))
            {
                var mainPart = doc.MainDocumentPart;
                var body = mainPart.Document.Body;
                foreach (var paragraph in body.Elements<Paragraph>())
                {
                    day04String.AppendLine(paragraph.InnerText);
                }
            }
            return day04String.ToString();
        }
    }
}