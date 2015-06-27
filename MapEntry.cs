
using System;

namespace BombeProto1
{
    class MapEntry
    {
        public readonly int StepsAheadOfKey;
        public readonly char LeftChar;
        public readonly char RightChar;

        public MapEntry(int stepsAhead, char leftChar, char rightChar)
        {
            if (leftChar >= rightChar) throw new ArgumentException();
            this.StepsAheadOfKey = stepsAhead;
            this.LeftChar = leftChar;
            this.RightChar = rightChar;
            
        }
    }
}
