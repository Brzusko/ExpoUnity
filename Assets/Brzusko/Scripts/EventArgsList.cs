using System;

namespace Brzusko.Events
{
    public class BackendRequestArgs : EventArgs
    {
        public string ServiceName { get; set; }
    }

    public class BackendResponseArgs : EventArgs 
    {
        public string ServiceName { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class BackendPingArgs : EventArgs
    {
        public BackendResponseArgs[] SuccessPings { get; set; }
        public BackendResponseArgs[] FailedPings { get; set; }
    }
}
