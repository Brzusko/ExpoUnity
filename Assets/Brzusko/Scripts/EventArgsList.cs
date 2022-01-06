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

    [Serializable]
    public class LoginResponse : EventArgs
    {
        public string RefreshKey { get; set; }
        public string AccessKey { get; set; }
        public bool WasSuccessful { get; set; }
    }
}
