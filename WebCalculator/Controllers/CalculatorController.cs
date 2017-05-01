using System.Web.Mvc;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class CalculatorController : Controller
    {


        public ActionResult Calculator()
        {
            Calculator viewModel = new Models.Calculator();

            return View(viewModel);
        }
        public ActionResult Examples()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Solve(Calculator viewModel)
        {
            viewModel.Solve();

            return View("Calculator", viewModel);
        }
    }
}