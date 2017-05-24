using System;
using NUnit.Framework;
using Datamining;

namespace Datamining_Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            bool a = Program.TestFunction();
            Assert.AreEqual(a, true);
        }
    }
}
