using System.Security.Cryptography.X509Certificates;
using AdventOfCode._2023.Day04.Day04Answers;
using AdventOfCode._2023.Day04;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using AdventOfCode._2023.Day04.Day04Inputs;
using AdventOfCode._2023.Day04Loggers;
using System;
using AdventOfCode._2023.Day05.DayFiveInput;
using AdventOfCode._2023.Day05.DayFiveAnswer;

using AdventOfCode._2023.Day04.Day04PartTwo;
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

            Day05Answer day5Ans = new Day05Answer(day05Input);





        }
    }
}