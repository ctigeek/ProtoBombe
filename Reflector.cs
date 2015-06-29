using System;

namespace BombeProto1
{
    public enum ReflectorType
    {
        A,
        B,
        C,
        B_Thin,
        C_Thin
    }

    public abstract class Reflector
    {
        public readonly ReflectorType ReflectorType;
        public readonly int[] Mapping;
        public Action<int> SignalOut { get; set; }

        protected Reflector(ReflectorType reflectorType, int[] mapping)
        {
            this.ReflectorType = reflectorType;
            this.Mapping = mapping;
        }

        public void Signal(int input)
        {
            var output = Map(input);
            if (SignalOut != null)
            {
                SignalOut(output);
            }
        }

        public int Map(int input)
        {
            return Mapping[input - 1];
        }

        public static Reflector CreateReflector(ReflectorType reflectorType)
        {
            if (reflectorType == ReflectorType.A) return new Reflector_A();
            if (reflectorType == ReflectorType.B) return new Reflector_B();
            if (reflectorType == ReflectorType.C) return new Reflector_C();
            if (reflectorType == ReflectorType.B_Thin) return new Reflector_B_Thin();
            if (reflectorType == ReflectorType.C_Thin) return new Reflector_C_Thin();
            throw new ArgumentException();
        }
    }

    public class Reflector_A : Reflector
    {
        private static int[] mapping = new int[] {5, 10, 13, 26, 1, 12, 25, 24, 22, 2, 23, 6, 3, 18, 17, 21, 15, 14, 20, 19, 16, 9, 11, 8, 7, 4};

        public Reflector_A() : base(ReflectorType.A, mapping)
        {
        }
    }

    public class Reflector_B : Reflector
    {
        private static int[] mapping = new int[] {25, 18, 21, 8, 17, 19, 12, 4, 16, 24, 14, 7, 15, 11, 13, 9, 5, 2, 6, 26, 3, 23, 22, 10, 1, 20};

        public Reflector_B() : base(ReflectorType.B, mapping)
        {
        }
    }

    public class Reflector_C : Reflector
    {
        private static int[] mapping = new int[] {6, 22, 16, 10, 9, 1, 15, 25, 5, 4, 18, 26, 24, 23, 7, 3, 20, 11, 21, 17, 19, 2, 14, 13, 8, 12};

        public Reflector_C()
            : base(ReflectorType.C, mapping)
        {
        }
    }

    public class Reflector_B_Thin : Reflector
    {
        private static int[] mapping = new int[] {5, 14, 11, 17, 1, 21, 25, 23, 10, 9, 3, 15, 16, 2, 12, 13, 4, 24, 26, 22, 6, 20, 8, 18, 7, 19};

        public Reflector_B_Thin()
            : base(ReflectorType.B_Thin, mapping)
        {
        }
    }

    public class Reflector_C_Thin : Reflector
    {
        private static int[] mapping = new int[] {18, 4, 15, 2, 10, 14, 20, 11, 22, 5, 8, 13, 12, 6, 3, 23, 26, 1, 24, 7, 25, 9, 16, 19, 21, 17};

        public Reflector_C_Thin()
            : base(ReflectorType.C_Thin, mapping)
        {
        }
    }
}