using System;
using System.Linq;

namespace BombeProto1
{
    public enum WheelType
    {
        I,
        II,
        III
    }

    abstract class Wheel
    {
        public readonly WheelType WheelType;
        private int[] Notches { get; set; }
        private int[] Mapping { get; set; }
        private int[] ReverseMapping { get; set; }
        //private bool[] Signaled { get; set; }
        public int Position { get; set; }
        public bool EnableNotch { get; set; }
        public Action<int> SignalOutRight { get; set; }
        public Action<int> SignalOutLeft { get; set; }
        public Action RotateNotch { get; set; }

        protected Wheel(WheelType wheelType, int[] map, int[] notches)
        {
            this.WheelType = wheelType;
          //  Signaled = new bool[26];
            EnableNotch = true;
            if (map.Length != 26) throw new ArgumentException();
            Mapping = map;
            Notches = notches;
            ReverseMapping = new int[26];
            for (int i=0; i<map.Length; i++)
            {
                var reverseNum = i + map[i];
                if (reverseNum < 0 || reverseNum > 25 ) throw new InvalidOperationException();
                ReverseMapping[reverseNum] = -map[i];
            }
            //ResetAllSignals();
        }

        public void Rotate()
        {
            Position++;
            if (Position > 26) Position = 1;
            if (EnableNotch && Notches.Contains(Position))
            {
                if (RotateNotch != null) RotateNotch();
            }
        }

        //public void ResetAllSignals()
        //{
        //    for (int i = 0; i < Signaled.Length; i++) Signaled[i] = false;
        //}

        public void SignalRightSide(int number)
        {
            var adjustedNumber = number + (Position - 1);
            if (adjustedNumber > 26) adjustedNumber -= 26;
            //if (!Signaled[adjustedNumber])
           // {
              //  Signaled[adjustedNumber] = true;
                if (SignalOutLeft != null)
                {
                    var output = adjustedNumber + Mapping[adjustedNumber - 1];
                    output -= (Position - 1);
                    if (output > 26) throw new InvalidOperationException(); //I don't think this will ever happen but I want to know if it does.
                    if (output < 1) output += 26;
                    SignalOutLeft(output);
                }
            //}
        }

        public void SignalLeftSide(int number)
        {
            var adjustedNumber = number + (Position - 1);
            if (adjustedNumber > 26) adjustedNumber -= 26;
            var input = adjustedNumber + ReverseMapping[adjustedNumber - 1];
            input -= (Position - 1);
            if (input > 26) throw new InvalidOperationException();
            if (input < 1) input += 26;

           // if (!Signaled[input])
           // {
              //  Signaled[input] = true;
                if (SignalOutRight != null) SignalOutRight(input);
            //}
        }

        public static Wheel CreateWheel(WheelType wheelType)
        {
            if (wheelType == WheelType.I) return new Wheel_I();
            if (wheelType == WheelType.II) return new Wheel_II();
            if (wheelType == WheelType.III) return new Wheel_III();
            throw new ArgumentException();
        }
    }

    internal class Wheel_I : Wheel
    {
        private readonly static int[] Mapping = new int[] { 4, 9, 10, 2, 7, 1, -3, 9, 13, 16, 3, 8, 2, 9, 10, -8, 7, 3, 0, -4, -20, -13, -21, -6, -22, -16 };
        private readonly static int[] Notches = new int[] { 18 };
        public Wheel_I()
            : base(WheelType.I, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_II : Wheel
    {
        private readonly static int[] Mapping = new int[] { 0, 8, 1, 7, 14, 3, 11, 13, 15, -8, 1, -4, 10, 6, -2, -13, 0, -11, 7, -6, -5, 3, -17, -2, -10, -21 };
        private readonly static int[] Notches = new int[] { 6 };
        public Wheel_II()
            : base(WheelType.II, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_III : Wheel
    {
        private readonly static int[] Mapping = new int[] { 1, 2, 3, 4, 5, 6, -4, 8, 9, 10, 13, 10, 13, 0, 10, -11, -8, 5, -12, -19, -10, -9, -2, -5, -8, -11 };
        private readonly static int[] Notches = new int[] { 23 };
        public Wheel_III()
            : base(WheelType.III, Mapping, Notches)
        {
            Position = 1;
        }
    }


    //internal class Wheel_IV : Wheel
    //{
    //    public Wheel_IV()
    //    {
    //        Notches = new[] {'K'};
    //        Mapping = new[] {'E', 'S', 'O', 'V', 'P', 'Z', 'J', 'A', 'Y', 'Q', 'U', 'I', 'R', 'H', 'X', 'L', 'N', 'F', 'T', 'G', 'K', 'D', 'C', 'M', 'W', 'B'};
    //        BuildWiring();
    //    }
    //}

    //internal class Wheel_V : Wheel
    //{
    //    public Wheel_V()
    //    {
    //        Notches = new[] {'A'};
    //        Mapping = new[] {'V', 'Z', 'B', 'R', 'G', 'I', 'T', 'Y', 'U', 'P', 'S', 'D', 'N', 'H', 'L', 'X', 'A', 'W', 'M', 'J', 'Q', 'O', 'F', 'E', 'C', 'K'};
    //        BuildWiring();
    //    }
    //}

    //internal class Wheel_VI : Wheel
    //{
    //    public Wheel_VI()
    //    {
    //        Notches = new[] {'A', 'N'};
    //        Mapping = new[] {'J', 'P', 'G', 'V', 'O', 'U', 'M', 'F', 'Y', 'Q', 'B', 'E', 'N', 'H', 'Z', 'R', 'D', 'K', 'A', 'S', 'X', 'L', 'I', 'C', 'T', 'W'};
    //        BuildWiring();
    //    }
    //}

    //internal class Wheel_VII : Wheel
    //{
    //    public Wheel_VII()
    //    {
    //        Notches = new[] {'A', 'N'};
    //        Mapping = new[] {'N', 'Z', 'J', 'H', 'G', 'R', 'C', 'X', 'M', 'Y', 'S', 'W', 'B', 'O', 'U', 'F', 'A', 'I', 'V', 'L', 'P', 'E', 'K', 'Q', 'D', 'T'};
    //        BuildWiring();
    //    }
    //}

    //class Wheel_VIII : Wheel
    //{
    //    public Wheel_VIII()
    //    {
    //        Notches = new[] { 'A', 'N' };
    //        Mapping = new[] {'F', 'K', 'Q', 'H', 'T', 'L', 'X', 'O', 'C', 'B', 'J', 'S', 'P', 'D', 'Z', 'R', 'A', 'M', 'E', 'W', 'N', 'I', 'U', 'Y', 'G', 'V'};
    //        BuildWiring();
    //    }
    //}

    //class Wheel_Beta : Wheel
    //{
    //    public Wheel_Beta()
    //    {
    //        Notches = new char[] { };
    //        Mapping = new[] {'L', 'E', 'Y', 'J', 'V', 'C', 'N', 'I', 'X', 'W', 'P', 'B', 'Q', 'M', 'D', 'R', 'T', 'A', 'K', 'Z', 'G', 'F', 'U', 'H', 'O', 'S'};
    //        BuildWiring();
    //    }
    //}

    //class Wheel_Gamma : Wheel
    //{
    //    public Wheel_Gamma()
    //    {
    //        Notches = new char[] { };
    //        Mapping = new[] {'F', 'S', 'O', 'K', 'A', 'N', 'U', 'E', 'R', 'H', 'M', 'B', 'T', 'I', 'Y', 'C', 'W', 'L', 'Q', 'P', 'Z', 'X', 'V', 'G', 'J', 'D'};
    //        BuildWiring();
    //    }
    //}
}