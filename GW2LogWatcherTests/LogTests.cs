using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GW2LogWatcher.Tests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void TestAddLine()
        {
            var label = new LinkLabel();
            var log = new Log(label, 10);

            log.Add("Line 1");
            log.Add("Line 2");

            Assert.AreEqual("Line 1" + Environment.NewLine + "Line 2" + Environment.NewLine, label.Text);
        }

        [TestMethod]
        public void TestAddLink()
        {
            var label = new LinkLabel();
            var log = new Log(label, 10);

            var link = new LogLink {Label = "link", Address = "http://example.com/"};
            log.Add("There is a ", link, " on this line");

            Assert.AreEqual("There is a link on this line" + Environment.NewLine, label.Text);
            Assert.AreEqual(1, label.Links.Count);
            Assert.AreEqual(11, label.Links[0].Start);
            Assert.AreEqual(4, label.Links[0].Length);
            Assert.AreEqual(link.Address, label.Links[0].LinkData);
        }

        [TestMethod]
        public void TestShift()
        {
            var label = new LinkLabel();
            var log = new Log(label, 2);

            log.Add("Line 1");
            Assert.AreEqual("Line 1" + Environment.NewLine, label.Text);

            log.Add("Line 2");
            Assert.AreEqual("Line 1" + Environment.NewLine + "Line 2" + Environment.NewLine, label.Text);

            log.Add("Line 3");
            Assert.AreEqual("Line 2" + Environment.NewLine + "Line 3" + Environment.NewLine, label.Text);

            log.Add("Line 4");
            Assert.AreEqual("Line 3" + Environment.NewLine + "Line 4" + Environment.NewLine, label.Text);
        }
    }
}
