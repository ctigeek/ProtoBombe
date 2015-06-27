using System;

namespace BombeProto1
{
    public enum ReflectorType
    {
        A,
        B
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
            throw new ArgumentException();
        }
    }

    public class Reflector_A : Reflector
    {
        //EJMZALYXVBWFCRQUONTSPIKHGD
        private static int[] mapping = new int[] {5, 10, 13, 26, 1, 12, 25, 24, 22, 2, 23, 6, 3, 18, 17, 21, 15, 14, 20, 19, 16, 9, 11, 8, 7, 4};

        public Reflector_A() : base(ReflectorType.A, mapping)
        {
        }
    }

    public class Reflector_B : Reflector
    {
        //YRUHQSLDPXNGOKMIEBFZCWVJAT
        private static int[] mapping = new int[] {25, 18, 21, 8, 17, 19, 12, 4, 16, 24, 14, 7, 15, 11, 13, 9, 5, 2, 6, 26, 3, 23, 22, 10, 1, 20};

        public Reflector_B() : base(ReflectorType.B, mapping)
        {
        }
    }
}