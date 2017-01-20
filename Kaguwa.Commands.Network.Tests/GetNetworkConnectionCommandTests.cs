using System.Management.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaguwa.Commands.Network.Tests
{
    [TestClass]
    public class GetNetworkConnectionCommandTests
    {
        [TestMethod]
        public void ShouldCreateCmdlet()
        {
            // Create an instance of the Cmdlet.
            GetNetworkConnectionCommand cmd = new GetNetworkConnectionCommand();

            // Check if it is a Cmdlet.
            Assert.IsTrue(cmd is Cmdlet);
        }
    }
}
