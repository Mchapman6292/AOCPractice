using System.Numerics;

namespace AdventOfCode._2023.Day05.SeedRangeStructures
{
    public struct SeedRangeStruct
    {
        public BigInteger Start { get; set; }
        public BigInteger End { get; set; }



        public SeedRangeStruct(BigInteger start, BigInteger end)
        {
            Start = start;
            End = end;
        }
    }


    public struct BaseSeedStructure
    {
        public BigInteger Start { get; set; }
        public BigInteger SeedCount { get; set; }
        public BigInteger End { get; set; }


        public BaseSeedStructure(BigInteger start, BigInteger seedCount)
        {
            Start = start;
            SeedCount = seedCount;
            End = start + seedCount;
        }

    }
}
