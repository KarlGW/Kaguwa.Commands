using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using System.Text.RegularExpressions;
using Kaguwa.Commands.Network.Types;

namespace Kaguwa.Commands.Network
{
    [Cmdlet(VerbsCommon.Get, "NetworkConnection")]
    [OutputType(typeof(NetworkConnection))]
    public class GetNetworkConnectionCommand : Cmdlet
    {
        // Define parameters.
        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("Name", "Process")]
        public string[] ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }
        private string[] processName;

        [Parameter(
            ValueFromPipelineByPropertyName = true)]
        [Alias("Id", "PID")]
        public int[] ProcessId
        {
            get { return processId; }
            set { processId = value; }
        }
        private int[] processId;

        [Parameter(Position = 1)]
        [Alias("Connection")]
        [ValidateSet(
            "Established",
            "Listening",
            "Time_Wait",
            "Closing"
            )]
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string state;

        private IEnumerable<NetworkConnection> _connections;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            // Fetch the connections.
            _connections = GetConnections();
        }

        protected override void ProcessRecord()
        {
            var connections = _connections;

            // Handle the parameters.
            // Either use ProcessName or ProcessId.
            if (ProcessName != null)
            {
                connections = connections.Where(connection => connection.ProcessName != null &&
                                                Regex.IsMatch(connection.ProcessName,
                                                string.Format("^(?:{0})", string.Join("|", ProcessName))));
            }
            else if (ProcessId != null)
            {
                connections = connections.Where(connection => ProcessId.Contains(connection.ProcessId));
            }

            // Check if state was specified.
            if (State != null)
            {
                connections = connections.Where(connection => connection.State == State);
            }

            // Write the NetworkConnection objects.
            connections.ToList().ForEach(WriteObject);
        }

        /// <summary>
        /// Static internal method to get the connections in use by the localhost.
        /// </summary>
        /// <returns>
        /// It returns an IEnumerable containing all the TCP/UDP connections.
        /// </returns>
        private static IEnumerable<NetworkConnection> GetConnections()
        {
            // Call the static method GetConnections from the Connections class.
            IEnumerable<NetworkConnection> connections = Connections.GetConnections();

            return connections;
        }
    }
}
