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
    public class ExpressionTests
    {
        [TestMethod()]
        public void ExpressionTest()
        {
            Expression express = new DomainEntities.Expression();

            Assert.IsNotNull(express);
            Assert.AreEqual("", express.rawValue);
            Assert.AreEqual("", express.errorMessage);
            Assert.IsNotNull(express.results);
            Assert.AreEqual(0, express.results.Count);
        }

        [TestMethod()]
        public void EvaluateTest()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "1 + 1";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("2", result);
        }

        [TestMethod()]
        public void EvaluateTest2()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "2*6+5*4";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("32", result);
            Assert.AreEqual(1, express.results.Count);
            Assert.AreEqual(result, express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest3()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "2*(6+5)*4";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("88", result);
            Assert.AreEqual(2, express.results.Count);
            Assert.AreEqual("2*11*4", express.results[0]);
            Assert.AreEqual(result, express.results[1]);
        }

        [TestMethod()]
        public void EvaluateTest4()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "[(8+2)*4/{1+1}]*2";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("40", result);
            Assert.AreEqual(4, express.results.Count);
            Assert.AreEqual("[10*4/{1+1}]*2", express.results[0]);
            Assert.AreEqual("[10*4/2]*2", express.results[1]);
            Assert.AreEqual("20*2", express.results[2]);
            Assert.AreEqual(result, express.results[3]);
        }

        [TestMethod()]
        public void EvaluateTest5()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "5%2";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("1", result);
            Assert.AreEqual(1, express.results.Count);
            Assert.AreEqual(result, express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest6()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "5/2";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("2.5", result);
            Assert.AreEqual(1, express.results.Count);
            Assert.AreEqual(result, express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest7()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "5/3";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual((5.0 / 3.0).ToString(), result);
            Assert.AreEqual(1, express.results.Count);
            Assert.AreEqual(result, express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest9()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "(5+3)*(5+3)";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("64", result);
            Assert.AreEqual(3, express.results.Count);
            Assert.AreEqual(result, express.results[2]);
            Assert.AreEqual("8*8", express.results[1]);
            Assert.AreEqual("8*(5+3)", express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest10()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "4^3";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("64", result);
            Assert.AreEqual(1, express.results.Count);
            Assert.AreEqual(result, express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest11()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "[(2+7)^(3-1)]/3";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("27", result);
            Assert.AreEqual(5, express.results.Count);
            Assert.AreEqual(result, express.results[4]);
            Assert.AreEqual("81/3", express.results[3]);
            Assert.AreEqual("[81]/3", express.results[2]);
            Assert.AreEqual("[9^2]/3", express.results[1]);
            Assert.AreEqual("[9^(3-1)]/3", express.results[0]);
        }

        [TestMethod()]
        public void EvaluateTest12()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "[1+2-1]^(2^(3-1))^0.5";
            result = express.Evaluate();

            Assert.AreEqual("", express.errorMessage);
            Assert.AreEqual("4", result);
            Assert.AreEqual(6, express.results.Count);
            Assert.AreEqual(result, express.results[5]);
            Assert.AreEqual("16^0.5", express.results[4]);
            Assert.AreEqual("2^4^0.5", express.results[3]);
            Assert.AreEqual("2^(4)^0.5", express.results[2]);
            Assert.AreEqual("2^(2^2)^0.5", express.results[1]);
            Assert.AreEqual("2^(2^(3-1))^0.5", express.results[0]);
        }


        [TestMethod()]
        public void EvaluateTest_Error1()
        {
            string result;
            Expression express = new DomainEntities.Expression();
            express.rawValue = "(5+3";
            result = express.Evaluate();

            Assert.AreEqual("Invalid expression.  Error with '(5+3', fix and resubmit.", express.errorMessage);
            Assert.AreEqual("", result);
        }
    }
}