using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using AOCPractice.AdjacentServices;
using System;
using AdventOfCode._2023.Day03.Day03Loggers;
using AOCInputs;


namespace AdventOfCode._2023.Day03.Day03Answer
{
    public class Day03Answer
    {


        public void CalculateAnswer()
        {

            Input aocInput = new Input();
            Day03Logger appLogger = new Day03Logger();

            AdjacentService adjacentService = new AdjacentService(aocInput, appLogger);



            appLogger.ClearLog();
                string inputString = aocInput.FullInputString;
            string testString = aocInput.TestString;


            // Clean input strings by removing Windows line endings (\r\n) and Unix line endings (\n)
            // Calculations to find Index are made assuming that each new line is 140 chars so we need to remove any extras.



            string removedNewLineString = testString.TrimEnd('\r', '\n');
            string removedFullString = inputString.TrimEnd('\r', '\n');



            // Create CharRecord & DigitRecord lists
            List<CharRecord> charRecords = adjacentService.CreateCharRecordsFromInput(removedFullString);
            List<DigitRecord> digitRecords = adjacentService.CreateDigitRecordsFromInput(removedFullString);


            // By comparing the indexes of digit190Ind & digit881Ind I could see that even though I had adjusted the removed the (\r\n) chars it was still 2 off.
            // I adjusted the calculations used in GetAllCharsAndSpaceFromString to use 142 as the value to check directly above/below.
            // After being unable to account for the extra 2 Indexes myself some help from ChatGpt helped explain.
            //  TrimEnd only removes the final \r\n, leaving the \r\n between each line intact, explaining the extra 2 indexes per line.

            List<char> newSymbols = aocInput.GetAllCharsAndSpaceFromString(inputString);

            DigitRecord digit190 = digitRecords.FirstOrDefault(digit => digit.Value == 190);
            DigitRecord digit881 = digitRecords.FirstOrDefault(digit => digit.Value == 881);

            string digit190Ind = digit190.ReturnIndexesAsString();
            string digit881Ind = digit881.ReturnIndexesAsString();



            // Update the Adjacent bool and count for Digit records

            adjacentService.UpdateAdjacentDigitRecords(charRecords, digitRecords);

                // Calculate answer 

                int part1Total = adjacentService.CalculateTotal(digitRecords);



            /// Part 2 Uses the same records updated in part 1, we just need to filter CharRecords  for records with '*' and adJacentCount == 2
            /// 
            var gearRecords = charRecords
                            .Where(g => g.Symbol == '*' && g.AdjacentCount == 2).ToList();



            int gearTotal = adjacentService.calculateGearTotal(gearRecords);



            // Random bits of logging/Console that used.


            int carriageReturns = inputString.Count(c => c == '\r');
            int newLines = inputString.Count(c => c == '\n');

            int removedFullCount = removedFullString.Length;
            bool is140CharsLine = removedFullCount % 140 == 0;



            int digitRecordCount = digitRecords.Count();

            int isAdjacentCount = digitRecords.Count(d => d.IsAdjacent);

            appLogger.Info($"Total number of DigitRecords: {digitRecordCount} , number of adjacent records: {isAdjacentCount}.");


            int total = adjacentService.CalculateTotal(digitRecords);


            Console.WriteLine($"Total : {total}");



            Console.WriteLine($"DigitRecord 190 Indexes : {digit190Ind}.");
            Console.WriteLine($"DigitRecord 881 Indexes : {digit881Ind}.");


            Console.WriteLine($"Full string length : {removedFullCount}., lines: {removedFullCount}.");

            Console.WriteLine($" 140 chars per line =  {is140CharsLine}.");

            Console.WriteLine($" Carriage : {carriageReturns}");
            Console.WriteLine($" newLine : {newLines}.");

            appLogger.LogList(newSymbols);




            foreach (var record in gearRecords)
            {
            int adjacentCount = record.AdjacentRecords.Count;
            Console.WriteLine($" Number of Adjacent records {adjacentCount}.");
            }



            Console.WriteLine($"Gear total : {gearTotal}");


            Console.ReadKey();

        }
    }
}
