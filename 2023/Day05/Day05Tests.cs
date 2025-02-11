using AdventOfCode._2023.Day05.DayFiveAnswer;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.SeedRangeStructures;
using AdventOfCode._2023.Day05.MapTypes;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Numerics;

namespace AdventOfCode._2023.Day05.Day05Tests
{
    public class Day05Tests
    {
        private readonly Day05Logger _logger;
        private readonly SeedService _seedService;
        private readonly Day05Part1Answer _answer;
        private Day05Input _day05Input;


        public Day05Tests(LogManager logManager, Day05Input input, SeedService seedService)
        {
            _logger = logManager.GetLogger();
            _day05Input = input;
            _seedService = seedService;
        }



        public void TestDetermineMapSides(List<BaseSeedStruct> seedRanges, SortedDictionary<MapType, string> mapStrings, MapType mapType)
        {
            List<string> splitMapValues = _day05Input.SplitMapValuesByLine(mapStrings[mapType]);
            foreach (var splitMapValue in splitMapValues)
            {
                BigInteger destinationStart;
                BigInteger sourceStart;
                BigInteger range;
                _day05Input.ParseMapStringToBigInt(splitMapValue, out destinationStart, out sourceStart, out range);

                BigInteger sourceEnd = sourceStart + range - 1;

                foreach (var seedRange in seedRanges)
                {
                    BigInteger seedStart = seedRange.Start;
                    BigInteger seedEnd = seedRange.Start + seedRange.SeedCount;

                    bool seedStartInRange;
                    bool seedEndInRange;

                    _seedService.DetermineMapSides(seedStart, seedEnd, sourceStart, sourceEnd, out seedStartInRange, out seedEndInRange);

                    _logger.Debug($"Map {mapType} - Source Range [{sourceStart}-{sourceEnd}]");
                    _logger.Debug($"Seed Range [{seedStart}-{seedEnd}]");
                    _logger.Debug($"Start In Range: {seedStartInRange}");
                    _logger.Debug($"End In Range: {seedEndInRange}");
                    _logger.Debug("-------------------");
                }
            }
        }


    }
}
