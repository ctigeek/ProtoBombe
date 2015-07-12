using System; 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace BombeProto1
{
    internal class Enigma
    {
        public readonly Wheel[] Wheels; //in order left to right.
        public readonly Wheel RightWheel;
        public readonly ReadOnlyDictionary<char,char> PlugboardDictionary;
        protected readonly Reflector Reflector;

        public char[] CurrentSetting
        {
            get { return Wheels.Select(w => Convert.ToChar(w.Position + 64)).ToArray(); }
        }

        public Enigma(WheelType[] wheelTypes, ReflectorType reflectorType, IList<Tuple<char,char>> plugboardSettings = null)
        {
            Reflector = Reflector.CreateReflector(reflectorType);
            Wheels = new Wheel[wheelTypes.Length];
            ConfigureWheels(wheelTypes);
            RightWheel = Wheels[Wheels.Length - 1];
            var tempDict = new Dictionary<char, char>();
            if (plugboardSettings != null)
            {
                ValidatePlugboardSettings(plugboardSettings);
                foreach (var tuple in plugboardSettings)
                {
                    tempDict.Add(tuple.Item1, tuple.Item2);
                    tempDict.Add(tuple.Item2, tuple.Item1);
                }
            }
            PlugboardDictionary = new ReadOnlyDictionary<char, char>(tempDict);
        }

        private void ConfigureWheels(WheelType[] wheelTypes)
        {
            for (int i = 0; i < wheelTypes.Length; i++)
            {
                Wheels[i] = Wheel.CreateWheel(wheelTypes[i]);
                if (i == 0)
                {
                    Reflector.SignalOut = Wheels[0].SignalLeftSide;
                    Wheels[0].SignalOutLeft = Reflector.Signal;
                }
                else
                {
                    Wheels[i - 1].SignalOutRight = Wheels[i].SignalLeftSide;
                    Wheels[i].SignalOutLeft = Wheels[i - 1].SignalRightSide;
                }
            }
        }

        private void ValidatePlugboardSettings(IList<Tuple<char, char>> plugboardSettings)
        {
            var characters = new List<char>();
            foreach (var tuple in plugboardSettings)
            {
                if (characters.Contains(tuple.Item1))
                {
                    throw new ArgumentException("The plugboard letter "+tuple.Item1+" was specified more than once.");
                }
                if (characters.Contains(tuple.Item1))
                {
                    throw new ArgumentException("The plugboard letter " + tuple.Item2 + " was specified more than once.");
                }
            }
        }

        public char Encode(char p)
        {
            char returnChar = 'A';
            RightWheel.SignalOutRight = i =>
            {
                returnChar = Convert.ToChar(i + 64);
                if (returnChar <'A' || returnChar > 'Z') throw new InvalidDataException();
            };
            RightWheel.SignalRightSide(translateWithPlugboard(p) - 64);
            return translateWithPlugboard(returnChar);
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

        private char translateWithPlugboard(char c)
        {
            if (PlugboardDictionary.ContainsKey(c))
            {
                return PlugboardDictionary[c];
            }
            return c;
        }
    }
}