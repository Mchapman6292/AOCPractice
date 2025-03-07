﻿using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using System.Numerics;

/* First step is to find valid numbers/range that can be passed to the method
 * 
 * 
 * 
 * */


namespace AdventOfCode._2023.Day05.Mappers.BaseMappers
{
    public class BaseMapper
    {

        private readonly Day05Logger _logger;


        public BaseMapper(LogManager logManager)
        {
            _logger = logManager.GetLogger();
        }

        public void MapValues(BigInteger destinationStart, BigInteger sourceStart, BigInteger range, out BigInteger newDestination, out BigInteger newSource)
        {
            newDestination = destinationStart;
            newSource = sourceStart;

            BigInteger difference = CalculateValidRange(sourceStart, destinationStart);

            if (difference > 0) 
            {
                newDestination += difference;
                newSource += difference;
            }

            else
            {
                newDestination -= difference;
                newSource -= difference;
            }
        }

        public BigInteger CalculateValidRange(BigInteger sourceStart, BigInteger destinationStart)
        {
            return sourceStart - destinationStart;
        }

       




    }
}
