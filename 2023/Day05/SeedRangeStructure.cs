using System.Numerics;

namespace AdventOfCode._2023.Day05.SeedRangeStructures
{
    public struct SeedRangeStructure
    {
        public BigInteger Start { get; set; }
        public BigInteger End { get; set; }

        public SeedRangeStructure(BigInteger start, BigInteger end)
        {
            Start = start;
            End = end;
        }
    }
}
