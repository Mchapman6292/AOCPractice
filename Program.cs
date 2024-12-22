using System.Security.Cryptography.X509Certificates;
using AdventOfCode._2023.Day04.Day04Answers;
using AdventOfCode._2023.Day04;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using AdventOfCode._2023.Day04.Day04Inputs;
using AdventOfCode._2023.Day04Loggers;
using System;

using AdventOfCode._2023.Day04.Day04PartTwo;

namespace AdventOfCode.InputMappers
{
    class Program
    {

        public Day04Answer Day04 { get; set; }



        static void Main(string[] args)
        {
            Day04Logger day04Logger = new Day04Logger();
            Day04Input aocInput = new Day04Input();
            Day04InputFormatter inputFormatter = new Day04InputFormatter(day04Logger, aocInput);
            Day04Part2 day04Part2 = new Day04Part2(inputFormatter, aocInput, day04Logger);
            Day04Answer day04Answer = new Day04Answer(inputFormatter, day04Logger, day04Part2);



            string[] fullString = inputFormatter.GenerateFullSplitInput();



            int part2Answer = day04Part2.CalculateTotalScratchcards();


            Console.WriteLine($"Part 2 answer: {part2Answer}");



            Console.ReadKey();








        }

    }
}