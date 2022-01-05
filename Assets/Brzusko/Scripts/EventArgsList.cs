using System;

namespace Brzusko.Events
{
    public class BackendRequestArgs : EventArgs
    {
        public ServiceType ServiceType { get; set; }
    }

    public class BackendResponseArgs : EventArgs 
    {
        public ServiceType ServiceType { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class BackendPingArgs : EventArgs
    {
        public ServiceType[] SuccessPings { get; set; }
        public ServiceType[] FailedPings { get; set; }
    }
}
