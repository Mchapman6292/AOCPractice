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


        public BigInteger Part2CalculateSeedRange(BigInteger seedStart, BigInteger seedEnd)
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

            foreach (var seeed in originalSeedValues)
            {
                Part2UpdateOneseedRange(seeed);
                updatedRanges.Add(seeed);
            }
            return updatedRanges;
        }







        public bool isWithinRange(BigInteger currentValue, BigInteger sourceStart, BigInteger sourceRange)
        {
            return (currentValue >= sourceStart && currentValue <= sourceRange);
        }

        public bool Part2IsWithinRange(SeedRangeStruct seedRange, BigInteger sourceStart, BigInteger sourceEnd)
        {
            return seedRange.Start <= sourceEnd && seedRange.End >= sourceStart;
        }

  

        // We need to check if the seed ranges are between the Source Range + Range, which is MapValueStruct SourceRange & MapValueStruct EndMapValue
        public void Part2DetermineMapSides(SeedRangeStruct seedRange, MapValueStruct mapValue, out bool SeedStartInMapRange, out bool SeedEndInMapRange)
        {
            SeedStartInMapRange = (seedRange.Start <= mapValue.EndMapValue && seedRange.Start>= mapValue.SourceRange);
            SeedEndInMapRange = (seedRange.End >= mapValue.SourceRange && seedRange.End <= mapValue.EndMapValue);
        }



        // Calculated using source start + range eg. 12,14,6 = sourceStart = 14, sourceEnd = 20
        public void DetermineMapSides(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd, out bool SeedStartInMapRange, out bool SeedEndInMapRange)
        {
            SeedStartInMapRange = (startSeed <= sourceEnd && startSeed >= sourceStart);
            SeedEndInMapRange = (endSeed >= sourceStart && endSeed <= sourceEnd);
        }


        public void TestRangeCases()
        {

            _logger.Debug($"TestRangeCases() called from: {new System.Diagnostics.StackTrace()}");

            _logger.Debug("Testing range cases...");

            // Fully within range
            BigInteger mapStart = 50;
            BigInteger mapEnd = 98;
            BigInteger seedStart = 60;
            BigInteger seedEnd = 70;

            SeedRangeStruct seedRange = new SeedRangeStruct()
            {
                Start = 60,
                End = 70
            };

            bool isInRange = Part2IsWithinRange(seedRange, mapStart, mapEnd);
            bool startInRange, endInRange;
            DetermineMapSides(seedStart, seedEnd, mapStart, mapEnd, out startInRange, out endInRange);

            _logger.Debug($"Case 1 - Fully within range:");
            _logger.Debug($"Map range: [{mapStart}-{mapEnd}], Seed range: [{seedStart}-{seedEnd}]");
            _logger.Debug($"IsInRange: {isInRange}, StartInRange: {startInRange}, EndInRange: {endInRange}");

            // Overlapping start
            seedStart = 45;
            seedEnd = 55;

            isInRange = Part2IsWithinRange(seedRange, mapStart, mapEnd);
            DetermineMapSides(seedStart, seedEnd, mapStart, mapEnd, out startInRange, out endInRange);

            _logger.Debug($"\nCase 2 - Overlapping start:");
            _logger.Debug($"Map range: [{mapStart}-{mapEnd}], Seed range: [{seedStart}-{seedEnd}]");
            _logger.Debug($"IsInRange: {isInRange}, StartInRange: {startInRange}, EndInRange: {endInRange}");

            // Overlapping end
            seedStart = 95;
            seedEnd = 105;

            isInRange = Part2IsWithinRange(seedRange, mapStart, mapEnd);
            DetermineMapSides(seedStart, seedEnd, mapStart, mapEnd, out startInRange, out endInRange);

            _logger.Debug($"\nCase 3 - Overlapping end:");
            _logger.Debug($"Map range: [{mapStart}-{mapEnd}], Seed range: [{seedStart}-{seedEnd}]");
            _logger.Debug($"IsInRange: {isInRange}, StartInRange: {startInRange}, EndInRange: {endInRange}");

            // : Outside range
            seedStart = 20;
            seedEnd = 30;

            isInRange = Part2IsWithinRange(seedRange, mapStart, mapEnd);
            DetermineMapSides(seedStart, seedEnd, mapStart, mapEnd, out startInRange, out endInRange);

            _logger.Debug($"\nCase 4 - Outside range:");
            _logger.Debug($"Map range: [{mapStart}-{mapEnd}], Seed range: [{seedStart}-{seedEnd}]");
            _logger.Debug($"IsInRange: {isInRange}, StartInRange: {startInRange}, EndInRange: {endInRange}");
        }


        public void TESTPart2CalculateNewSeedRange(BigInteger offSet, BigInteger startSeed, BigInteger endSeed, BigInteger mapStart, BigInteger mapEnd, bool needsLeftMapped, bool needsRightMapped, out BigInteger newStart, out BigInteger newEnd)
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


        public void UpdateTotalValueForMapType(Seed currentSeed, MapType mapType, BigInteger endValue)
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

            if (!seedRanges.Any())
            {
                throw new ArgumentNullException($"seedRanges parameter is null.");
            }


            List<BigInteger> startSorted = seedRanges
                       .Select(r => new SeedRangeStruct { Start = r.Start, End = r.End })
                       .Select(r => r.Start)
                       .OrderBy(s => s)
                       .ToList();
            List<BigInteger> endSorted = seedRanges
                    .Select(r => new SeedRangeStruct { Start = r.Start, End = r.End })
                    .Select(r => r.End)
                    .OrderBy(e => e)
                    .ToList();


            BigInteger lowestValue = BigInteger.Min(startSorted[0], endSorted[0]);
            int sortedStartIndex = 0;
            int sortedEndIndex = 0;
            int startCount = 0;
            int endCount = 0;
            int loopCount = 0;
            int maxIterations = 10000;
            int lastPossibleIndex = startSorted.Count + endSorted.Count - 1;


            if (lowestValue == startSorted[0])
            {
                sortedStartIndex++;
            }
            if (lowestValue == endSorted[0])
            {
                sortedEndIndex++;
            }
            BigInteger initialStartValue = sortedStartIndex == 1 ? startSorted[0] : startSorted[sortedStartIndex];
            BigInteger initialEndValue = sortedEndIndex == 1 ? endSorted[0] : endSorted[sortedEndIndex];


            while (sortedStartIndex < startSorted.Count - 1 && sortedEndIndex < endSorted.Count - 1)
            {
                Console.WriteLine($"Starting loop for sortedStartIndex: {sortedEndIndex}, StartSorted: {startSorted.Count}, sortedEndIndex: {sortedEndIndex}, endSorted: {endSorted.Count}.");
                loopCount++;
                if (loopCount % 100 == 0)
                {
                    _logger.Debug($"While loop iteration {loopCount}: StartIndex={sortedStartIndex}, EndIndex={sortedEndIndex}");
                }

                if (loopCount > maxIterations)
                {
                    throw new ArgumentException("Potential infinite loop detected: Exceeded maximum iteration count!");
                    break;
                }

                BigInteger currentStartValue = startSorted[sortedStartIndex];
                BigInteger currentEndValue = endSorted[sortedEndIndex];
                _logger.Debug($"Current Values - Start: {currentStartValue}, End: {currentEndValue}");
                BigInteger next = BigInteger.Min(startSorted[sortedStartIndex + 1], endSorted[sortedEndIndex]);


                if (next == startSorted[sortedStartIndex + 1])
                {
                    sortedStartIndex++;
                    _logger.Debug($"Updated sortedStartIndex: {sortedStartIndex}");
                }
                else
                {
                    sortedEndIndex++;
                }
                if (startCount == endCount)
                {
                    SeedRangeStruct newRange = new SeedRangeStruct()
                    {
                        Start = currentStartValue,
                        End = currentEndValue,
                    };
                    allSortedSeeds.Add(newRange);
                    _logger.Debug($"New Range created for Start: {newRange.Start}, End: {newRange.End}");
                }
            }

            List<BigInteger> remainingValues = new List<BigInteger>();
            while (sortedStartIndex < startSorted.Count)
            {
                remainingValues.Add(startSorted[sortedStartIndex++]);
            }
            while (sortedEndIndex < endSorted.Count)
            {
                remainingValues.Add(endSorted[sortedEndIndex++]);
            }

            remainingValues = remainingValues.OrderBy(v => v).ToList();

            if (remainingValues.Any())
            {
                allSortedSeeds.Add(new SeedRangeStruct
                {
                    Start = remainingValues[0],
                    End = remainingValues[1]
                });
            }

            return allSortedSeeds;
        }


        public List<SeedRangeStruct> TestCreateNewSeedRanges(BaseSeedStruct originalSeed, BigInteger mapStart, BigInteger mapEnd, BigInteger offSet, bool SeedStartInMapRange, bool SeedEndInMapRange)
        {
            List<SeedRangeStruct> newRanges = new List<SeedRangeStruct>();

            if (SeedStartInMapRange && !SeedEndInMapRange)
            {
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start + offSet,
                    End = mapEnd + offSet
                };
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = mapEnd + 1,
                    End = originalSeed.End
                };
                newRanges.Add(mappedRange);
                newRanges.Add(unmappedRange);
            }
            else if (SeedEndInMapRange && !SeedStartInMapRange)
            {
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start,
                    End = mapStart - 1
                };
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = mapStart + offSet,
                    End = originalSeed.End + offSet
                };
                newRanges.Add(unmappedRange);
                newRanges.Add(mappedRange);
            }
            else if (SeedStartInMapRange && SeedEndInMapRange)
            {
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start + offSet,
                    End = originalSeed.End + offSet
                };
                newRanges.Add(mappedRange);
            }
            else 
            {
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start,
                    End = originalSeed.End
                };
                newRanges.Add(unmappedRange);
            }

            return newRanges;
        }





        public List<SeedRangeStruct> TestCreateNewSeedRangesWithLogging(SeedRangeStruct originalSeed, BigInteger mapStart, BigInteger mapEnd, BigInteger offSet, bool SeedStartInMapRange, bool SeedEndInMapRange)
        {
            _logger.Debug($"Original range: [{originalSeed.Start}-{originalSeed.End}] Map: [{mapStart}-{mapEnd}] Offset: {offSet}");

            List<SeedRangeStruct> newRanges = new List<SeedRangeStruct>();
            if (SeedStartInMapRange && !SeedEndInMapRange)
            {
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start + offSet,
                    End = mapEnd + offSet
                };
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = mapEnd + 1,
                    End = originalSeed.End
                };
                newRanges.Add(mappedRange);
                newRanges.Add(unmappedRange);
                _logger.Debug($"Split: Start in range -> Mapped[{mappedRange.Start}-{mappedRange.End}] + Unmapped[{unmappedRange.Start}-{unmappedRange.End}]");
            }
            else if (SeedEndInMapRange && !SeedStartInMapRange)
            {
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start,
                    End = mapStart - 1
                };
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = mapStart + offSet,
                    End = originalSeed.End + offSet
                };
                newRanges.Add(unmappedRange);
                newRanges.Add(mappedRange);
                _logger.Debug($"Split: End in range -> Unmapped[{unmappedRange.Start}-{unmappedRange.End}] + Mapped[{mappedRange.Start}-{mappedRange.End}]");
            }
            else if (SeedStartInMapRange && SeedEndInMapRange)
            {
                SeedRangeStruct mappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start + offSet,
                    End = originalSeed.End + offSet
                };
                newRanges.Add(mappedRange);
                _logger.Debug($"Fully in range -> Mapped[{mappedRange.Start}-{mappedRange.End}]");
            }
            else
            {
                SeedRangeStruct unmappedRange = new SeedRangeStruct()
                {
                    Start = originalSeed.Start,
                    End = originalSeed.End
                };
                newRanges.Add(unmappedRange);
                _logger.Debug($"Outside range -> Unmapped[{unmappedRange.Start}-{unmappedRange.End}]");
            }
            return newRanges;
        }


        public List<SeedRangeStruct> ConvertBaseSeedToSeedStruct(List<BaseSeedStruct> baseSeeds)
        {
            List<SeedRangeStruct> newRanges = new List<SeedRangeStruct>();  

            foreach(var seed in baseSeeds) 
            {
                SeedRangeStruct newRange = new SeedRangeStruct()
                {
                    Start = seed.Start,
                    End = seed.End
                };
            }
            return newRanges;
        }



    }
}