using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
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
     *  - We don't need to apply the calculations until all mappings are complete,we then perform the operation on the original seed. 
     *  
     *  
     *  We need to apply this to all values through all maps and then find the lowest output.
     *  We don't need to apply this to all of the numbers within the range just the highest/lower value
     *  Possibly only interested in whether the values in the range increased or decreased as we only need the end lowest value. 
     *  
     *  
     *  
     *  Source range starts at 98 and goes for 2 numbers (98, 99)
     *  Destination range starts at 50 and goes for 2 numbers (50, 51)
     *  
     *  
     *  Seed 4106085912
     *  1640984363 3136305987 77225710
     *  We calculate 3136305987  + 77225710 to find our range, if our number falls between the result we then calculate the difference  between 3136305987 - 1640984363 and then apply that to all values within our initially calculated range





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


        Seed testSeed1 = new Seed(4106085912);









    }
}
