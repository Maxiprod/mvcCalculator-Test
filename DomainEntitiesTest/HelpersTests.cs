using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DomainEntities.Tests
{
    [TestClass()]
    public class HelpersTests
    {
        [TestMethod()]
        public void ReplaceFirstTest()
        {
            string startValue = "This is the test.";
            string search = "is";
            string replace = "ere";

            Assert.AreEqual("There is the test.", startValue.ReplaceFirst(search, replace));
        }

        [TestMethod()]
        public void IsNumericTest1()
        {
            string value = "5";

            Assert.AreEqual(true, value.IsNumeric());
        }

        [TestMethod()]
        public void IsNumericTest2()
        {
            string value = "5.0";

            Assert.AreEqual(true, value.IsNumeric());
        }

        [TestMethod()]
        public void IsNumericTest3()
        {
            string value = "5.0+";

            Assert.AreEqual(false, value.IsNumeric());
        }

        [TestMethod()]
        public void IsNumericTest4()
        {
            string value = "5^4";

            Assert.AreEqual(false, value.IsNumeric());
        }

        [TestMethod()]
        public void AddValueTest()
        {
            ArrayList list = new ArrayList();
            list.Add("4");

            list.AddValue("4");

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("4", list[0]);
        }

        [TestMethod()]
        public void AddValueTest1()
        {
            ArrayList list = new ArrayList();
            list.Add("4");

            list.AddValue("5");

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("4", list[0]);
            Assert.AreEqual("5", list[1]);
        }

        [TestMethod()]
        public void AddValueTest2()
        {
            ArrayList list = new ArrayList();
            list.Add("4");
            list.Add("6");

            list.AddValue("4");

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("4", list[0]);
            Assert.AreEqual("6", list[1]);
            Assert.AreEqual("4", list[2]);
        }

        [TestMethod()]
        public void AddValueTest4()
        {
            ArrayList list = new ArrayList();

            list.AddValue("4");

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("4", list[0]);
        }
    }
}