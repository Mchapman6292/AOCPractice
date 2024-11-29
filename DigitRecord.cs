using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DigitRecords
{
   public class DigitRecord
    {
        public int Value;
        public int StartIndex;
        public int EndIndex;
        bool _isValid = false;



        public bool IsValid
        {
            get => _isValid;
            private set => _isValid = value;
        }

        public void SetValid(bool value)
        {
            IsValid = value;
        }
    }
}