using System;
using System.Collections.Generic;
using System.Linq;

namespace BombeProto1
{
    internal class Bus
    {
        public readonly char Letter;
        public readonly Dictionary<char, bool> Signaled;
        public event EventHandler<char> SignalEvent;

        public bool IsConnected
        {
            get { return SignalEvent != null; }
        }

        public bool AllSignaled
        {
            get
            {
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    if (!Signaled[c]) return false;
                }
                return true;
            }
        }

        public Bus(char letter)
        {
            Letter = letter;
            Signaled = new Dictionary<char, bool>();
            for (char c = 'A'; c <= 'Z'; c++) Signaled.Add(c, false);
        }

        public void ResetBus()
        {
            for (char c = 'A'; c <= 'Z'; c++) Signaled[c] = false;
        }

        public void Signal(char c)
        {
            if (!Signaled[c])
            {
                Signaled[c] = true;
                OnSignalEvent(c);
            }
        }

        protected void OnSignalEvent(char c)
        {
            var handler = SignalEvent;
            if (handler != null)
            {
                handler(this, c);
            }
        }
    }
}