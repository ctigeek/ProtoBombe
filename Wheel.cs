using System;
using System.Linq;

namespace BombeProto1
{
    public enum WheelType
    {
        I,
        II,
        III,
        IV,
        V,
        VI,
        VII,
        VIII,
        Beta,
        Gamma
    }

    internal abstract class Wheel
    {
        public readonly WheelType WheelType;
        private int[] Notches { get; set; }
        private int[] Mapping { get; set; }
        private int[] ReverseMapping { get; set; }
        public int Position { get; set; }
        public bool EnableNotch { get; set; }
        public Action<int> SignalOutRight { get; set; }
        public Action<int> SignalOutLeft { get; set; }
        public Action RotateNotch { get; set; }

        protected Wheel(WheelType wheelType, int[] map, int[] notches)
        {
            this.WheelType = wheelType;
            EnableNotch = true;
            if (map.Length != 26) throw new ArgumentException();
            Mapping = map;
            Notches = notches;
            ReverseMapping = new int[26];
            for (int i = 0; i < map.Length; i++)
            {
                var reverseNum = i + map[i];
                if (reverseNum < 0 || reverseNum > 25) throw new InvalidOperationException();
                ReverseMapping[reverseNum] = -map[i];
            }
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

        public void SignalRightSide(int number)
        {
            var adjustedNumber = number + (Position - 1);
            while (adjustedNumber > 26) adjustedNumber -= 26;
            if (SignalOutLeft != null)
            {
                var output = adjustedNumber + Mapping[adjustedNumber - 1];
                output -= (Position - 1);
                if (output > 26) throw new InvalidOperationException(); //I don't think this will ever happen but I want to know if it does.
                while (output < 1) output += 26;
                SignalOutLeft(output);
            }
        }

        public void SignalLeftSide(int number)
        {
            var adjustedNumber = number + (Position - 1);
            while (adjustedNumber > 26) adjustedNumber -= 26;
            var input = adjustedNumber + ReverseMapping[adjustedNumber - 1];
            input -= (Position - 1);
            if (input > 26) throw new InvalidOperationException();
            while (input < 1) input += 26;

            if (SignalOutRight != null) SignalOutRight(input);
        }

        public static Wheel CreateWheel(WheelType wheelType)
        {
            if (wheelType == WheelType.I) return new Wheel_I();
            if (wheelType == WheelType.II) return new Wheel_II();
            if (wheelType == WheelType.III) return new Wheel_III();
            if (wheelType == WheelType.IV) return new Wheel_IV();
            if (wheelType == WheelType.V) return new Wheel_V();
            if (wheelType == WheelType.VI) return new Wheel_VI();
            if (wheelType == WheelType.VII) return new Wheel_VII();
            if (wheelType == WheelType.VIII) return new Wheel_VIII();
            if (wheelType == WheelType.Beta) return new Wheel_Beta();
            if (wheelType == WheelType.Gamma) return new Wheel_Gamma();
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

    internal class Wheel_IV : Wheel
    {
        private static readonly int[] Mapping = new int[] {4, 17, 12, 18, 11, 20, 3, -7, 16, 7, 10, -3, 5, -6, 9, -4, -3, -12, 1, -13, -10, -18, -20, -11, -2, -24};
        private readonly static int[] Notches = new int[] { 11 };
        public Wheel_IV()
            : base(WheelType.IV, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_V : Wheel
    {
        private static readonly int[] Mapping = new int[] {21, 24, -1, 14, 2, 3, 13, 17, 12, 6, 8, -8, 1, -6, -3, 8, -16, 5, -6, -10, -4, -7, -17, -19, -22, -15};
        private readonly static int[] Notches = new int[] { 1 };
        public Wheel_V()
            : base(WheelType.V, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_VI : Wheel
    {
        private static readonly int[] Mapping = new int[] {9, 14, 4, 18, 10, 15, 6, -2, 16, 7, -9, -7, 1, -6, 11, 2, -13, -7, -18, -1, 3, -10, -14, -21, -5, -3};
        private readonly static int[] Notches = new int[] { 1, 14 };
        public Wheel_VI()
            : base(WheelType.VI, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_VII : Wheel
    {
        private static readonly int[] Mapping = new int[] {13, 24, 7, 4, 2, 12, -4, 16, 4, 15, 8, 11, -11, 1, 6, -10, -16, -9, 3, -8, -5, -17, -12, -7, -21, -6};
        private readonly static int[] Notches = new int[] { 1, 14 };
        public Wheel_VII()
            : base(WheelType.VII, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_VIII : Wheel
    {
        private static readonly int[] Mapping = new int[] {5, 9, 14, 4, 15, 6, 17, 7, -6, -8, -1, 7, 3, -10, 11, 2, -16, -5, -14, 3, -7, -13, -2, 1, -18, -4};
        private readonly static int[] Notches = new int[] { 1, 14 };
        public Wheel_VIII()
            : base(WheelType.VIII, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_Beta : Wheel
    {
        private static readonly int[] Mapping = new int[] {11, 3, 22, 6, 17, -3, 7, 1, 15, 13, 5, -10, 4, -1, -11, 2, 3, -17, -8, 6, -14, -16, -2, -16, -10, -7};
        private readonly static int[] Notches = new int[] {  };
        public Wheel_Beta()
            : base(WheelType.Beta, Mapping, Notches)
        {
            Position = 1;
        }
    }

    internal class Wheel_Gamma : Wheel
    {
        private static readonly int[] Mapping = new int[] {5, 17, 12, 7, -4, 8, 14, -3, 9, -2, 2, -10, 7, -5, 10, -13, 6, -6, -2, -4, 5, 2, -1, -17, -15, -22};
        private readonly static int[] Notches = new int[] { };
        public Wheel_Gamma()
            : base(WheelType.Gamma, Mapping, Notches)
        {
            Position = 1;
        }
    }
}