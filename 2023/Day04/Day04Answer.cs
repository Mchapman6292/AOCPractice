using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AdventOfCode._2023.Day04
{
    public class Day04Answer
    {
        // List of winning numbers & list of actual numbers.
        // One card worth 1 point.
        // Every subsequent match doubles the score from this point. 


        // Can create classes for winning numbers & match numbers, used this approach last time so will try something different for this one.
        // If we can find a way to differentiate the winning numbers from actual numbers with a bool value etc we can then look for duplicates in the full list of numbers.
        // Simplest approach is probably to create a dict of winning numbers and check each number against this.


        public string ExtractInputFromWordDoc(string filepath)
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
            string[] cardGames = inputString.Split('\n');
            return cardGames;
        }


        public Dictionary<string, Dictionary<string, List<int>>> ExtractCardValues(string[] cardGames)
        {
            Dictionary<string, Dictionary<string, List<int>>> cardGamesDict = new Dictionary<string, Dictionary<string, List<int>>>();


            if (cardGames.Length == 0)
            {
                throw new ArgumentNullException($" Input string is empty for {nameof(ExtractCardValues)}.");
            }

            foreach (string game in cardGames)
            {
                int winNumbersIndex = game.IndexOf(':');
            }
        }


        public string ExtractWinNumbers(string cardGame)
        {
            if (cardGame == null || cardGame == string.Empty)
            {
                throw new ArgumentNullException($" cardGame string is null or empty for {nameof(ExtractWinNumbers)}");
            }

            int startIndex = cardGame.IndexOf(":") + 1;
            int endIndex = cardGame.IndexOf("|");


            string winNumbersSubString = cardGame.Substring(startIndex, endIndex - startIndex);

            int[] winNumbers = winNumbersSubString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(int.Parse)
                                                    .ToArray();

            


        }





        
    }
}
