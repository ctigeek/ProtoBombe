using System.Collections.Generic;

namespace BombeProto1
{
    internal class DiagonalBoard
    {
        public readonly Dictionary<char, Bus> Buses;

        public DiagonalBoard(Dictionary<char, Bus> buses)
        {
            this.Buses = buses;

            foreach (var A_bus in buses.Values)
            {
                A_bus.SignalEvent += (sender, c) =>
                {
                    var bus = (Bus) sender;
                    if (c != bus.Letter)
                    {
                        buses[c].Signal(bus.Letter);
                    }
                };
            }
        }
    }
}