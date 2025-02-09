using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;
using System.Numerics;
using System.Text;
using AdventOfCode._2023.Day05.SeedRangeStructures;


namespace AdventOfCode._2023.Day05.DayFiveAnswer
{

    /* 94, 14, 55, 13 
     *  - Initial values to transform 
     *
     * - 50 = destinationStart range start, 
     * - 98 = sourceStart range start
     * - 2 = range
     * 
     *                  FORMULA
     *  - First check if number falls between the destinationStart and sourceStart values
     *  - We need to calculate the difference in the destinationStart and sourceStart range
     *  - Then apply for the count(range) of numbers that are between 50-98 to the sourceStart range.
     *  - We don't need to apply the calculations until all mappings are complete,we then perform the operation on the original seed. 
     *  
     *  
     *  We need to apply this to all values through all maps and then find the lowest output.
     *  We don't need to apply this to all of the numbers within the range just the highest/lower value
     *  Possibly only interested in whether the values in the range increased or decreased as we only need the end lowest value. 
     *  
     *  
     *  
     *  Source range starts at 98 and goes for 2 numbers (98, 99)
     *  Destination range starts at 50 and goes for 2 numbers (50, 51)
     *  
     *  
     *  Seed 4106085912
     *  1640984363 3136305987 77225710
     *  We calculate 3136305987  + 77225710 to find our range, if our number falls between the result we then calculate the difference  between 3136305987 - 1640984363 and then apply that to all values within our initially calculated range
     *  
     *  
     *  
     *  Part 2
     *  FIRST Value IS THE START, SECOND SEED IS THE RANGE ---- E.G  79 and contains 14 values: 79, 80, ..., 91, 92. 
     *  Calculate seed range(inclusive)
     *  
     *  
     *  Need an efficient way to find if any of the seeds are within the difference, binary search?
     *  
     *  We calculate the new range, and then compare this to the range of seeds, if the value of the new range is greater than the range of seeds then we need to define a new seed range. 
     *  if(newRange.Lowest <= seedrange.lowest && new Range.Highest >= seedrange.highest) then apply to all values
     *  Do any of the seeds exist in multiple seed ranges?
     *  Do we do each seed one at a time or do each calculation for all seed ranges and group them into larger groupings?





    */


    public class Day05Part1Answer
    {
        private  Day05Input _day05Input { get; set; }

        private readonly Day05Logger _day05Logger;

        private readonly SeedService _seedService;

        public List<Seed> allSeedsList { get; set; }

        public List<Seed> allTestSeedsList { get; set; }

        public List<(BigInteger Start, BigInteger Length)> seedRanges { get; set; }

        public List<BaseSeedStructure> testBaseRanges { get; set; }




        public Day05Part1Answer(Day05Input input, LogManager logManager, SeedService seedService)
        {
            _day05Input = input;
            _day05Logger = logManager.GetLogger();
            _seedService = seedService;

        }

        public void InitializeSeedList()
        {
            List<Seed> seeds = new List<Seed>
            {
                new Seed(4106085912),
                new Seed(135215567),
                new Seed(529248892),
                new Seed(159537194),
                new Seed(1281459911),
                new Seed(114322341),
                new Seed(1857095529),
                new Seed(814584370),
                new Seed(2999858074),
                new Seed(50388481),
                new Seed(3362084117),
                new Seed(37744902),
                new Seed(3471634344),
                new Seed(240133599),
                new Seed(3737494864),
                new Seed(346615684),
                new Seed(1585884643),
                new Seed(142273098),
                new Seed(917169654),
                new Seed(286257440)
            };
            allSeedsList = seeds;
        }


        public void InitializeTestSeedList()
        {
            List<Seed> seeds = new List<Seed>
            {
                new Seed(79),
                new Seed(14),
                new Seed(55),
                new Seed(13)
            };
            allTestSeedsList = seeds;
        }

        public void InitializeSeedRanges()
        {
            seedRanges = new List<(BigInteger Start, BigInteger Length)>
            {
                (4106085912, 135215567),
                (529248892, 159537194),
                (1281459911, 114322341),
                (1857095529, 814584370),
                (2999858074, 50388481),
                (3362084117, 37744902),
                (3471634344, 240133599),
                (3737494864, 346615684),
                (1585884643, 142273098),
                (917169654, 286257440)
            };
        }

        public void InitializeTestBaseSeedStructures()
        {
            testBaseRanges = new List<BaseSeedStructure>
            {
                new BaseSeedStructure(79, 14),
                new BaseSeedStructure(55, 13),
                new BaseSeedStructure(82, 3),
                new BaseSeedStructure(46, 10),
                new BaseSeedStructure(35, 8),

                new BaseSeedStructure(50, 20),     // Range fully inside a mapping
                new BaseSeedStructure(10, 20),     // Range completely before mapping
                new BaseSeedStructure(200, 20),    // Range completely after mapping
                new BaseSeedStructure(40, 20),     // Range overlapping start of mapping
                new BaseSeedStructure(65, 20),     // Range overlapping end of mapping
                new BaseSeedStructure(150, 1),     // Single point test
                new BaseSeedStructure(45, 75),     // Range encompassing multiple mappings
                new BaseSeedStructure(290, 30),    // Range spanning consecutive mappings
                new BaseSeedStructure(580, 50)     // Range spanning multiple non-consecutive mappings
            };
        }

        public List<BaseSeedStructure> GetTestBaseRanges()
        {
            return testBaseRanges;
        }

        public List<Seed> GetTestSeedList()
        {
            return allTestSeedsList;
        }

        public List<Seed> GetSeedList()
        {
            return allSeedsList;
        }


        public BigInteger CalculateForOneMap(MapType map, BigInteger startValue, bool test)
        {
            var mapString = test ? _day05Input.TestMaps[map] : _day05Input.AllMaps[map];
            List<string> splitMapValues = _day05Input.SplitMapValuesByLine(mapString);
            BigInteger currentValue = startValue;

            foreach (string splitValue in splitMapValues)
            {
                BigInteger destinationStart, sourceStart, range;
                _day05Input.ParseAlmanacNumbersFromLine(splitValue, out destinationStart, out sourceStart, out range);
                BigInteger topRangeValue = _seedService.CalculateMaxSourceRange(sourceStart, range);

                if (_seedService.isWithinRange(currentValue, sourceStart, topRangeValue))
                {
                    BigInteger oldValue = currentValue;
                    BigInteger offSet = _seedService.CaclulateOffSet(destinationStart, sourceStart);
                    currentValue += offSet;
                    Console.WriteLine($"Map {map}: Value changed from {oldValue} to {currentValue}");
                    break;
                }
            }
            return currentValue;
        }

        public void CalculateForAllMaps(SortedDictionary<MapType, string> maps, Seed seed, bool test)
        {
            BigInteger currentValue = seed.StartValue;
            foreach (MapType map in maps.Keys)
            {
                currentValue = CalculateForOneMap(map, currentValue, test);
                seed.MapValues[map] = currentValue;
            }
        }

        public BigInteger? CalculateForAllSeeds(SortedDictionary<MapType, string> maps, List<Seed> seeds, bool test)
        {
            foreach (Seed seed in seeds)
            {
                CalculateForAllMaps(maps, seed, test);
            }

            StringBuilder results = new StringBuilder();
            results.AppendLine("\nFinal Results:");
            foreach (Seed seed in seeds)
            {
                results.AppendLine($"Seed {seed.StartValue} -> Location {seed.MapValues.Values.Last()}");
            }
            BigInteger? lowestResult = seeds.Select(s => s.MapValues.Values.Last()).Min();
            results.AppendLine($"\nLowest Location: {lowestResult}");
            Console.WriteLine(results.ToString());

            return lowestResult;
        }






        public List<SeedRangeStruct>  CalculatePart2RangeForOneMap(MapType map, List<BaseSeedStructure> originalSeedRanges, bool useTestMapStrings)
        {
            List<SeedRangeStruct> allMappedSeedRanges = new List<SeedRangeStruct>();
            BigInteger currentMinimum = _day05Input.GetMaxValueFromMaps(_day05Input.AllMaps);


            List<SeedRangeStruct> unSortedRanges = new List<SeedRangeStruct>();


            var mapString = useTestMapStrings ? _day05Input.TestMaps[map] : _day05Input.AllMaps[map];

            foreach (var splitValue in _day05Input.SplitMapValuesByLine(mapString))
            {
              

                BigInteger destinationStart;
                BigInteger sourceStart;
                BigInteger range;

                _day05Input.ParseAlmanacNumbersFromLine(splitValue, out destinationStart, out sourceStart, out range);

                BigInteger sourceEnd = _seedService.CalculateMaxSourceRange(sourceStart, range);

                foreach (var seedRange in originalSeedRanges)
                {
                    if (_seedService.Part2IsWithinRange(seedRange.Start, seedRange.End, sourceStart, sourceEnd))
                    {
                        BigInteger offSet = destinationStart - sourceStart;

                        bool SeedStartInMapRange;
                        bool SeedEndInMapRange;
                        BigInteger newStart = 0;
                        BigInteger newEnd = 0;

                        _seedService.DetermineMapSides(seedRange.Start, seedRange.End, sourceStart, sourceEnd, out SeedStartInMapRange, out SeedEndInMapRange);

                        _seedService.TESTPart2CalculateNewSeedRange(offSet, seedRange.Start, seedRange.End, sourceStart, sourceEnd, SeedStartInMapRange, SeedEndInMapRange, out newStart, out newEnd);

                        // start/end calculations using Min/Max aren't being used

                        if (SeedStartInMapRange && SeedEndInMapRange)
                        {
                            SeedRangeStruct newRange = new SeedRangeStruct()
                            {
                                Start = seedRange.Start + offSet,
                                End = seedRange.End + offSet
                            };
                            unSortedRanges.Add(newRange);
                            break;
                        }

                        if (SeedStartInMapRange && !SeedEndInMapRange)
                        {
                            SeedRangeStruct newRange = new SeedRangeStruct()
                            {
                                Start = seedRange.Start + offSet,
                                End = sourceEnd
                            };
                            unSortedRanges.Add(newRange);
                            break;
                        }

                        if (SeedEndInMapRange && !SeedStartInMapRange)
                        {
                            SeedRangeStruct newRange = new SeedRangeStruct()
                            {
                                Start = sourceStart,
                                End = sourceEnd + offSet
                            };
                            unSortedRanges.Add(newRange);
                        }     
                    }
                    allMappedSeedRanges.AddRange(_seedService.SortSeedRanges(unSortedRanges));
                }
            }
            return allMappedSeedRanges;
        }






    }
}
    




















