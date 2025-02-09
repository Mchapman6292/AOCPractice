using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using AdventOfCode._2023.Day05.SeedRangeStructures;
using DocumentFormat.OpenXml.Drawing;
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



        public void Part2UpdateOneseedRange(SeedRangeStruct originalSeedValue)
        {
            originalSeedValue.End = originalSeedValue.Start + originalSeedValue.End;
        }

        public List<SeedRangeStruct> UpdateAllPart2SeedRanges(List<SeedRangeStruct> originalSeedValues)
        {
            List<SeedRangeStruct> updatedRanges = new List<SeedRangeStruct>();

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


        public void DetermineMapSides(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd, out bool SeedStartInMapRange, out bool SeedEndInMapRange)
        {
            BigInteger actualStart = BigInteger.Min(sourceStart, sourceEnd);
            BigInteger actualEnd = BigInteger.Max(sourceStart, sourceEnd);

            SeedStartInMapRange = (startSeed >= actualStart && startSeed <= actualEnd);
            SeedEndInMapRange = (endSeed >= actualStart && endSeed <= actualEnd);
        }




        public void TESTPart2CalculateNewSeedRange(BigInteger offSet ,BigInteger startSeed, BigInteger endSeed, BigInteger mapStart, BigInteger mapEnd,  bool needsLeftMapped,  bool needsRightMapped, out BigInteger newStart, out BigInteger newEnd)
        {
            // If seed start is within the map range(mapStart - mapEnd) return true & vice versa for rightMapped

            BigInteger start = BigInteger.Min(startSeed, mapStart);
            BigInteger end = BigInteger.Max(endSeed, mapEnd);


            if (needsLeftMapped && needsRightMapped)
            {
                newStart = start + offSet;
                newEnd = end + offSet;
            }
            
            if (needsLeftMapped && !needsRightMapped)
            {
                newStart = start + offSet;
                newEnd = end;
            }

            if (needsRightMapped && !needsLeftMapped)
            {
                newStart = startSeed;
                newEnd = end + offSet;
            }
            else
            {
                newStart = startSeed;
                newEnd = endSeed;
            }           
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

        public List<SeedRangeStruct> SortSeedRanges(List<SeedRangeStruct> seedRanges) 
        {
            List<SeedRangeStruct> allSortedSeeds = new List<SeedRangeStruct>();

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
                    SeedRangeStruct newRange = new SeedRangeStruct()
                    {
                        Start = currentStartValue,
                        End = currentEndValue,
                    };
                    allSortedSeeds.Add(newRange);
                    _logger.Debug($"New Range created for Start{ newRange.Start}, End: {newRange.End}");
                }
            }
            return allSortedSeeds;
        }


        /*
        public List<SeedRangeStruct> TestCreateNewSeedRanges(BaseSeedStructure originalSeed, BigInteger mapStart, BigInteger mapEnd, BigInteger range, bool SeedStartInMapRange, bool SeedEndInMapRange)
        {
            BigInteger offSet = mapStart - mapEnd;

            if(SeedStartInMapRange)
            {
                SeedRangeStruct leftOverRanges = new SeedRangeStruct()
                {
                    Start = mapEnd,
                    End = originalSeed.End
                };
            }

            

        }
        */

















    }
}
