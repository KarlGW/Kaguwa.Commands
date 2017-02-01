using System.Management.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaguwa.Commands.Network.Tests
{
    [TestClass]
    public class GetNetworkConnectionHostCommandTests
    {
        [TestMethod]
        public void ShouldCreateCmdlet()
        {
            // Create an instance of the Cmdlet.
            GetNetworkConnectionHostCommand cmd = new GetNetworkConnectionHostCommand();

            // Check if it is a Cmdlet.
            Assert.IsTrue(cmd is Cmdlet);
        }
    }
}
