using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace BombeProto1
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] {"Z", "Z", "Z"};
            try
            {
                var mapConfigFile = ConfigurationManager.AppSettings["mapConfigFile"];
                var mapConfiguration = LoadMapConfigFromFile(mapConfigFile);
                var bombe = new Bombe(mapConfiguration);
                if (bombe.CurrentKeys.Length > args.Length) throw new ArgumentException("incorrect number of arguments.");
                var startingPositions = new char[] { args[0][0], args[1][0], args[2][0]};
                bombe.Run(startingPositions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

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

            //var map13 = new MapEntry(13, 'A', 'S');
            //var map7 = new MapEntry(7, 'S', 'V');
            //var map16 = new MapEntry(16, 'E', 'V');
            //var map2 = new MapEntry(2, 'E', 'N');
            //var map11 = new MapEntry(11, 'E', 'U');
            //var map5 = new MapEntry(5, 'E', 'G');
            //var map15 = new MapEntry(15, 'G', 'L');
            //var map6 = new MapEntry(6, 'G', 'R');
            //var map12 = new MapEntry(12, 'G', 'R');
            //var map14 = new MapEntry(14, 'A', 'R');
            //var map9 = new MapEntry(9, 'R', 'Z');
            //var map10 = new MapEntry(10, 'H', 'Z');

            //var mapping = new[] {map13, map7, map16, map2, map11, map5, map15, map6, map12, map14, map9, map10};
            //var wheelTypes = new[] {WheelType.II, WheelType.V, WheelType.III};
            //var bombe = new Bombe(mapping, 'G', 'A', wheelTypes, ReflectorType.B, true);
            //var startingPosition = new[] {'Z', 'Z', 'A'};
            //bombe.Run(startingPosition);

            Console.ReadLine();
        }

        static MapConfiguration LoadMapConfigFromFile(string mapConfigFile)
        {
            var fullPath = mapConfigFile;
            if (!File.Exists(mapConfigFile))
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                var executingPath = executingAssembly.Location.Replace(executingAssembly.GetName().Name + ".exe", "");
                fullPath = Path.Combine(executingPath, mapConfigFile);    
                if (!System.IO.File.Exists(fullPath)) throw new ArgumentException("Map config file '" + fullPath + "' does not exist.");
            }
            
            string filebody;
            using (var reader = new StreamReader(fullPath))
            {
                filebody = reader.ReadToEnd();
            }
            var jobject = JObject.Parse(filebody);
            return jobject.ToObject<MapConfiguration>();
        }
    }
}
