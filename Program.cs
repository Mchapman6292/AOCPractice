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

            day05Ans.InitializeTestBaseSeedStructures();

            List<BaseSeedStructure> baseSeeds = day05Ans.GetTestBaseRanges();
            SortedDictionary<MapType, string> testMaps = day05Input.GetTestMaps();


            day05Test.TestDetermineMapSides(baseSeeds, testMaps, MapType.EdgeCases);

           






            Console.ReadKey();





        }

      
    }
}