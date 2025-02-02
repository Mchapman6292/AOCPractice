using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;
using AdventOfCode._2023.Day05.SeedManager.Seeds;
using System.Numerics;
using AOCInputs;
using AdventOfCode._2023.Day05.SeedManager.SeedServices;

namespace AdventOfCode._2023.Day05.DayFiveAnswer
{

    /* 94, 14, 55, 13 
     *  - Initial values to transform 
     *
     * - 50 = destinationStart range start, 
     * - 98 = sourceStart range start
     * - 2 = range
     * 
     *                  FORMULA
     *  - First check if number falls between the destinationStart and sourceStart values
     *  - We need to calculate the difference in the destinationStart and sourceStart range
     *  - Then apply for the count(range) of numbers that are between 50-98 to the sourceStart range.
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

        private readonly SeedService _seedService;


        public Day05Answer(Day05Input input, LogManager logManager, SeedService seedService)
        {
            Input = input;
            _logger = logManager.GetLogger();
            _seedService = seedService;

        }

        public List<Seed> InitializeSeedList()
        {
            List<Seed> seeds = new List<Seed>
            {
                new Seed(4106085912),
                new Seed(135215567),
                new Seed(529248892),
                new Seed(159537194),
                new Seed(1281459911),
                new Seed(114322341),
                new Seed(1857095529),
                new Seed(814584370),
                new Seed(2999858074),
                new Seed(50388481),
                new Seed(3362084117),
                new Seed(37744902),
                new Seed(3471634344),
                new Seed(240133599),
                new Seed(3737494864),
                new Seed(346615684),
                new Seed(1585884643),
                new Seed(142273098),
                new Seed(917169654),
                new Seed(286257440)
            };
            return seeds;
        }



        public BigInteger Test()
        {
            List<Seed> seeds = InitializeSeedList();

            BigInteger lowestEndResult = seeds[0].CurrentValue;

            foreach (Seed currentSeed in seeds)
            {
                List<string> mapStrings = Input.GetAllMapStringsInList();

                foreach (string mapString in mapStrings)
                {
                    List<string> splitMapValues = Input.SplitMapValuesByLine(Input.SeedToSoilMap);

                    foreach (string splitValue in splitMapValues)
                    {
                        BigInteger currentValue = currentSeed.CurrentValue;

                        BigInteger destinationStart;
                        BigInteger sourceStart;
                        BigInteger range;

                        Input.ParseAlmanacNumbersFromLine(splitValue, out destinationStart, out sourceStart, out range);

                        BigInteger topRangeValue = _seedService.CalculateMaxSourceRange(sourceStart, range);

                        if (!_seedService.isWithinRange(currentValue, sourceStart, topRangeValue))
                        {
                            break;
                        }

                        BigInteger offSet = _seedService.CaclulateOffSet(sourceStart, range);

                        currentSeed.CurrentValue = currentValue + offSet;
                    }
                }
                if(currentSeed.CurrentValue < lowestEndResult) 
                {
                    lowestEndResult = currentSeed.CurrentValue; 
                }
            }
            return lowestEndResult;
        }




    }
}
    




















