using AOCPractice.BaseLoggers;
using AdventOfCode._2023.Day05.MapTypes;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using System.Numerics;
namespace AdventOfCode._2023.Day05.DayFiveLogger
{
    public class Day05Logger : BaseLogger
    {


        public void LogSeedMapValues(Seed seed)
        {
            if (seed == null) 
            {
                throw new ArgumentNullException(nameof(seed));
            }

            foreach(var mapValue in seed.MapValues) 
            {
                BigInteger? value = mapValue.Value;

                day05Logger.Debug($"{value.ToString() ?? "Value is null"}.");
            }
        }










    }
}
