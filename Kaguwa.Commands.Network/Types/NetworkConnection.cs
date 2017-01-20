using System.Net;

namespace Kaguwa.Commands.Network.Types
{
    /// <summary>
    /// The representation of the NetworkConnection that
    /// will be used by the Get-NetworkConnection Cmdlet.
    /// </summary>
    public class NetworkConnection
    {
        public string Protocol { get; set; }
        public IPAddress LocalAddress { get; set; }
        public int LocalPort { get; set; }
        public IPAddress RemoteAddress { get; set; }
        public int? RemotePort { get; set; }
        public string State { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }
}
