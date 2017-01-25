using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Kaguwa.Network.Types;
using Kaguwa.Network;

namespace Kaguwa.Commands.Network
{
    [Cmdlet(VerbsCommon.Get, "NetworkHost")]
    [OutputType(typeof(Host))]
    public class GetNetworkHostCommand : Cmdlet
    {
        // Define parameters.
        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("HostName", "Name", "IPAddress", "Address")]
        [AllowNull()]
        [AllowEmptyString()]
        public string Host
        {
            get { return host; }
            set { host = value; }
        }
        private string host;

        Host _host;

        protected override void ProcessRecord()
        {
            // If null or empty, set to  the local computer name.
            if (string.IsNullOrEmpty(host))
            {
                host = Environment.MachineName;
            }

            // Get the entry and write it to output.
            _host = Dns.GetHost(host);
            WriteObject(_host);
        }
    }
}
