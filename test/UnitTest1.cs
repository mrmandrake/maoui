using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugSweeperPage;
using Maoui;
using Maoui.Forms;
using Xamarin.Forms;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Forms.Init();
            var a = new BugsPage();
        }
    }
}
