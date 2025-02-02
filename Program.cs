﻿using AdventOfCode._2023.Day05.DayFiveAnswer;
using AdventOfCode._2023.Day05.InputService.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveLogger;
using AdventOfCode._2023.Day05.LogManagers;

namespace AdventOfCode.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Day05Logger logger = new Day05Logger();
            LogManager logManager = new LogManager(logger);
            Day05Input day05Input = new Day05Input(logManager);
            Day05Answer day5Ans = new Day05Answer(day05Input, logManager);





        }
    }
}