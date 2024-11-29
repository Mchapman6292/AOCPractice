using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using System.Text.RegularExpressions;
using System.Linq;
using  AOCInputs;
using AOCPractice.InputMappers;
using AOCPractice.AdjacentServices;


namespace AdventOfCode.InputMappers
{
    class Program
    {
        // Handle checking first and last array where we only check the next/previous array
        //

        public char[] Symbols => new[] { '*', '+', '-', '/', '@', '#', '$', '=', '%', '&' };

        static void Main(string[] args)
        {
            AOCInput aocInput = new AOCInput();
            InputMapper inputMapper = new InputMapper(aocInput);
            AdjacentService adjacentService = new AdjacentService(inputMapper,aocInput);

            string inputString = aocInput.FullInputString;


            inputMapper.ControllerMapAllRecords(inputString);


            IEnumerable<CharRecord> charRecords = adjacentService.CharRecords;
            IEnumerable<DigitRecord> digitRecords = adjacentService.DigitRecords;


            adjacentService.GetAllAdjacentDigitRecords(charRecords, digitRecords);


            int total = adjacentService.Total;


            Console.WriteLine($"Total : {total}");






            Console.ReadKey();

        }


      
    }
}




