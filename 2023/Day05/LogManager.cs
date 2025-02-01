using AdventOfCode._2023.Day05.DayFiveLogger;

namespace AdventOfCode._2023.Day05.LogManagers
{
    public class LogManager
    {
        private readonly Day05Logger _logger;


        public LogManager(Day05Logger logger)
        {
            _logger =logger;
        }


        public Day05Logger GetLogger()
        { 
            return _logger; 
        }
    }
}
