using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2023.Day04.Day04PartTwo
{
    public class Day04Part2
    {
        // Iterate through and count up all the matches for each card.
        // First need to Get number of matches for each card. 



        private readonly Day04Logger _day04Logger;

        public Day04PuzzleInput PuzzleInput { get; set; }

        public Day04Part2(Day04Logger day04Logger, Day04PuzzleInput puzzleInput)
        {
            _day04Logger = day04Logger;
            PuzzleInput = puzzleInput;
        }



        public void LogTestCount(string cardGameString)
        {

        }



        public Dictionary<int, int> CreateMatchCountDict(string cardGameString)
        {
            int cardGame = PuzzleInput.ExtractCardNumber(cardGameString);
            int count = GetMatchCountFromCardGame(cardGameString);

            return new Dictionary<int, int>
            {
                {cardGame, count}
            };
  

        }



        public int GetMatchCountFromCardGame(string cardGame)
        {
            int matchCount = 0;

            List<int> winNumbers = PuzzleInput.ExtractWinNumbers(cardGame);
            List<int> actualNumber = PuzzleInput.ExtractActualNumbers(cardGame);

            return winNumbers.Intersect(actualNumber).Count();

        }







    }
}
