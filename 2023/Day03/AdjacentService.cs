using AdventOfCode.CharRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.DigitRecords;
using AOCInputs;
using System.Reflection.Metadata.Ecma335;
using AOCPractice.AppLoggers;
using Serilog.Core;
using System.Text.RegularExpressions;
using AdventOfCode._2023.Day03;

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
        

        private readonly Day3Logger _appLogger;

        private Input _aocInput;

        public List<CharRecord> _charRecords {  get; set; } = new List<CharRecord>();
        public List<DigitRecord> _digitRecords { get; set; } = new List<DigitRecord>();






        public AdjacentService( Input aocInput, Day3Logger logger)
        {
            _aocInput = aocInput;
            _appLogger = logger;

        }


        public List<DigitRecord> CreateDigitRecordsFromInput(string inputString)
        {
            var matches = Regex.Matches(inputString, @"\d+");
            return matches.Cast<Match>().Select(m =>
            {
                int startIndex = m.Index;
                int endIndex = Math.Max(m.Index + m.Length - 1, m.Index);
                var digitRecord = new DigitRecord
                {
                    Value = int.Parse(m.Value),
                    StartIndex = startIndex,
                    EndIndex = endIndex
                };

                digitRecord.AllIndexes = digitRecord.GetAllIndices();

                return digitRecord;
            })
            .ToList();
        }


        public List<CharRecord> CreateCharRecordsFromInput(string inputString)
        {
            var matches = Regex.Matches(inputString, "[*+\\-/@#$=%&]");
            return matches.Cast<Match>().Select(n =>
            {
                char symbol = n.Value[0];
                int index = n.Index;
                CharRecord charRecord = new CharRecord
                {
                    Symbol = symbol,
                    Index = index,
                };
                return charRecord;
            })
            .ToList();
        }



        public void UpdateAdjacentDigitRecords(IEnumerable<CharRecord> charRecords, IEnumerable<DigitRecord> digitRecords)
        {

            foreach (var charRecord in charRecords)
            {
                charRecord.isChecked = true;

                List<int> positionsToCheck = CalculateIndexesToCheck(charRecord);

                var itemStrings = positionsToCheck.Select(item => item.ToString() ?? "null");

                _appLogger.Info($" Indexes to check for charRecord: {charRecord.Symbol}, charRecordIndex: {charRecord.Index} =       {positionsToCheck}.");
                _appLogger.LogEnum(positionsToCheck);


                foreach (var digitRecord in digitRecords)
                {


                    for (int i = digitRecord.StartIndex; i <= digitRecord.EndIndex; i++)
                    {

                        if (positionsToCheck.Contains(i))
                        {
                            digitRecord.IsAdjacent = true;
                            charRecord.AdjacentCount++;

                            charRecord.AdjacentRecords.Add(digitRecord);


                            _appLogger.Info($"ISADJACENT SET TO TRUE FOR VALUE: {digitRecord.Value}, startIndex: {digitRecord.StartIndex}, endIndex: {digitRecord.EndIndex}.");
                            break;
                        }


                        if (digitRecord.StartIndex == int.MaxValue)
                        {
                            _appLogger.Info($" StartIndex not set for {digitRecord.Value}, startIndex: {digitRecord.StartIndex}.");
                            throw new InvalidOperationException($" StartIndex not set for {digitRecord.Value}, startIndex: {digitRecord.StartIndex}.");
                        }
                        else if (digitRecord.EndIndex == int.MaxValue)
                        {
                            _appLogger.Info($" EndIndex not set for {digitRecord.Value} EndIndex: ({digitRecord.EndIndex}).");
                            throw new InvalidOperationException($" EndIndex not set for {digitRecord.Value} EndIndex: ({digitRecord.EndIndex}).");
                        }
                    }
                }
            }
        }


        // Each new line adds +2 to index count due to \r & \n


        public List<int> CalculateIndexesToCheck(CharRecord currentRecord)
        {
            if (currentRecord == null || currentRecord.Index == int.MaxValue || currentRecord.Symbol == null)
            {
                _appLogger.Info($"currentRecord property not set Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");
                throw new ArgumentNullException($"currentRecord property not set Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");
            }

            _appLogger.Info($"currentRecord: Index: {currentRecord.Index}, Symbol: {currentRecord.Symbol}, isChecked: {currentRecord.isChecked}.");

            int currentIndex = currentRecord.Index;
            List<int> indexesToCheck = new List<int>();

            int[] allDirections = { -143, -142, -141, -1, 1, 141, 142, 143 };
            int[] firstPositionDirections = { 1, 142, 143 };
            int[] secondPositionDirections = { -1, 1, 141, 142, 143 };
            int[] secondLastDirections = { -143, -142, -141, -1, 1, 141, 142 };
            int[] lastPositionDirections = { -143, -142, -1, 141, 142 };

            int position = currentIndex % 142;
            int[] directionsToUse;

            int lastLineIndex = _aocInput.FullInputString.Length - 141;

            if (currentIndex < 142)
            {
                if (currentIndex == 0) directionsToUse = new[] { 1, 142, 143 };
                else if (currentIndex == 1) directionsToUse = new[] { -1, 1, 142, 143 };
                else if (currentIndex == 140) directionsToUse = new[] { -1, 1, 141, 142 };
                else if (currentIndex == 141) directionsToUse = new[] { -1, 141, 142 };
                else directionsToUse = new[] { -1, 1, 141, 142, 143 };
            }
            else if (currentIndex >= lastLineIndex)
            {
                if (position == 0) directionsToUse = new[] { -142, -141, 1 };
                else if (position == 1) directionsToUse = new[] { -143, -142, -1, 1 };
                else if (position == 140) directionsToUse = new[] { -143, -142, -141, -1, 1 };
                else if (position == 141) directionsToUse = new[] { -143, -142, -1 };
                else directionsToUse = new[] { -143, -142, -141, -1, 1 };
            }
            else
            {
                if (position == 0) directionsToUse = firstPositionDirections;
                else if (position == 1) directionsToUse = secondPositionDirections;
                else if (position == 140) directionsToUse = secondLastDirections;
                else if (position == 141) directionsToUse = lastPositionDirections;
                else directionsToUse = allDirections;
            }

            indexesToCheck.AddRange(directionsToUse.Select(d => currentIndex + d));

            var loggedIndexes = string.Join(", ", indexesToCheck);
            _appLogger.Info($"Indexes to check for {currentRecord.Symbol} with index {currentRecord.Index} = {loggedIndexes}.");

            return indexesToCheck;
        }



        public int CalculateTotal(List<DigitRecord> digitRecords)
        {
            int total = 0;

            foreach (var digitRecord in digitRecords)
            {
                if (digitRecord.IsAdjacent)
                {
                    total += digitRecord.Value;
                    _appLogger.Info($"New Total : {total}.");
                }
            }
            return total;
        }

        public int calculateGearTotal(List<CharRecord> gearRecords)
        {
            int total = 0;

            foreach(CharRecord gearRecord in gearRecords)
            {
                int adjacentTotal = gearRecord.GetAllAdjacentValues();
                total += adjacentTotal;
            }
            return total;
        }


   
    }
}
