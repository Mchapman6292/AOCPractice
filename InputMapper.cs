using AdventOfCode.CharRecords;
using AdventOfCode.DigitRecords;
using AOCInputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOCPractice.InputMappers
{
    public class InputMapper
    {
        public AOCInput AocInput { get; set; }
        public string[] splitInput;


        public IEnumerable<CharRecord> CharRecords { get; private set; }
        public IEnumerable<DigitRecord> DigitRecords { get; private set; }



        public InputMapper(AOCInput aocInput)
        {
            AocInput = aocInput;
            splitInput = AocInput.FullInputString.Split('\n');
            CharRecords = CreateCharRecordsFromInput(AocInput.FullInputString);
            DigitRecords = CreateDigitRecordsFromInput(AocInput.FullInputString);
        }


        // 140 chars per line

        public char[] Symbols => new[] { '*', '+', '-', '/', '@', '#', '$', '=', '%', '&' };

        public IEnumerable<DigitRecord> CreateDigitRecordsFromInput(string inputString)
        {
            var digitMatches = Regex.Matches(inputString, @"\d+");
            return digitMatches.Cast<Match>().Select(m => {
                int startIndex = m.Index;
                int endIndex = Math.Max(m.Index + m.Length - 1, m.Index);

                return new DigitRecord
                {
                    Value = int.Parse(m.Value),
                    StartIndex = startIndex,
                    EndIndex = endIndex,
                    AllIndexes = Enumerable.Range(startIndex, endIndex - startIndex + 1).ToList()
                };
            });
        }


        public IEnumerable<CharRecord> CreateCharRecordsFromInput(string inputString)
        {
            var symbolMatches = Regex.Matches(inputString, "[*+\\-/@#$=%&]");
            return symbolMatches.Cast<Match>().Select(m => new CharRecord
            {
                Symbol = m.Value[0],
                Index = m.Index
            });
        }

        

    }
}

