using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AOCPractice.BaseLoggers;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day04.Day04PuzzleInputs
{
    public class Day04PuzzleInput
    {

        private readonly Day04Logger _day04Logger;




        public Day04PuzzleInput(Day04Logger day04Logger)
        {
            _day04Logger = day04Logger;
        }


    


        // Use first 4 card games as test.
        public string[]  GenerateSplitInputString()
        {
            string day4Input = ExtractInputFromWordDoc();
            string[] splitInput = SplitStringByNewLine(day4Input);

            return splitInput;
        }

        


        public string ExtractInputFromWordDoc(string filepath = "C:\\Users\\mchap\\source\\repos\\AOCPractice\\InputWordDocs\\Day4.docx")
        {
            StringBuilder day04String = new StringBuilder();
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

        public string[] SplitStringByNewLine(string inputString)
        {
            return inputString.Split(new[] { Environment.NewLine, "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }





        public Dictionary<int, Dictionary<string, List<int>>> ExtractCardValues(string[] cardGames)
        {
            Dictionary<int, Dictionary<string, List<int>>> cardGamesDict = new Dictionary<int, Dictionary<string, List<int>>>();


            if (cardGames.Length == 0)
            {
                throw new ArgumentNullException($" Input string is empty for {nameof(ExtractCardValues)}.");
            }

            foreach (string game in cardGames)
            {
                int cardNumber = ExtractCardNumber(game);
                List<int> winNumers = ExtractWinNumbers(game);
                List<int> actualNumbers = ExtractActualNumbers(game);

                Dictionary<string, List<int>> cardValues = new Dictionary<string, List<int>>
                {
                    { "WinningNumbers", winNumers },
                    { "ActualNumbers", actualNumbers}
                };

                cardGamesDict.Add(cardNumber, cardValues);
            }
            return cardGamesDict;
        }




        public List<int> ExtractWinNumbers(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractWinNumbers)}");
            }


            int gameNumber = ExtractCardNumber(cardGame);
            string winPattern = @":\s*((?:\d+\s*)+)\|";

            Match match = Regex.Match(cardGame, winPattern);

            if(!match.Success)
            {
                throw new FormatException($"Error extracting winNumbers for {cardGame}.");
            }

            string winNumberString = match.Groups[1].Value;

            List<int> winNumbers = winNumberString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

     
                                                 
            _day04Logger.Info($"Win numbers extracted for cardGame: {gameNumber}.");

            _day04Logger.LogList(winNumbers);

            return winNumbers;
        }

        public List<int> ExtractActualNumbers(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractActualNumbers)}");
            }

            int gameNumber = ExtractCardNumber(cardGame);
            string actualPattern = @"\|\s*((?:\d+\s*)+)$";

            Match match = Regex.Match(cardGame, actualPattern);

            if(!match.Success) 
            {
                throw new FormatException($"Error extracting actualNumbers for {cardGame}.");
            }

            string actualNumberString = match.Groups[1].Value;

            List<int> actualNumbers = actualNumberString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();


            _day04Logger.Info($"Actual numbers extracted for cardGame: {gameNumber}.");

            _day04Logger.LogList(actualNumbers);

            return actualNumbers;
        }

        public int ExtractCardNumber(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractActualNumbers)}");
            }

            string cardRegex = @"Card\s+(\d+)";

            Match match = Regex.Match(cardGame, cardRegex);

            if (!match.Success)
            {
                throw new FormatException($"Could not find card number in: {cardGame}");
            }

            return int.Parse(match.Groups[1].Value);
        }
    }
}

