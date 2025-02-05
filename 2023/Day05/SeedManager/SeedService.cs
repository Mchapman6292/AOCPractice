using AdventOfCode._2023.Day05.Almanacs;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
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



        public BigInteger  Test(BigInteger sourceStart, BigInteger range, Seed currentSeed)
        {
            BigInteger updatedRange = CalculateMaxSourceRange(sourceStart, range);

            if(!isWithinRange(currentSeed.CurrentValue, sourceStart, updatedRange))
            {
                return currentSeed.CurrentValue;
            }

            return currentSeed.CurrentValue + updatedRange;
        }

        // Returns the new highest source range only. 
        public BigInteger CalculateMaxSourceRange(BigInteger sourceStart, BigInteger Range)
        {
            return sourceStart + Range;
        }


        public bool isWithinRange(BigInteger currentValue, BigInteger sourceStart, BigInteger sourceRange)
        {
            return(currentValue >= sourceStart && currentValue <= sourceRange);
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
