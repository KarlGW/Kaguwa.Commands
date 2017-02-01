using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using Kaguwa.Commands.Network.Types;

namespace Kaguwa.Commands.Network
{
    [Cmdlet(VerbsCommon.Get, "NetworkConnection",
        DefaultParameterSetName = "ProcessName")]
    [OutputType(typeof(NetworkConnection))]
    public class GetNetworkConnectionCommand : Cmdlet
    {
        #region Parameters

        [Parameter(
            Position = 0,
            ParameterSetName = "ProcessName",
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The name of one or more proccess in network connection to filter by.")]
        [Alias("Name", "Process")]
        public string[] ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }
        private string[] processName;

        [Parameter(
            Position = 0,
            ParameterSetName = "ProcessId",
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The ID of one or more proccess in network connection to filter by.")]
        [Alias("Id", "PID")]
        public int[] ProcessId
        {
            get { return processId; }
            set { processId = value; }
        }
        private int[] processId;

        [Parameter(
            Position = 1,
            HelpMessage = "The state of connections to filter by.")]
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

        #endregion Parameters

        private IEnumerable<NetworkConnection> _connections;

        #region CmdletOverrides
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
                connections = ConnectionByProcessName(connections, ProcessName);                
            }
            else if (ProcessId != null)
            {
                connections = ConnectionByProcessId(connections, ProcessId);
            }

            // Check if state was specified.
            if (State != null)
            {
                connections = ConnectionByState(connections, State);
            }

            // Write the NetworkConnection objects.
            connections.ToList().ForEach(WriteObject);
        }

        #endregion CmdletOverrides

        #region Helper Methods

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

        /// <summary>
        /// Helper method to filter connections from GetConnections().
        /// </summary>
        /// <param name="connections">IEnumerable containing TCP/UDP connections.</param>
        /// <param name="ProcessName">Parameter ProcessName</param>
        /// <returns>
        /// It returns an IEnumerable containing filtered TCP/UDP connections.
        /// </returns>
        private static IEnumerable<NetworkConnection> ConnectionByProcessName(IEnumerable<NetworkConnection> connections, string[] ProcessName)
        {
            // Define wildcard
            List<NetworkConnection> filteredConnections = new List<NetworkConnection>();

            foreach (string name in ProcessName)
            {

                WildcardOptions wildcardOptions = WildcardOptions.IgnoreCase |
                    WildcardOptions.Compiled;

                WildcardPattern wildcard = new WildcardPattern(name, wildcardOptions);

                foreach (NetworkConnection connection in connections)
                {

                    if (wildcard.IsMatch(connection.ProcessName))
                    {
                        filteredConnections.Add(connection);
                    }
                }
            }

            connections = null;
            connections = filteredConnections;

            return connections;
        }

        /// <summary>
        /// Helper method to filter connections from GetConnections().
        /// </summary>
        /// <param name="connections">IEnumerable containing TCP/UDP connections.</param>
        /// <param name="ProcessId">Parameter ProcessId</param>
        /// <returns>
        /// It returns an IEnumerable containing filtered TCP/UDP connections.
        /// </returns>
        private static IEnumerable<NetworkConnection> ConnectionByProcessId(IEnumerable<NetworkConnection> connections, int[] ProcessId)
        {
            connections = connections.Where(connection => ProcessId.Contains(connection.ProcessId));

            return connections;
        }

        /// <summary>
        /// Helper method to filter connections from GetConnections().
        /// </summary>
        /// <param name="connections">IEnumerable containing TCP/UDP connections.</param>
        /// <param name="State">Parameter ProcessName</param>
        /// <returns>
        /// It returns an IEnumerable containing filtered TCP/UDP connections.
        /// </returns>
        private static IEnumerable<NetworkConnection> ConnectionByState(IEnumerable<NetworkConnection> connections, string State)
        {
            connections = connections.Where(connection => connection.State == State);

            return connections;
        }

        #endregion Helper Methods
    }
}