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


    public struct BaseSeedStruct
    {
        public BigInteger Start { get; set; }
        public BigInteger SeedCount { get; set; }
        public BigInteger End { get; set; }


        public BaseSeedStruct(BigInteger start, BigInteger seedCount)
        {
            Start = start;
            SeedCount = seedCount;
            End = start + seedCount - 1;
        }

    }

    public struct MapValueStruct
    {
        public BigInteger DestinationRange { get; set; }
        public BigInteger SourceRange { get; set; }
        public BigInteger Range {  get; set; }
        public BigInteger EndMapValue { get; set; }
        public BigInteger OffSet { get; set; }


        public MapValueStruct(BigInteger destinationRange, BigInteger sourceRange, BigInteger range) 
        {
            DestinationRange = destinationRange;
            SourceRange = sourceRange;
            Range = range;
            EndMapValue = sourceRange + range;
            OffSet = DestinationRange - SourceRange;
           
        }
    }


}
