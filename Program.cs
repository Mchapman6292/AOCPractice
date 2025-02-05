using AdventOfCode._2023.Day05.DayFiveAnswer;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;
using System.Numerics;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using AdventOfCode._2023.Day05.MapTypes;
using DocumentFormat.OpenXml.Bibliography;
using AdventOfCode._2023.Day05.Day05Tests;


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
            Day05Answer day05Ans = new Day05Answer(day05Input, logManager, seedService);
            Day05Tests day05Test = new Day05Tests();

            day05Ans.InitializeSeedList();
            day05Ans.InitializeTestSeedList();

            List<Seed> seedList = day05Ans.GetSeedList();
            List<Seed> testSeedList = day05Ans.GetTestSeedList();

            SortedDictionary<MapType, string> AllMaps = day05Input.GetAllMaps();
            SortedDictionary<MapType, string> testMaps = day05Input.GetTestMaps();


            Seed testSeed = seedList.First();

            Console.Clear();

            Console.WriteLine($"-------------------------\n -------------------------\n ----------------");

            /*
            Console.WriteLine($"Seed{testSeed.StartValue}.");

            day05Ans.CalculateForAllMaps(testMaps, testSeed);

            BigInteger? answer = testSeed.MapValues[MapType.HumidityToLocation];

            Console.WriteLine($"Answer: {answer}.");
            */

            bool test = false;
            BigInteger? results =day05Ans.CalculateForAllSeeds(AllMaps, seedList, test);

            if(results.HasValue) 
            {
                Console.WriteLine($"Final result: {results.Value.ToString()}");
            }
   





        }

      
    }
}