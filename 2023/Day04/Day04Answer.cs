using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AdventOfCode._2023.Day04.Day04Answers
{
    public class Day04Answer
    {
        public Day04PuzzleInput PuzzleInput { get; set; }
        public Day04Logger day4Logger { get; set; }

        // List of winning numbers & list of actual numbers.
        // One card worth 1 point.
        // Every subsequent match doubles the score from this point. 


        // Can create classes for winning numbers & match numbers, used this approach last time so will try something different for this one.
        // If we can find a way to differentiate the winning numbers from actual numbers with a bool value etc we can then look for duplicates in the full list of numbers.
        // Simplest approach is probably to create a dict of winning numbers and check each number against this.


        public Day04Answer(Day04PuzzleInput puzzleInput, Day04Logger d4Logger)
        {
            PuzzleInput = puzzleInput;
            day4Logger = d4Logger;
        }



        public void Answer()
        {

            List<int> cardNumbers = new List<int> { 1, 2, 3, 4 };
 
            string[] testStrings = PuzzleInput.GenerateTestString();

            Dictionary<int, Dictionary<string, List<int>>> games = PuzzleInput.ExtractCardValues(testStrings);

            day4Logger.LogScratchCardGames(games, cardNumbers);



        }






        
    }
}
