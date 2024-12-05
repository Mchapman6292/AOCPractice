using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.DigitRecords;

namespace AdventOfCode.CharRecords
{
    public class CharRecord
    {
        public char? Symbol { get; set; } = null;
        public int Index { get; set; } = int.MaxValue;
        public bool isChecked { get; set; } =  false;

        public int AdjacentCount { get; set; }  

        public List<DigitRecord> AdjacentRecords { get; set; } = new List<DigitRecord>();   



        public int GetAllAdjacentValues()
        {
            int total = 1;

            foreach(DigitRecord record in AdjacentRecords)
            {
                total *= record.Value;
            }
            return total;
        }
    }

}
