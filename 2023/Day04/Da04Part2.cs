using AdventOfCode._2023.Day04.Day04Inputs;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using AdventOfCode._2023.Day04Loggers;

namespace AdventOfCode._2023.Day04.Day04PartTwo
{
    public class Day04Part2
    {
        public Day04InputFormatter day04InputFormatter { get; set; }

        public Day04Input day04Input { get; set; }

        private readonly Day04Logger _day04Logger;

        public Dictionary<int,int> CardGameMatchesDict { get; set; }

        public Dictionary<int, int> numberOfCardsDict { get; set; } 

        public int HighestCardGame { get; private set; }

        public Day04Part2(Day04InputFormatter inputFormat, Day04Input d04Input, Day04Logger d04Logger)
        {
            day04InputFormatter = inputFormat;
            CardGameMatchesDict = new Dictionary<int, int>();
            numberOfCardsDict = new Dictionary<int, int>();
            day04Input = d04Input;
            _day04Logger = d04Logger;

        }


        public int CalculateTotalScratchcards()
        {
            string[] testString = day04InputFormatter.GenerateTestString();
            string[] fullString = day04InputFormatter.GenerateFullSplitInput();

            InitialPopulateMatchCountDict(fullString);
            SetMaxCardGameNumber();





            return CardGameMatchesDict.Values.Sum();
        }

        public void UpdateAllCardGameMatchesDict(string[] cardGames)
        {
            foreach (string cardGame in cardGames)
            {

                int cardGameInt = day04InputFormatter.ExtractCardNumber(cardGame);
                int matchCount = GetMatchCountFromCardGame(cardGame);
                int cardCount = numberOfCardsDict[cardGameInt];

                UpdateNumberOfCardsDict(cardGameInt, matchCount, cardCount);




                _day04Logger.Info($" Game{cardGameInt} Count{matchCount}. {nameof(UpdateAllCardGameMatchesDict)}");


               





            }
        }

        public void UpdateNumberOfCardsDict(int cardGame, int matchCount, int cardCount)
        {
            int actualRange = GetCardGameRange(cardGame, matchCount);

            for (int i = cardGame + 1; i <= cardGame + actualRange; i++)
            {
                numberOfCardsDict[i] += cardCount;
                Console.WriteLine($"CardGame {i} updated to {numberOfCardsDict[i]}.");
            }

        }




        public void UpdateOneCardGameMatchesDict(int cardGame, int matchCount, int cardCount)
        {
            int actualRange = GetCardGameRange(cardGame, matchCount);

          

            for (int i = cardGame + 1; i <= cardGame + actualRange; i++)
            {
                CardGameMatchesDict[i]++;
                Console.WriteLine($"CardGame {i} updated to {CardGameMatchesDict[i]}.");
            }
        }






        public void LogTestCount(string[] cardGameString)
        {
            foreach (var game in cardGameString)
            {
                int cardNumber = day04InputFormatter.ExtractCardNumber(game);
                int matchCount = GetMatchCountFromCardGame(game);
                Console.WriteLine($"Card Game: {cardNumber}, match count: {matchCount} for {nameof(LogTestCount)}.");
            }
        }


        public int GetMatchCountFromCardGame(string cardGame)
        {
            List<int> winNumbers = day04InputFormatter.ExtractWinNumbers(cardGame);
            List<int> actualNumber = day04InputFormatter.ExtractActualNumbers(cardGame);
            return winNumbers.Intersect(actualNumber).Count();
        }



        

        public void InitialPopulateMatchCountDict(string[] cardGames)
        {
            foreach(int number in day04InputFormatter.ExtractAllCardNumbers(cardGames))
            {
                int count = 0;
                CardGameMatchesDict.Add(number, count);
            }

        }



        public void SetMaxCardGameNumber()
        {
            if(!CardGameMatchesDict.Keys.Any())
            {
                throw new InvalidOperationException($"CardGameMatch Dictionary not created.");
            }
            HighestCardGame = CardGameMatchesDict.Keys.Max();
            _day04Logger.Info($" Highest card game set to {HighestCardGame}");
        }




        // Only called within UpdateOneCardGameMatchesDict
        public int GetCardGameRange(int cardGame, int count)
        {
            if (cardGame + count <= HighestCardGame)
            {
                return count;
            }
            return HighestCardGame - cardGame;

        }



  



 




    }
}