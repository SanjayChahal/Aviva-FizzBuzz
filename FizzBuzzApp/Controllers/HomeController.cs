using Microsoft.AspNetCore.Mvc;
using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;

namespace FizzBuzzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFizzBuzzService _fizzBuzzService;
        private List<FizzBuzzModel> _finalModel;

        public HomeController(IFizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService;
            _finalModel = new List<FizzBuzzModel>();

            Console.WriteLine("Hello, World!");
            ModelState.AddModelError("Number", "i got refreshed");

        }

        public IActionResult Index()
        {
            Console.WriteLine("iam getting model from session");
            HttpContext.Session.SetObject("FizzBuzzModel", _finalModel);
            return View();
        }

        [HttpPost]
        public IActionResult Index(int number)
        {
            if (number < 1 || number > 1000)
            {
                ModelState.AddModelError("Number", "Please enter a number between 1 and 1000.");
                return View();
            }

            Console.WriteLine("iam setting model from session");
            _finalModel = HttpContext.Session.GetObject<List<FizzBuzzModel>>("FizzBuzzModel");

            var model = _fizzBuzzService.GenerateFizzBuzz(number);
            _finalModel.AddRange(model);
            HttpContext.Session.SetObject("FizzBuzzModel", _finalModel);
            return View(_finalModel);
        }
    }
}
