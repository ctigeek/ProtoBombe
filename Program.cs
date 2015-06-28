using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombeProto1
{
    class Program
    {
        static void Main(string[] args)
        {
            //13 A-->S  
            // 7 S-->V
            //16 V-->E
                    // 2 E-->N
                    //11 E-->U
            // 5 E-->G
                    //15 G-->L
            // 6 G-->R
                    //12 G-->R
            //14 R-->A
                    // 9 R-->Z
                    //10 Z-->H

            var map13 = new MapEntry(13, 'A', 'S');
            var map7 = new MapEntry(7, 'S', 'V');
            var map16 = new MapEntry(16, 'E', 'V');
            var map2 = new MapEntry(2, 'E', 'N');
            var map11 = new MapEntry(11, 'E', 'U');
            var map5 = new MapEntry(5, 'E', 'G');
            var map15 = new MapEntry(15, 'G', 'L');
            var map6 = new MapEntry(6, 'G', 'R');
            var map12 = new MapEntry(12, 'G', 'R');
            var map14 = new MapEntry(14, 'A', 'R');
            var map9 = new MapEntry(9, 'R', 'Z');
            var map10 = new MapEntry(10, 'H', 'Z');

            var mapping = new[] {map13, map7, map16, map2, map11, map5, map15, map6, map12, map14, map9, map10};
            var wheelTypes = new[] {WheelType.I, WheelType.II, WheelType.III};
            var bombe = new Bombe(mapping, 'G', 'A', wheelTypes, ReflectorType.B);
            var startingPosition = new[] {'Z', 'Z', 'A'};
            bombe.Run(startingPosition);


            //var enigma13 = new BombeEnigma(13) {StartingEnigma = true , MappedOutputLetter = 'S'};

            //var enigma7 = new BombeEnigma(7) {MappedOutputLetter = 'V'};
            //var enigma16 = new BombeEnigma(16) {MappedOutputLetter = 'E'};
            //var enigma2 = new BombeEnigma(2) {MappedOutputLetter = 'N'};
            //var enigma11 = new BombeEnigma(11) {MappedOutputLetter = 'U'};

            //var enigma5 = new BombeEnigma(5) {MappedOutputLetter = 'G'};
            //var enigma15 = new BombeEnigma(15){MappedOutputLetter = 'L'};

            //var enigma6 = new BombeEnigma(6) { MappedOutputLetter = 'R'};
            //var enigma12 = new BombeEnigma(12) {MappedOutputLetter = 'R'};

            //var enigma14 = new BombeEnigma(14) {MappedOutputLetter = 'A'};
            //var enigma9 = new BombeEnigma(9) {MappedOutputLetter = 'Z'};
            //var enigma10 = new BombeEnigma(10) {MappedOutputLetter = 'H'};

            //enigma13.OutputConnections = new[] {enigma7};
            //enigma7.OutputConnections = new[] {enigma16};
            //enigma16.OutputConnections = new[] {enigma2, enigma11, enigma5};
            //enigma5.OutputConnections = new[] {enigma15, enigma6, enigma12};
            //enigma6.OutputConnections = new[] {enigma14, enigma9};
            //enigma12.OutputConnections = new[] {enigma14, enigma9};
            //enigma9.OutputConnections = new[] {enigma10};
            //enigma14.OutputConnections = new[] {enigma13};

            //enigma6.SameOutputLetters = new[] {enigma12};
            //enigma12.SameOutputLetters = new[] {enigma6};

            //var bombes = new[] {enigma13, enigma7, enigma16, enigma2, enigma11, enigma5, enigma15, enigma6, enigma12, enigma14, enigma9, enigma10};
            //var bombe = new Bombe(bombes);

            //if (bombe.Run('Z', 'Z', 'Z'))
            //{
            //    Console.WriteLine("Found!  {0}{1}{2}  {3}", bombe.CurrentKeyLeft, bombe.CurrentKeyMiddle, bombe.CurrentKeyRight, bombe.CurrentMatchingChar);
            //}

            //var enigma = new Enigma();
            //enigma.WheelRight = new Wheel_III();
            //enigma.WheelMiddle = new Wheel_II();
            //enigma.WheelLeft = new Wheel_I();
            //enigma.WheelMiddle.WheelToTheLeft = enigma.WheelLeft;
            //enigma.WheelRight.WheelToTheLeft = enigma.WheelMiddle;
            //enigma.Reflector = new Reflector_B();

            //char[] output = new char[30];
            //char c = 'A';
            //for (int i = 0; i < 30; i++)
            //{
            //    output[i] = enigma.RotateAndEncode(c);
            //    Console.WriteLine(" {0}", output[i]);
            //}

            Console.ReadLine();
        }
    }
}
