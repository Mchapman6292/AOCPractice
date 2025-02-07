using AdventOfCode._2023.Day05.Almanacs;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using DocumentFormat.OpenXml.Drawing;
using System.Numerics;
using AdventOfCode._2023.Day05.SeedRangeStructures;

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






        public bool isWithinRange(BigInteger currentValue, BigInteger sourceStart, BigInteger sourceRange)
        {
            return(currentValue >= sourceStart && currentValue <= sourceRange);
        }

        public bool Part2IsWithinRange(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd)
        {
            return startSeed <= sourceEnd && endSeed >= sourceStart;
        }


        public SeedRangeStructure Part2CalculateNewSeedRange(BigInteger startSeed, BigInteger endSeed, BigInteger sourceStart, BigInteger sourceEnd)
        {
            return new SeedRangeStructure
            {
                Start = BigInteger.Max(startSeed, sourceStart),
                End = BigInteger.Max(endSeed, sourceEnd)
            };  
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












    }
}
