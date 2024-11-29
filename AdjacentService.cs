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

        private IEnumerable<CharRecord> _charRecords;
        private IEnumerable<DigitRecord> _digitRecords;

        private InputMapper _inputMapper;

        private AOCInput _aocInput;



        public IEnumerable<CharRecord> CharRecords
        {
            get { return _charRecords; }
            private set { _charRecords = value; }
        }

        public IEnumerable<DigitRecord> DigitRecords
        {
            get { return _digitRecords; }
            private set { _digitRecords = value; }
        }



        public int Total = 0;

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

        public AdjacentService(InputMapper inputMapper, AOCInput aocInput)
        {
            _inputMapper = inputMapper;
            _charRecords = inputMapper.CharRecords;
            _digitRecords = inputMapper.DigitRecords;
            _aocInput = aocInput;

        }








        public void GetAllAdjacentDigitRecords(IEnumerable<CharRecord> charRecords, IEnumerable<DigitRecord> digitRecords)
        {
            foreach (var charRecord in charRecords)
            {
                if (charRecord.IsChecked)
                {
                    continue;
                }

                List<int> positionsToCheck = CalculateIndexesToCheck(charRecord);

                foreach (var digitRecord in digitRecords)
                {
                    if(digitRecord.IsValid == true) 
                    {
                        continue;
                    }
                    if (positionsToCheck.Contains(digitRecord.StartIndex) || positionsToCheck.Contains(digitRecord.EndIndex))
                    {
                        digitRecord.SetValid(true);
                        Console.WriteLine($" digitRecord {digitRecord.Value} set to true. StartIndex: {digitRecord.StartIndex}, EndIndex: {digitRecord.EndIndex}.");
                    }
                }
            }
        }



        public void CalculateTotal(IEnumerable<DigitRecord> digitRecords)
        {

            foreach(var digitRecord in digitRecords)
            {
                if (digitRecord.IsValid)
                {
                    Total += digitRecord.Value;
                    Console.WriteLine($"New Total : {Total}.");
                }
            }
            
        }




        public List<int> CalculateIndexesToCheck(CharRecord currentRecord)
        {


            int currentIndex = currentRecord.Index;
            var indexesToCheck = new List<int>();
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
            return indexesToCheck;
        }
    }
}
