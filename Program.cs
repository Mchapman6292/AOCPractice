using AdventOfCode._2023.Day04.Day04Inputs;
using AdventOfCode._2023.Day05.Day05Tests;
using AdventOfCode._2023.Day05.DayFiveAnswer;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;
using AdventOfCode._2023.Day05.SeedRangeStructures;
using System.Numerics;


namespace AdventOfCode.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Day05Logger logger = new Day05Logger();
            LogManager logManager = new LogManager(logger);
            SeedService seedService = new SeedService(logManager);
            Day05Input day05Input = new Day05Input(logManager);
            Day05Part1Answer day05Ans = new Day05Part1Answer(day05Input, logManager, seedService);
            Day05Tests day05Test = new Day05Tests(logManager, day05Input, seedService);


            logger.Info("TEST MESSAGE - If you see this, logger is working");

            day05Ans.InitializeTestBaseSeedStructures();
            day05Ans.InitializePart2QuestionTestSeeds();
            day05Ans.InitializeSeedRanges();



            SortedDictionary<MapType, string> QuestionTestMaps = new SortedDictionary<MapType, string>
            {
            [MapType.SeedToSoil] =
                "50 98 2\n" +
                "52 50 48",
            };



            List < BaseSeedStruct> questionTestSeeds = day05Ans.ReturnPart2QuestionTestSeeds();
            SortedDictionary<MapType, string> testMaps = day05Input.GetQuestionTestMaps();


            BigInteger result = day05Ans.CalculateAllMapRanges(questionTestSeeds, QuestionTestMaps);

            Console.WriteLine($"Result: {result}.");
            





            Console.ReadKey();





        }

      
    }
}