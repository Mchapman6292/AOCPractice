using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.CharRecords
{
    public class CharRecord
    {
        public char Symbol;
        public int Index;
        private bool _isChecked = false;



        public bool IsChecked
        {
            get => _isChecked;
            private set => _isChecked = value;
        }

        public void SetChecked(bool value)
        {
            IsChecked = value;
        }
    }

}
