using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BombeProto1
{
    internal class Bombe
    {
        private readonly char input;
        private readonly char entry;
        public readonly List<BombeEnigma> Enigmas;  
        public readonly Dictionary<char, Bus> Buses;
        public char[] CurrentKeys { get; private set; }

        public Bombe(IEnumerable<MapEntry> mapping, char input, char entry, 
                        WheelType[] wheelTypes, ReflectorType reflectorType)
        {
            this.input = input;
            this.entry = entry;
            Buses = new Dictionary<char, Bus>();
            for (char c = 'A'; c <= 'Z'; c++) Buses.Add(c, new Bus(c));

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
                }
                IncrementWheels(CurrentKeys.Length - 1);
            } while (!DoesStartingPositionMatchCurrentPosition(startingPositions));
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

        //private bool ComputeAndCheck(char c)
        //{
        //    //first reset all the enigmas
        //    foreach (var enigma in Enigmas)
        //    {
        //        enigma.computed = false;
        //        enigma.HasBeenChecked = false;
        //    }
        //    var starting = Enigmas.First(e => e.StartingEnigma);
        //    starting.Compute(c);

        //    //Now check everything for consistency....
        //    //1. make sure input of each matches output of next....
        //    if (!InputMatchesOutputAdInfinitum(starting)) return false;

        //    //2. make sure each letter only appears once, except where it's allowed.
        //    foreach (var enigma in Enigmas) enigma.HasBeenChecked = false;
        //    var letterCount = new Dictionary<char, int>();
        //    CountOutputLetters(starting, letterCount);

        //    foreach (var letter in letterCount.Where(lc => lc.Value > 1))
        //    {
        //        var enigmaCounts = Enigmas.Where(e => e.outputLetter == letter.Key).ToArray();
        //        //if the number of enigmas with that output letter != the number of times that output letter was found....
        //        if (enigmaCounts.Length != letter.Value) return false;
        //        //if the number of enigmas with that output letter != the number of others that are suppose to have that same output letter.....
        //        if (enigmaCounts.Select(e => e.SameOutputLetters.Length).Any(len => len + 1 != enigmaCounts.Length)) return false;

        //        if (enigmaCounts.Any(enigma => !enigma.SameOutputLetters.All(enigmaCounts.Contains)))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //private bool InputMatchesOutputAdInfinitum(BombeEnigma enigma)
        //{
        //    if (!enigma.HasBeenChecked)
        //    {
        //        enigma.HasBeenChecked = true;
        //        if (enigma.OutputConnections.Select(e => e.inputLetter).Any(c => c != enigma.outputLetter)) return false;

        //        foreach (var e in enigma.OutputConnections)
        //        {
        //            if (!InputMatchesOutputAdInfinitum(e)) return false;
        //        }
        //    }
        //    return true;
        //}

        //private void CountOutputLetters(BombeEnigma enigma,Dictionary<char, int> letterCount)
        //{
        //    foreach (var outputConnection in enigma.OutputConnections)
        //    {
        //        if (!outputConnection.HasBeenChecked)
        //        {
        //            outputConnection.HasBeenChecked = true;
        //            if (letterCount.ContainsKey(outputConnection.outputLetter))
        //            {
        //                letterCount[outputConnection.outputLetter]++;
        //            }
        //            else
        //            {
        //                letterCount.Add(outputConnection.outputLetter, 1);
        //            }
        //            CountOutputLetters(outputConnection, letterCount);
        //        }
        //    }
        //}

        //private char EncodeWithAllEnigmas(char c)
        //{
        //    var output = c;
        //    foreach (var enigma in Enigmas)
        //    {
        //        output = enigma.Encode(output);
        //    }
        //    return output;
        //}
    }
}
