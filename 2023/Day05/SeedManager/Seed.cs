using System.Numerics;
using AdventOfCode._2023.Day05.MapTypes;

namespace AdventOfCode._2023.Day05.SeedManager.Seeds
{
    public class Seed
    {
        public BigInteger StartValue;
        public BigInteger CurrentValue { get; set; }    
        public SortedDictionary<MapType, BigInteger?> MapValues { get; set; }
        public BigInteger EndMapValue { get; set; }

        public Seed(BigInteger startValue)
        {
            StartValue = startValue;

            CurrentValue = StartValue;

            MapValues = new SortedDictionary<MapType, BigInteger?>
            {
                { MapType.SeedToSoil, null },
                { MapType.SoilToFertilizer, null },
                { MapType.FertilizerToWater, null },
                { MapType.WaterToLight, null },
                { MapType.LightToTemperature, null },
                { MapType.TemperatureToHumidity, null },
                { MapType.HumidityToLocation, null }
            };
        }





    }

}
