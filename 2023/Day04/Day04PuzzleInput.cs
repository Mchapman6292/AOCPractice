using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AOCPractice.BaseLoggers;

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
        public string[]  GenerateTestString()
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
            return inputString.Split('\n');
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

            int startIndex = cardGame.IndexOf(":") + 1;
            int endIndex = cardGame.IndexOf("|");


            string winNumbersSubString = cardGame.Substring(startIndex, endIndex - startIndex);

            return winNumbersSubString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(int.Parse)
                                                    .ToList();
        }

        public List<int> ExtractActualNumbers(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractActualNumbers)}");

            }

            Console.WriteLine

            int startIndex = cardGame.IndexOf('|') + 1;
            int endIndex = cardGame.Length - 1;

            string actualNumbersSubString = cardGame.Substring(startIndex, endIndex - startIndex);

            return actualNumbersSubString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                        .Select(int.Parse)
                                                       .ToList();
        }

        public int ExtractCardNumber(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractActualNumbers)}");
            }

            int startIndex = 0;
            int endIndex = cardGame.IndexOf(':') - 1;

            string cardSubstring = cardGame.Substring(startIndex, endIndex - 1);

            string cardGameString = cardSubstring.Replace("Card", " ").Trim();

            Console.Write($" Substring for {cardGame}: {cardSubstring}, trimmed string: {cardGameString}.");


            return int.Parse(cardGameString);
        }

    }
}
