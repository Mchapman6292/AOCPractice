using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using AOCInputs;
using AOCPractice.AdjacentServices;
using AOCPractice.AppLoggers;
using AOCPractice.InputMappers;


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
            AppLogger applogger = new AppLogger();
            AdjacentService adjacentService = new AdjacentService(inputMapper,aocInput, applogger);
  

            string inputString = aocInput.FullInputString;

            string testString = aocInput.TestString;




            IEnumerable<CharRecord> charRecords = inputMapper.CreateCharRecordsFromInput(testString);

            IEnumerable<DigitRecord> digitRecords = inputMapper.CreateDigitRecordsFromInput(testString);

            adjacentService.UpdateAdjacentDigitRecords(charRecords, digitRecords);


            applogger.LogRecord<CharRecord>(charRecords);
            applogger.LogRecord<DigitRecord> (digitRecords);



            int digitRecordCount = digitRecords.Count();

            int isAdjacentCount = digitRecords.Count(d => d.IsAdjacent);




            applogger.Info($"Total number of DigitRecords: {digitRecordCount} , number of adjacent records: {isAdjacentCount}.");   



            int total = adjacentService.CalculateTotal(digitRecords);


            Console.WriteLine($"Total : {total}");






            Console.ReadKey();

        }


      
    }
}




