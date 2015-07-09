using System;
using System.Collections.Generic;
using System.Linq;

namespace BombeProto1
{
    internal class Bombe
    {
        private readonly char input;
        private readonly char entry;
        public readonly List<BombeEnigma> Enigmas;  
        public readonly Dictionary<char, Bus> Buses;
        public char[] CurrentKeys { get; private set; }
        private DiagonalBoard diagonalBoard;
        public bool DiagonalBoardEnabled
        {
            get { return diagonalBoard != null; }
        }

        public Bombe(MapConfiguration mapConfiguration)
            : this(mapConfiguration.MapEntries, mapConfiguration.InputLetter, mapConfiguration.CurrentEntry, mapConfiguration.WheelTypes, mapConfiguration.ReflectorType, mapConfiguration.EnableDiagonalBoard)
        {
        }

        public Bombe(IEnumerable<MapEntry> mapping, char input, char entry, 
                        WheelType[] wheelTypes, ReflectorType reflectorType, bool enableDiagonalBoard)
        {
            this.input = input;
            this.entry = entry;
            Buses = new Dictionary<char, Bus>();
            for (char c = 'A'; c <= 'Z'; c++) Buses.Add(c, new Bus(c));
            if (enableDiagonalBoard)
            {
                diagonalBoard = new DiagonalBoard(Buses);
            }
            Enigmas = new List<BombeEnigma>();

            foreach (var map in mapping)
            {
                var bombeEnigma = new BombeEnigma(wheelTypes, reflectorType, map.StepsAheadOfKey, Buses[map.LeftChar], Buses[map.RightChar]);
                Enigmas.Add(bombeEnigma);
            }
            CurrentKeys = new char[wheelTypes.Length];
        }

        private void IncrementWheels(int wheelNum)
        {
            if (CurrentKeys[wheelNum] == 'Z')
            {
                if (wheelNum > 0) IncrementWheels(wheelNum - 1);
                CurrentKeys[wheelNum] = 'A';
            }
            else
            {
                CurrentKeys[wheelNum]++;
            }
            SetAllWheelPositions();
        }

        private bool CheckBusesAllSignaled()
        {
            return Buses.Values.Where(b => b.IsConnected).Any(bus => bus.AllSignaled);
        }

        private bool RunCheck()
        {
            foreach (var bus in Buses.Values) bus.ResetBus();
            var inputBus = Buses[input];
            inputBus.Signal(entry);
            if (!CheckBusesAllSignaled())
            {
                //success!
                return true;
            }
            return false;
        }

        public bool Run(char[] startingPositions)
        {
            var allMatches = new List<char[]>();
            Console.WriteLine("Starting run with wheel positions: {0} {1} {2}", startingPositions[0], startingPositions[1], startingPositions[2]);
            CurrentKeys = new char[startingPositions.Length];
            startingPositions.CopyTo(CurrentKeys, 0);
            SetAllWheelPositions();
            int matches = 0;
            do
            {
                Console.WriteLine("Checking position {0} {1} {2}", CurrentKeys[0], CurrentKeys[1], CurrentKeys[2]);
                if (RunCheck())
                {
                    Console.WriteLine("************************************** Match!!!");
                    matches++;
                    var copy = new char[CurrentKeys.Length];
                    CurrentKeys.CopyTo(copy, 0);
                    allMatches.Add(copy);
                }
                IncrementWheels(CurrentKeys.Length - 1);
            } while (!DoesStartingPositionMatchCurrentPosition(startingPositions));
            
            foreach (var match in allMatches) Console.WriteLine("{0} {1} {2}", match[0], match[1], match[2]);
            Console.WriteLine("Found {0} matches.", matches);
            return false;
        }

        private bool DoesStartingPositionMatchCurrentPosition(char[] startingPositions)
        {
            if (startingPositions.SequenceEqual(CurrentKeys))
            {
                return true;
            }
            return false;
        }

        private void SetAllWheelPositions()
        {
            foreach (var enigma in Enigmas)
            {
                enigma.SetRotorsBasedOnBombeKey(CurrentKeys);
            }
        }
    }
}
