using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AdventOfCode._2023.Day04.Day04PartTwo;

namespace AdventOfCode._2023.Day04.Day04Answers
{
    public class Day04Answer
    {
        public Day04PuzzleInput PuzzleInput { get; set; }
        public Day04Logger day04Logger { get; set; }

        public Day04Part2 day04Part2 { get; set; }

        // List of winning numbers & list of actual numbers.
        // One card worth 1 point.
        // Every subsequent match doubles the score from this point. 


        // Can create classes for winning numbers & match numbers, used this approach last time so will try something different for this one.
        // If we can find a way to differentiate the winning numbers from actual numbers with a bool value etc we can then look for duplicates in the full list of numbers.
        // Simplest approach is probably to create a dict of winning numbers and check each number against this.


        public Day04Answer(Day04PuzzleInput puzzleInput, Day04Logger d4Logger, Day04Part2 part2)
        {
            PuzzleInput = puzzleInput;
            day04Logger = d4Logger;
            day04Part2 = part2;
        }



        public void Answer()
        {

            List<int> cardNumbers = new List<int> { 1, 2, 3, 4 };
 
            string[] testStrings = PuzzleInput.GenerateSplitInputString();

            Dictionary<int, Dictionary<string, List<int>>> games = PuzzleInput.ExtractCardValues(testStrings);

        }



        public int GetTotalMatches(string[] cardGameString)
        {
            int gameTotal = 0;


            foreach(var game in PuzzleInput.ExtractCardValues(cardGameString))
            {


                var gameNumber = game.Key;
                var allNumbers = game.Value;

                List<int> winningNumbers = allNumbers["WinningNumbers"];
                List<int> actualNumbers = allNumbers["ActualNumbers"];

                List<int> matchingNumbers = winningNumbers.Intersect(actualNumbers).ToList();

                int matchCount = matchingNumbers.Count;

                int cardTotal = calculateCardScore(matchCount);

                gameTotal += cardTotal;
            }
            return gameTotal;
        }


        public int calculateCardScore(int gameTotal)
        {
            int gamescore = 0;

            if(gameTotal == 0)
            {
                return 0;
            }
            if(gameTotal == 1) 
            {
                return 1;
            }

            else
            {
                gamescore = 1;
                gameTotal--;
            }

            while(gameTotal > 0) 
            {
                gamescore = gamescore * 2;
                gameTotal--;
            }

            return gamescore;
 
        }






        
    }
}
