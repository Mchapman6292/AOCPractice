using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using AOCInputs;
using AOCPractice.AdjacentServices;
using AOCPractice.AppLoggers;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Cryptography.X509Certificates;


namespace AdventOfCode.InputMappers
{
    class Program
    {
        // Handle checking first and last array where we only check the next/previous array
        //Do not count numbers twice if they are adjacent to two different symbols.



        public char[] Symbols => new[] { '*', '+', '-', '/', '@', '#', '$', '=', '%', '&' };


        
  

        static void Main(string[] args)
        {
            Input aocInput = new Input();
            AppLogger applogger = new AppLogger();

            AdjacentService adjacentService = new AdjacentService(aocInput, applogger);



            applogger.ClearLog();

            string inputString = aocInput.FullInputString;

            string testString = aocInput.TestString;



            List<char> newSymbols = aocInput.GetAllCharsAndSpaceFromString(inputString);



            int carriageReturns = inputString.Count(c => c == '\r');
            int newLines = inputString.Count(c => c == '\n');


            string removedNewLineString = testString.TrimEnd('\r', '\n');


            string removedFullString = inputString.TrimEnd('\r', '\n');

            int removedFullCount = removedFullString.Length;

            bool is140CharsLine =  removedFullCount % 140 == 0;






            List <CharRecord> charRecords = adjacentService.CreateCharRecordsFromInput(removedFullString);

            List<DigitRecord> digitRecords = adjacentService.CreateDigitRecordsFromInput(removedFullString);


            DigitRecord digit190 = digitRecords.FirstOrDefault(digit => digit.Value == 190);
            DigitRecord digit881 = digitRecords.FirstOrDefault(digit => digit.Value == 881);

            string digit190Ind = digit190.ReturnIndexesAsString();
            string digit881Ind = digit881.ReturnIndexesAsString();








            adjacentService.UpdateAdjacentDigitRecords(charRecords, digitRecords);












            int digitRecordCount = digitRecords.Count();

            int isAdjacentCount = digitRecords.Count(d => d.IsAdjacent);

            applogger.Info($"Total number of DigitRecords: {digitRecordCount} , number of adjacent records: {isAdjacentCount}.");   


            int total = adjacentService.CalculateTotal(digitRecords);


            Console.WriteLine($"Total : {total}");



            Console.WriteLine($"DigitRecord 190 Indexes : {digit190Ind}.");
            Console.WriteLine($"DigitRecord 881 Indexes : {digit881Ind}.");


            Console.WriteLine($"Full string length : {removedFullCount}., lines: {removedFullCount}.");

            Console.WriteLine($" 140 chars per line =  {is140CharsLine}.");

            Console.WriteLine($" Carriage : {carriageReturns}");
            Console.WriteLine($" newLine : {newLines}.");

            applogger.LogList(newSymbols);




            var gearRecords = charRecords
         .Where(g => g.Symbol == '*' && g.AdjacentCount == 2).ToList();


            foreach (var record in gearRecords)
            {
                int adjacentCount = record.AdjacentRecords.Count;
                Console.WriteLine($" Number of Adjacent records {adjacentCount}.");
            }

            int gearTotal = adjacentService.calculateGearTotal(gearRecords);


            Console.WriteLine($"Gear total : {gearTotal}"); 





            Console.ReadKey();

        }


      
    }
}




