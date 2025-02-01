using AdventOfCode._2023.Day05.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using System.Numerics;

namespace AdventOfCode._2023.Day05.DayFiveAnswer
{

    /* 94, 14, 55, 13 
     *  - Initial values to transform 
     *
     * - 50 = destination range start, 
     * - 98 = source range start
     * - 2 = range
     * 
     *                  FORMULA
     *  - First check if number falls between the destination and source values
     *  - We need to calculate the difference in the destination and source range
     *  - Then apply for the count(range) of numbers that are between 50-98 to the source range.
     *  
     *  
     *  We need to apply this to all values through all maps and then find the lowest output.
     *  We don't need to apply this to all of the numbers within the range just the highest/lower value
     *  Possibly only interested in whether the values in the range increased or decreased as we only need the end lowest value. 





    */


    public class Day05Answer
    {
        public Day05Input Input { get; set; }

        private readonly Day05Logger _logger;



        public Day05Answer(Day05Input input, LogManager logManager) 
        {
            Input = input;
            _logger = logManager.GetLogger();
        }





        


    }
}
