using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using AOCInputs;
using AOCPractice.AdjacentServices;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Cryptography.X509Certificates;
using AdventOfCode._2023.Day04.Day04Answers;
using AdventOfCode._2023.Day04;
using AdventOfCode._2023.Day04.Day04PuzzleInputs;
using Serilog;
using AdventOfCode._2023.Day04.Day04PartTwo;

namespace AdventOfCode.InputMappers
{
    class Program
    {

        public Day04Answer Day04 {  get; set; } 



        static void Main(string[] args)
        {

            Day04Logger day04Logger = new Day04Logger();
            Day04PuzzleInput day04Input = new Day04PuzzleInput(day04Logger);
            Day04Part2 day04Part2 = new Day04Part2(day04Logger, day04Input);
            Day04Answer day04Answer = new Day04Answer(day04Input, day04Logger, day04Part2);



            string[] testStrings = day04Input.GenerateSplitInputString();

           day04Answer.GetTotalMatches(testStrings);


            Console.ReadKey();

            Log.CloseAndFlush();







        }

    }
}




