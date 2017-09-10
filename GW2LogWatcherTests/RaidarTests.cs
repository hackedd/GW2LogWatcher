using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GW2LogWatcher.Tests
{
    [TestClass]
    public class RaidarTests
    {
        private string testFiles;
        private string username;
        private string password;

        [TestInitialize]
        public void TestInitialize()
        {
            testFiles = TestUtils.TestFiles;

            username = TestUtils.Settings.RaidarUsername;
            password = TestUtils.Settings.RaidarPassword;

            if (string.IsNullOrEmpty(username))
                Assert.Inconclusive("Gw2Raidar username not configured.");
            if (string.IsNullOrEmpty(password))
                Assert.Inconclusive("Gw2Raidar password not configured.");
        }

        [TestMethod]
        public void TestLogin()
        {
            var session = new RaidarSession();
            session.Login(username, password);
            Assert.IsTrue(session.IsLoggedIn);
        }

        [TestMethod]
        public void TestLoginIncorrect()
        {
            var session = new RaidarSession();

            try
            {
                session.Login(username, "incorrect");
                Assert.Fail("Login with incorrect credentials did not fail");
            }
            catch (WebException e)
            {
                if (!e.Message.StartsWith("Unable to log in"))
                    throw;
            }

            Assert.IsFalse(session.IsLoggedIn);
        }

        [TestMethod]
        public void TestUploadAndWait()
        {
            var session = new RaidarSession();
            session.Login(username, password);
            Assert.IsTrue(session.IsLoggedIn);

            var testLog = Path.Combine(testFiles, "20170904-143221.evtc");
            var notification = session.UploadAndWait(testLog);

            Assert.IsNotNull(notification);
            Assert.AreEqual("20170904-143221.evtc", notification.Filename);
        }

        [TestMethod]
        public void TestHandler()
        {
            RaidarSession.Instance.Login(username, password);

            var testLog = Path.Combine(TestUtils.TestFiles, "20170904-143221.evtc");
            var handler = new RaidarHandler();
            var e = handler.Handle(testLog);

            Assert.IsTrue(e.Success);
            Assert.IsTrue(e.Output.StartsWith("https://www.gw2raidar.com/encounter/"));
        }
    }
}
