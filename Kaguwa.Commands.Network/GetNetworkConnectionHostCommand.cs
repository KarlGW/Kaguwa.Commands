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
        #region Parameters

        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("IPAddress", "Address", "RemoteAddress")]
        [AllowNull()]
        [AllowEmptyString()]
        public string[] Host
        {
            get { return host; }
            set { host = value; }
        }
        private string[] host;

        #endregion Parameters

        List<Host> _host = new List<Host>();

        #region CmdletOverrides

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
                foreach (string h in host)
                {
                    if (!string.IsNullOrEmpty(h))
                    {
                        string entry = null;
                        Regex rgx = new Regex(@"^(http|https)://");
                        entry = rgx.Replace(h, string.Empty);
                        _host.Add(Dns.GetHost(entry));
                    }
                }
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            WriteObject(_host);
        }

        #endregion CmdletOverrides
    }
}