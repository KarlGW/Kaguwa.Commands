using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation;

namespace Kaguwa.Commands.Network.Tests
{
    [TestClass]
    public class GetNetworkHostCommandTests
    {
        [TestMethod]
        public void ShouldCreateCmdlet()
        {
            // Create an instance of the Cmdlet.
            GetNetworkHostCommand cmd = new GetNetworkHostCommand();

            // Check if it is a Cmdlet.
            Assert.IsTrue(cmd is Cmdlet);
        }
    }
}
