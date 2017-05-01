using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebCalculator.Models;

namespace WebCalculator.Controllers.Tests
{
    [TestClass()]
    public class CalculatorControllerTests
    {
        [TestMethod()]
        public void CalculatorTest()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();

            // Act
            ViewResult result = controller.Calculator() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ExamplesTest()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();

            // Act
            ViewResult result = controller.Examples() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void SolveTest()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();

            Calculator calculator = new Calculator();
            calculator.expression = "1 + 1";

            // Act
            ViewResult result = controller.Solve(calculator) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("2", calculator.result);
            Assert.AreEqual(1, calculator.steps.Count);
            Assert.AreEqual("2", calculator.steps[0]);
            Assert.AreEqual("", calculator.errorMessage);
        }
    }
}