using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.Tests
{
    [TestClass()]
    public class PowerTests
    {
        [TestMethod()]
        public void PowerTest()
        {
            Power power = new DomainEntities.Power(0.5);

            Assert.IsNotNull(power);
            Assert.AreEqual(0.5, power.number);
            Assert.AreEqual(-1, power.parenIndex);
        }

        [TestMethod()]
        public void PowerTest1()
        {
            Power power = new DomainEntities.Power(1.5,2);

            Assert.IsNotNull(power);
            Assert.AreEqual(1.5, power.number);
            Assert.AreEqual(2, power.parenIndex);
        }

        [TestMethod()]
        public void SolveTest()
        {
            Power power = new DomainEntities.Power(2.0);

            Assert.AreEqual(8.0, power.Solve(3.0));
        }

        [TestMethod()]
        public void SolveTest1()
        {
            Power power = new DomainEntities.Power(3.0);
            power.exponent = 2.0;

            Assert.AreEqual(9.0, power.Solve());
        }

        [TestMethod()]
        public void GetExpressionTest()
        {
            Power power = new DomainEntities.Power(2.5);
            power.exponent = 3.5;

            Assert.AreEqual("2.5^3.5", power.GetExpression());
        }
    }
}