using AdventOfCode.CharRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.DigitRecords;
using AOCInputs;
using AOCPractice.InputMappers;
using System.Reflection.Metadata.Ecma335;
using AOCPractice.AppLoggers;
using Serilog.Core;

namespace AOCPractice.AdjacentServices
{
    public class AdjacentService
    {
        // 140 chars per line
        //Most efficient to find chars and check for adjacent numbers
        // Need to only check ahead for first line 
        // Only check behind for last line
        // Need to have a valid range to check based on proximity to start/end
        // Define regular rules first and then build edge cases
        // Need to handle cases where symbol is found at the start/end of the line. 
        // Do we need to add numbers that are adjacent to two symbols twice??
        // Numbers that are broken up by a symbol
        

        private readonly AppLogger _appLogger;

        private InputMapper _inputMapper;

        private AOCInput _aocInput;



        public int CurrentIndex { get; set; }

        public List<int> FirstIndexEdgeCases = new List<int>();

        public List<int> SecondIndexEdgeCases = new List<int>();

        public List<int> ThirdIndexEdgeCases = new List<int>();

        public List<int> ThirsLastIndexEdgeCases = new List<int>();

        public List<int> SecondLastIndexCases = new List<int>();
        public List<int> LastIndexEdgeCases = new List<int>();

        // Computed properties calculate values on-the-fly based on CurrentIndex when accessed
        public int TopLeftIndex => CurrentIndex - 141;
        public int TopIndex => CurrentIndex - 140;
        public int TopRightIndex => CurrentIndex - 139;
        public int LeftIndex => CurrentIndex - 1;
        public int RightIndex => CurrentIndex + 1;
        public int BottomLeftIndex => CurrentIndex + 139;
        public int BottomIndex => CurrentIndex + 140;
        public int BottomRightIndex => CurrentIndex + 141;

        public AdjacentService(InputMapper inputMapper, AOCInput aocInput, AppLogger logger)
        {
            _inputMapper = inputMapper;
            _aocInput = aocInput;
            _appLogger = logger;

        }


        public void UpdateAdjacentDigitRecords(IEnumerable<CharRecord> charRecords, IEnumerable<DigitRecord> digitRecords)
        {

            List<DigitRecord> result = new List<DigitRecord>(); 

            foreach (var charRecord in charRecords)
            {
                charRecord.isChecked = true;

                List<int> positionsToCheck = CalculateIndexesToCheck(charRecord);

                _appLogger.Info($" Indexes to check for charRecord: {charRecord.Symbol}, charRecordIndex: {charRecord.Index}.");
                _appLogger.LogList(positionsToCheck);


                foreach (var digitRecord in digitRecords)
                {
                    

                    for (int i = digitRecord.StartIndex; i <= digitRecord.EndIndex; i++)
                    {
                        if (positionsToCheck.Contains(i))
                        {
                            digitRecord.IsAdjacent = true;
                            break;
                        }
                        

                        if (digitRecord.StartIndex == int.MaxValue)
                        {
                            _appLogger.Info($" StartIndex not set for {digitRecord.Value}, startIndex: {digitRecord.StartIndex}.");
                        }
                        else if (digitRecord.EndIndex == int.MaxValue)
                        {
                            _appLogger.Info($" EndIndex not set for {digitRecord.Value} EndIndex: ({digitRecord.EndIndex}).");
                        }
                    }
                } 
            }
        }




        public int CalculateTotal(IEnumerable<DigitRecord> digitRecords)
        {
            int total = 0;

            foreach(var digitRecord in digitRecords)
            {
                if (digitRecord.IsAdjacent)
                {
                    total += digitRecord.Value;
                    _appLogger.Info($"New Total : {total}.");
                }
            }
            return total;
        }




        public List<int> CalculateIndexesToCheck(CharRecord currentRecord)
        {

            if(currentRecord == null || currentRecord.Index == int.MaxValue || currentRecord.Symbol == null)
            {
                _appLogger.Info($"currentRecord property not set Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");
                throw new ArgumentNullException($"currentRecord property not set Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");
            }

            _appLogger.Info($"currentRecord: Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");


            int currentIndex = currentRecord.Index;


            List<int> indexesToCheck = new List<int>();
            
         
            int[] allDirections = { -141, -140, -139, -1, 1, 139, 140, 141 };
            int[] firstPositionDirections = { 1, 140, 141 };
            int[] secondPositionDirections = { -1, 1, 139, 140, 141 };
            int[] secondLastDirections = { -141, -140, -139, -1, 1, 139, 140 };
            int[] lastPositionDirections = { -141, -140, -1, 139, 140 };


            int position = currentIndex % 140;
            int[] directionsToUse;

            int lastLineIndex = _aocInput.FullInputString.Length - 139;

            // Handle first line where we need to only check ahead. 
            if (currentIndex < 140)
            {
                if (currentIndex == 0) directionsToUse = new[] { 1, 140, 141 };
                else if (currentIndex == 1) directionsToUse = new[] { -1, 1, 140, 141 };
                else if (currentIndex == 138) directionsToUse = new[] { -1, 1, 139, 140 };
                else if (currentIndex == 139) directionsToUse = new[] { -1, 139, 140 };
                else directionsToUse = new[] { -1, 1, 139, 140, 141 };
            }
            // After one line(140 spaces) we use the modulo to calculate the position and select the rules we need to apply. 

            // Handle last line.
            else if (currentIndex >= lastLineIndex)
            {
                if (position == 0) directionsToUse = new[] { -140, -139, 1 };
                else if (position == 1) directionsToUse = new[] { -141, -140, -1, 1 };
                else if (position == 138) directionsToUse = new[] { -141, -140, -139, -1, 1 };
                else if (position == 139) directionsToUse = new[] { -141, -140, -1 };
                else directionsToUse = new[] { -141, -140, -139, -1, 1 };
            }
            else
            {
                if (position == 0) directionsToUse = firstPositionDirections;
                else if (position == 1) directionsToUse = secondPositionDirections;
                else if (position == 138) directionsToUse = secondLastDirections;
                else if (position == 139) directionsToUse = lastPositionDirections;
                else directionsToUse = allDirections;
            }

            indexesToCheck.AddRange(directionsToUse.Select(d => currentIndex + d));

            _appLogger.LogList(indexesToCheck);
            
            return indexesToCheck;
        }
    }
}
