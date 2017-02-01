using System.Collections.Generic;
using System.Diagnostics;
using Kaguwa.Commands.Network.Types;
using Kaguwa.Network;
using Kaguwa.Network.Enums;
using Kaguwa.Network.Types;

namespace Kaguwa.Commands.Network
{
    /// <summary>
    /// A class that provides one static method. This method fetches all
    /// the current Tcp/Udp (v4 and v6) available on the system.
    /// </summary>
    public class Connections
    {
        /// <summary>
        /// Static method that calls the IPHelper wrappers functions to
        /// get the TCP/UDP connections on localhost.
        /// </summary>
        /// <returns>
        /// It returns a list combined of lists of TcpConnections, Tcp6Connections,
        /// UdpConnections and Udp6Connections.
        /// </returns>
        public static IEnumerable<Types.NetworkConnection> GetConnections()
        {
            var connections = new List<Types.NetworkConnection>();
            // Fetch processes in advance to send along to the IPHelpers methods.
            Process[] processes = Process.GetProcesses();

            // Create lists of all the connections for the protocols and verions.
            var tcpConnections = IPHelper.GetTcpConnections(IPVersion.IPv4, processes);
            var tcp6Connections = IPHelper.GetTcpConnections(IPVersion.IPv6, processes);
            var udpConnections = IPHelper.GetUdpConnections(IPVersion.IPv4, processes);
            var udp6Connections = IPHelper.GetUdpConnections(IPVersion.IPv6, processes);

            // Iterate through the results and create a NetworkConnection object
            // to be given back from the Cmdlet.
            foreach (TcpConnection connection in tcpConnections)
            {
                connections.Add(new Types.NetworkConnection
                {
                    Protocol = connection.Protocol.ToString(),
                    LocalAddress = connection.LocalAddress.ToString(),
                    LocalPort = connection.LocalPort,
                    RemoteAddress = connection.RemoteAddress.ToString(),
                    RemotePort = connection.RemotePort,
                    State = connection.State.ToString(),
                    ProcessId = connection.ProcessId,
                    ProcessName = connection.ProcessName
                });
            }

            foreach (TcpConnection connection in tcp6Connections)
            {
                connections.Add(new Types.NetworkConnection
                {
                    Protocol = connection.Protocol.ToString(),
                    LocalAddress = connection.LocalAddress.ToString(),
                    LocalPort = connection.LocalPort,
                    RemoteAddress = connection.RemoteAddress.ToString(),
                    RemotePort = connection.RemotePort,
                    State = connection.State.ToString(),
                    ProcessId = connection.ProcessId,
                    ProcessName = connection.ProcessName
                });
            }

            foreach (UdpConnection connection in udpConnections)
            {
                connections.Add(new Types.NetworkConnection
                {
                    Protocol = connection.Protocol.ToString(),
                    LocalAddress = connection.LocalAddress.ToString(),
                    LocalPort = connection.LocalPort,
                    ProcessId = connection.ProcessId,
                    ProcessName = connection.ProcessName
                });
            }

            foreach (UdpConnection connection in udp6Connections)
            {
                connections.Add(new Types.NetworkConnection
                {
                    Protocol = connection.Protocol.ToString(),
                    LocalAddress = connection.LocalAddress.ToString(),
                    LocalPort = connection.LocalPort,
                    ProcessId = connection.ProcessId,
                    ProcessName = connection.ProcessName
                });
            }

            return connections;
        }
    }
}
