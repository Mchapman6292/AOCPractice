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

namespace AdventOfCode.InputMappers
{
    class Program
    {

        public Day04Answer Day04 {  get; set; } 



        static void Main(string[] args)
        {

            Day04Logger day04Logger = new Day04Logger();
            Day04PuzzleInput day04Input = new Day04PuzzleInput(day04Logger);
            Day04Answer day04Answer = new Day04Answer(day04Input, day04Logger);


            int testAnswer = day04Answer.CompareNumbers();

            Console.WriteLine(testAnswer);

            Console.ReadKey();

            Log.CloseAndFlush();







        }

    }
}




