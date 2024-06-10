using Microsoft.AspNetCore.Mvc;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;
using FizzBuzzApp.Core;

namespace FizzBuzzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFizzBuzzService _fizzBuzzService;
        private readonly IHttpContextDataService _dataService;
        private List<FizzBuzzModel> _finalModel;

        public HomeController(IFizzBuzzService fizzBuzzService, IHttpContextDataService sessionService)
        {
            _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
            _dataService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _finalModel = new List<FizzBuzzModel>();
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20)
        {

            // Retrieve the session data here

            var sessionModel = _dataService.GetData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey);
            
            if (sessionModel != null)
            {
                _finalModel = sessionModel;
            }

            // Calculate the start index based on page number and page size
            int startIndex = (pageNumber - 1) * pageSize;

            // Get a subset of FizzBuzzModel based on pagination parameters
            var modelSubset = _finalModel.Skip(startIndex).Take(pageSize).ToList();

            // Calculate total number of pages
            int totalPages = (int)Math.Ceiling((double)_finalModel.Count / pageSize);

            // Set pagination related data in ViewBag
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            // Pass the subset model to the view
            return View(modelSubset);
        }

        [HttpPost]
        public IActionResult Index(int number)
        {
            Console.WriteLine("ActionResult Index(int number)");

            if (number < 1 || number > 1000)
            {
                ModelState.AddModelError("Number", "Please enter a number between 1 and 1000.");
                return View();
            }

             var sessionModel = _dataService.GetData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey);


            if (sessionModel != null && sessionModel.Count > 0)
            {
                _finalModel.AddRange(sessionModel);
            }

            var model = _fizzBuzzService.GenerateFizzBuzz(number);

            if (model != null && model.Count > 0)
            {
                _finalModel.AddRange(model);
            }

            _dataService.SaveData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey, _finalModel);

            return RedirectToAction("Index");
        }
    }
}
