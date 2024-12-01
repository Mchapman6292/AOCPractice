using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DigitRecords
{
    public class DigitRecord
    {
        public int Value { get; set; } = int.MinValue;
        public int StartIndex { get; set; } = int.MaxValue;
        public int EndIndex { get; set; } = int.MaxValue;

        public List<int> AllIndexes { get; set; } = new List<int>();
        public bool IsAdjacent { get; set; } = false;



        public List<int> GetAllIndices()
        {
            return Enumerable.Range(StartIndex, EndIndex - StartIndex + 1).ToList();
        }
    }
}