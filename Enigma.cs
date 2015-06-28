using System;
using System.Linq;

namespace BombeProto1
{
    internal class Enigma
    {
        public readonly Wheel[] Wheels; //in order left to right.
        public readonly Wheel RightWheel;
        protected readonly Reflector Reflector;

        public char[] CurrentSetting
        {
            get { return Wheels.Select(w => Convert.ToChar(w.Position + 64)).ToArray(); }
        }

        public Enigma(WheelType[] wheelTypes, ReflectorType reflectorType)
        {
            Reflector = Reflector.CreateReflector(reflectorType);
            Wheels = new Wheel[wheelTypes.Length];
            for (int i = 0; i < wheelTypes.Length; i++)
            {
                Wheels[i] = Wheel.CreateWheel(wheelTypes[i]);
            }
            RightWheel = Wheels[Wheels.Length - 1];
            Reflector.SignalOut = Wheels[0].SignalLeftSide;
            Wheels[0].SignalOutLeft = Reflector.Signal;

            for (int i = 1; i < Wheels.Length; i++)
            {
                Wheels[i - 1].SignalOutRight = Wheels[i].SignalLeftSide;
                Wheels[i].SignalOutLeft = Wheels[i - 1].SignalRightSide;
            }
        }

        public char Encode(char p)
        {
            char returnChar = 'A';
            RightWheel.SignalOutRight = i =>
            {
                returnChar = Convert.ToChar(i + 64);
            };
            RightWheel.SignalRightSide(p - 64);
            return returnChar;
        }

        public char RotateAndEncode(char p)
        {
            RightWheel.Rotate();
            //Console.Write("{0} {1} {2} ", WheelLeft.Position, WheelMiddle.Position, WheelRight.Position);
            return Encode(p);
        }

        public void SetWheelPositions(char[] positions)
        {
            SetWheelPositions(positions.Select(p=>(int)p-64).ToArray());
        }

        public void SetWheelPositions(int[] positions)
        {
            if (positions.Length != Wheels.Length) throw new ArgumentException();
            for (int i = 0; i < positions.Length; i++)
            {
                Wheels[i].Position = positions[i];
            }
        }
    }
}