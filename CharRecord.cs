using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.CharRecords
{
    public class CharRecord
    {
        public char? Symbol { get; set; } = null;
        public int Index { get; set; } = int.MaxValue;
        public bool isChecked { get; set; } =  false;

    }

}
