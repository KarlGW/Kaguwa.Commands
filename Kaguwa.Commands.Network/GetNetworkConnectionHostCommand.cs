using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Kaguwa.Network.Types;
using Kaguwa.Network;

namespace Kaguwa.Commands.Network
{
    [Cmdlet(VerbsCommon.Get, "NetworkConnectionHost")]
    [OutputType(typeof(Host))]
    public class GetNetworkConnectionHostCommand : Cmdlet
    {
        // Define parameters.
        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("HostName", "IPAddress", "Address", "RemoteAddress")]
        [AllowNull()]
        [AllowEmptyString()]
        public string[] Host
        {
            get { return host; }
            set { host = value; }
        }
        private string[] host;

        List<Host> _host = new List<Host>();

        protected override void ProcessRecord()
        {
            // If null or empty, set to  the local computer name.
            if (host == null || host.Length == 0)
            {
                var localhost = Environment.MachineName;
                _host.Add(Dns.GetHost(localhost));
            }
            else
            {
                for(int i = 0; i < host.Length; i++)
                {
                    // Strip protocol from Url.
                    if (!string.IsNullOrEmpty(host[i]))
                    {
                        Regex rgx = new Regex(@"^(http|https)://");
                        host[i] = rgx.Replace(host[i], string.Empty);
                        _host.Add(Dns.GetHost(host[i]));
                    }
                }
            }

            WriteObject(_host);
        }
    }
}