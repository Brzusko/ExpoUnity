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
        public long Status { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class BackendPingArgs : EventArgs
    {
        public ServiceType[] SuccessPings { get; set; }
        public ServiceType[] FailedPings { get; set; }
        public bool CriticalServicesAreDead { get; set; }
    }

    public class BasicMassage : EventArgs
    {
        public string Message { get; set; }
    }
}
