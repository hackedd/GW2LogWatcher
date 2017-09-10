using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GW2LogWatcher.Tests
{
    [TestClass]
    public class DpsReportHandlerTests
    {
        [TestMethod]
        public void TestHandle()
        {
            var testLog = Path.Combine(TestUtils.TestFiles, "20170901-143309.evtc");
            var handler = new DpsReportHandler();
            var e = handler.Handle(testLog);

            Assert.IsTrue(e.Success);
            Assert.IsTrue(e.Output.StartsWith("https://dps.report/"));
            Assert.IsTrue(e.Output.Contains("20170901-143309_golem"));
        }
    }
}
