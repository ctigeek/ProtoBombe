using System;
using System.Linq;

namespace BombeProto1
{
    internal class BombeEnigma : Enigma
    {
        public readonly int StepsAheadOfKey;
        public readonly Bus LeftBus;
        public readonly Bus RightBus;

        public BombeEnigma(WheelType[] wheelTypes, ReflectorType reflectorType, 
                                int stepsAhead, Bus leftBus, Bus rightBus)
            : base(wheelTypes, reflectorType)
        {
            this.StepsAheadOfKey = stepsAhead;
            foreach (var wheel in Wheels)
            {
                wheel.EnableNotch = false;
            }
            this.LeftBus = leftBus;
            LeftBus.EnigmaConnected = true;
            LeftBus.SignalEvent += LeftBus_SignalEvent;
            this.RightBus = rightBus;
            RightBus.EnigmaConnected = true;
            RightBus.SignalEvent += RightBus_SignalEvent;
        }

        void RightBus_SignalEvent(object sender, char e)
        {
            SignalInRight(e);
        }

        void LeftBus_SignalEvent(object sender, char e)
        {
            SignalInLeft(e);
        }

        public char SignalInLeft(char c)
        {
            var response = Signal(c);
            RightBus.Signal(response);
            return response;
        }

        public char SignalInRight(char c)
        {
            var response = Signal(c);
            LeftBus.Signal(response);
            return response;
        }

        private char Signal(char c)
        {
            if (c < 'A' || c > 'Z') throw new ArgumentException();
            var response = base.Encode(c);
            return response;
        }

        public void SetRotorsBasedOnBombeKey(char[] bombeKey)
        {
            var positions = bombeKey.Select(c => (int) c - 64).ToArray();
            positions[positions.Length - 1] += StepsAheadOfKey;
            if (positions[positions.Length - 1] > 26) positions[positions.Length - 1] -= 26;
            base.SetWheelPositions(positions);
        }
    }
}