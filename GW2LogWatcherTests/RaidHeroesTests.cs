using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GW2LogWatcher.Tests
{
    [TestClass]
    public class RaidHeroesHandlerTests
    {
        private string testFiles;

        [TestInitialize]
        public void TestInitialize()
        {
            testFiles = TestUtils.TestFiles;
            RaidHeroesHandler.RaidHeroesLocation = Path.Combine(testFiles, "RaidHeroes", "raid_heroes.exe");
        }

        [TestMethod]
        public void TestHandle()
        {
            var testLog = Path.Combine(testFiles, "20170901-143309.evtc");
            var testOutput = Path.Combine(testFiles, "20170901-143309_golem.html");

            if (File.Exists(testOutput))
            {
                File.Delete(testOutput);
            }

            var handler = new RaidHeroesHandler();
            var e = handler.Handle(testLog);

            Assert.IsTrue(e.Success);
            Assert.IsTrue(e.Output.Equals(testOutput));
            Assert.IsTrue(File.Exists(testOutput));
            File.Delete(testOutput);
        }

        [TestMethod()]
        public void TestHandleError()
        {
            var testLog = Path.Combine(testFiles, "does-not-exist.evtc");
            var testOutput = Path.Combine(testFiles, "does-not-exist_err.html");

            if (File.Exists(testOutput))
            {
                File.Delete(testOutput);
            }

            var handler = new RaidHeroesHandler();
            var e = handler.Handle(testLog);

            Assert.IsFalse(e.Success);
            Assert.IsTrue(e.Output.Contains("File does-not-exist_err.html created"));
            Assert.IsTrue(File.Exists(testOutput));
            File.Delete(testOutput);
        }
    }
}