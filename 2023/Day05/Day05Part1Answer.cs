using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;
using AdventOfCode._2023.Day05.SeedRangeStructures;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Numerics;
using System.Text;


namespace AdventOfCode._2023.Day05.DayFiveAnswer
{

    /* 94, 14, 55, 13 
     *  - Initial values to transform 
     *
     * - 50 = mapStart range start, 
     * - 98 = mapEnd range start
     * - 2 = range
     * 
     *                  FORMULA
     *  - First check if number falls between the mapStart and mapEnd values
     *  - We need to calculate the difference in the mapStart and mapEnd range
     *  - Then apply for the count(range) of numbers that are between 50-98 to the mapEnd range.
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
        private Day05Input _day05Input { get; set; }

        private readonly Day05Logger _day05Logger;

        private readonly SeedService _seedService;

        public List<Seed> allSeedsList { get; set; }

        public List<BaseSeedStruct> Part2QuestionTestSeeds { get; set; }

        public List<(BigInteger Start, BigInteger Length)> seedRanges { get; set; }

        public List<BaseSeedStruct> testBaseRanges { get; set; }




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


        public void InitializePart2QuestionTestSeeds()
        {
            List<BaseSeedStruct> seeds = new List<BaseSeedStruct>
            {
                new BaseSeedStruct(79,14),
                new BaseSeedStruct (55, 13)
            };
            Part2QuestionTestSeeds = seeds;
        }


        public List<BaseSeedStruct> ReturnPart2QuestionTestSeeds()
        {
            return Part2QuestionTestSeeds;  
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
            testBaseRanges = new List<BaseSeedStruct>
            {
                new BaseSeedStruct(79, 14),
                new BaseSeedStruct(55, 13),
                new BaseSeedStruct(82, 3),
                new BaseSeedStruct(46, 10),
                new BaseSeedStruct(35, 8),

                new BaseSeedStruct(50, 20),     // Range fully inside a mapping
                new BaseSeedStruct(10, 20),     // Range completely before mapping
                new BaseSeedStruct(200, 20),    // Range completely after mapping
                new BaseSeedStruct(40, 20),     // Range overlapping start of mapping
                new BaseSeedStruct(65, 20),     // Range overlapping end of mapping
                new BaseSeedStruct(150, 1),     // Single point test
                new BaseSeedStruct(45, 75),     // Range encompassing multiple mappings
                new BaseSeedStruct(290, 30),    // Range spanning consecutive mappings
                new BaseSeedStruct(580, 50)     // Range spanning multiple non-consecutive mappings
            };
        }



        public List<BaseSeedStruct> GetTestBaseRanges()
        {
            return testBaseRanges;
        }


        public List<Seed> GetSeedList()
        {
            return allSeedsList;
        }


        public BigInteger CalculateForOneMap(MapType map, BigInteger startValue, bool test)
        {
            var mapString = test ? _day05Input.QuestionTestMaps[map] : _day05Input.AllMaps[map];
            List<string> splitMapValues = _day05Input.SplitMapValuesByLine(mapString);
            BigInteger currentValue = startValue;

            foreach (string splitValue in splitMapValues)
            {
                BigInteger destinationStart, sourceStart, range;
                _day05Input.ParseMapStringToBigInt(splitValue, out destinationStart, out sourceStart, out range);
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





        /*
        public List<SeedRangeStruct> CalculatePart2RangeForOneMap(MapType map, List<BaseSeedStruct> originalSeedRanges, bool useTestMapStrings)
        {
            List<SeedRangeStruct> allMappedSeedRanges = new List<SeedRangeStruct>();
            BigInteger currentMinimum = _day05Input.GetMaxValueFromMaps(_day05Input.AllMaps);



            var mapString = useTestMapStrings ? _day05Input.QuestionTestMaps[map] : _day05Input.AllMaps[map];

            foreach (var splitValue in _day05Input.SplitMapValuesByLine(mapString))
            {
                List<SeedRangeStruct> unSortedRanges = new List<SeedRangeStruct>();

                BigInteger mapStart;
                BigInteger mapEnd;
                BigInteger range;

                _day05Input.ParseMapStringToMapValueStruct(splitValue, out mapStart, out mapEnd, out range);

                BigInteger sourceStartPlusRange = _seedService.CalculateMaxSourceRange(mapEnd, range);

                foreach (BaseSeedStruct seedRange in originalSeedRanges)
                {
                    if (_seedService.Part2IsWithinRange(seedRange.Start, seedRange.End, mapEnd, sourceStartPlusRange))
                    {
                        BigInteger offSet = mapStart - mapEnd;

                        bool SeedStartInMapRange;
                        bool SeedEndInMapRange;
                        BigInteger newStart = 0;
                        BigInteger newEnd = 0;

                        _seedService.DetermineMapSides(seedRange.Start, seedRange.End, mapEnd, sourceStartPlusRange, out SeedStartInMapRange, out SeedEndInMapRange);

                        unSortedRanges.AddRange(_seedService.TestCreateNewSeedRangesWithLogging(seedRange, mapStart, mapEnd, offSet, SeedStartInMapRange, SeedEndInMapRange));

                    }
                }
                var sortedRanges = _seedService.SortSeedRanges(unSortedRanges);
                var lowestValue = sortedRanges[0].End;
                if (lowestValue < currentMinimum)
                {
                    currentMinimum = lowestValue;
                }
            }
            return allMappedSeedRanges;
        }

        */

        // Does this method get called on the SeedRange in the originalList as well as the updated list?
        //

        public BigInteger CalculateAllMapRanges(List<BaseSeedStruct> originalSeedRanges, SortedDictionary<MapType, string> maps)
        {
            List<BigInteger> lowestTransformationValues = new List<BigInteger>();

            /* Convert initial Values into seedRangeStruct, The range is already calculated in BaseSeedStruct creation but it is less confusing to just take the two values 
             * BaseSeedStruct Start & End = startSeed number and the already calculated Start + seedcount value*/
           
            List<SeedRangeStruct> transformedOriginalRanges = _seedService.ConvertBaseSeedToSeedStruct(originalSeedRanges);

            //We create the list that holds the previous lines ranges, we then set this at the end of each line to the previous lines total ranges
            List<SeedRangeStruct> currentLineSeedRanges = new List<SeedRangeStruct>();

            // This iterates over all of the values in each MapString, we still need to take the map string and parse the values from each line
            foreach (string fullMapString in maps.Values)
            {
                var singleMapStringList = _day05Input.SplitMapValuesByLine(fullMapString).ToList();

                foreach(string sinlgeMapString in singleMapStringList)
                {
                    // Create a struct which holds all the values needed from the maps, the total range and offset is calculated by the struct upon creation.
                    MapValueStruct currentMapValues = _day05Input.ParseMapStringToMapValueStruct(sinlgeMapString);

                    foreach(SeedRangeStruct seedRange in currentLineSeedRanges)
                    {
                        bool seedStartInMapRange;
                        bool seedEndInMapRange;

                        _seedService.Part2DetermineMapSides(seedRange, currentMapValues, out seedStartInMapRange, out seedEndInMapRange);

                    }
                }
            }



        }










    }
}
    




















