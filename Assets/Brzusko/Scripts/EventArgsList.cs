using System;

namespace Brzusko.Events
{
    public class BackendEventArgs : EventArgs
    {
        public enum State
        {
            SentPing,
            RecivedPong,
        }

        public string ServiceName { get; set; }
    }
}
