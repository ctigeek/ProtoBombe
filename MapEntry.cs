using System;

namespace BombeProto1
{
    class MapConfiguration
    {
        public WheelType[] WheelTypes { get; set; }
        public ReflectorType ReflectorType { get; set; }
        public bool EnableDiagonalBoard { get; set; }
        public char CurrentEntry { get; set; }
        public char InputLetter { get; set; }
        public MapEntry[] MapEntries { get; set; }
    }

    internal class MapEntry
    {
        public int StepsAheadOfKey { get; set; }
        public char LeftChar { get; set; }
        public char RightChar { get; set; }

        public MapEntry(int stepsAhead, char leftChar, char rightChar)
        {
            if (leftChar >= rightChar) throw new ArgumentException();
            this.StepsAheadOfKey = stepsAhead;
            this.LeftChar = leftChar;
            this.RightChar = rightChar;
        }
    }
}
