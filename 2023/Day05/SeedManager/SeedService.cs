using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using AdventOfCode._2023.Day05.SeedRangeStructures;
using System.Numerics;

namespace AdventOfCode._2023.Day05.SeedManager.SeedServices
{
    public class SeedService
    {
        private readonly Day05Logger _logger;



        public SeedService(LogManager logManager)
        {
            _logger = logManager.GetLogger();
        }



        // Returns the new highest source range only. 
        public BigInteger CalculateMaxSourceRange(BigInteger sourceStart, BigInteger Range)
        {
            return sourceStart + Range;
        }


        public BigInteger Part2CalculateSeedRange(BigInteger seedStart , BigInteger seedEnd) 
        {
            return seedStart + seedEnd;
        }



        public void Part2UpdateOneseedRange(SeedRangeStructure originalSeedValue)
        {
            originalSeedValue.End = originalSeedValue.Start + originalSeedValue.End;
        }

        public List<SeedRangeStructure> UpdateAllPart2SeedRanges(List<SeedRangeStructure> originalSeedValues)
        {
            List<SeedRangeStructure> updatedRanges = new List<SeedRangeStructure>();

            foreach(var seeed in originalSeedValues)
            {
                Part2UpdateOneseedRange(seeed);
                updatedRanges.Add(seeed);
            }
            return updatedRanges;
        }
 






        public bool isWithinRange(BigInteger currentValue, BigInteger sourceStart, BigInteger sourceRange)
        {
            return(currentValue >= sourceStart && currentValue <= sourceRange);
        }

        public bool Part2IsWithinRange(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd)
        {
            return startSeed <= sourceEnd && endSeed >= sourceStart;
        }





        public void TESTPart2CalculateNewSeedRange(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd, out bool needsLeftMapped, out bool needsRightMapped)
        {
            // If seed start is within the map range(sourceStart - sourceEnd) return true & vice versa for rightMapped

            BigInteger start = BigInteger.Min(startSeed, sourceStart);
            BigInteger end = BigInteger.Max(endSeed, sourceEnd);

            needsLeftMapped = (startSeed <= sourceEnd);
            needsRightMapped = (endSeed >= sourceStart);

            

        }






        public BigInteger CaclulateOffSet(BigInteger destinationStart, BigInteger sourceStart)
        {
            return destinationStart - sourceStart;
        }


        public void UpdateTotalValueForMapType(Seed currentSeed,MapType mapType, BigInteger endValue)
        {
            currentSeed.MapValues[mapType] = endValue;
        }


        public void UpdateCurrentValue(Seed currentSeed, BigInteger currentValue)
        {
            currentSeed.CurrentValue = currentValue;
        }


        // 

        public List<SeedRangeStructure> SortSeedRanges(List<SeedRangeStructure> seedRanges) 
        {
            List<SeedRangeStructure> allSortedSeeds = new List<SeedRangeStructure>();

            List<BigInteger> startSorted = seedRanges
                    .Select(r => r.Start)
                    .OrderBy(s => s)
                    .ToList();

            List<BigInteger> endSorted = seedRanges
                    .Select(r => r.End)
                    .OrderBy(e => e)
                    .ToList();


            BigInteger lowestValue = startSorted[0];

            int sortedStartIndex = 0;
            int sortedEndIndex = 0;
            int startCount = 0;
            int endCount = 0;


            while (sortedStartIndex < startSorted.Count && sortedEndIndex < endSorted.Count)
            {
                BigInteger currentStartValue = startSorted[sortedStartIndex];
                BigInteger currentEndValue = endSorted[sortedEndIndex];
               
                BigInteger next = BigInteger.Min(startSorted[sortedStartIndex + 1], endSorted[sortedEndIndex]);

                if(next == startSorted[sortedStartIndex + 1])
                {
                    startCount++;
                    sortedStartIndex++;
                }
                else 
                {
                    endCount++;
                }

                if(startCount == endCount)
                {
                    SeedRangeStructure newRange = new SeedRangeStructure()
                    {
                        Start = currentStartValue,
                        End = currentEndValue,
                    };
                    allSortedSeeds.Add(newRange);
                }
            }
            return allSortedSeeds;
        }

















    }
}
