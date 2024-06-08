using Microsoft.AspNetCore.Mvc;
using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFizzBuzzService _fizzBuzzService;
        private readonly ISessionService _sessionService;
        private List<FizzBuzzModel> _finalModel;

        public HomeController(IFizzBuzzService fizzBuzzService, ISessionService sessionService)
        {
            _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _finalModel = new List<FizzBuzzModel>();
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            Console.WriteLine("I am on page number redirection");

            // Retrieve the session data here
            var sessionModel = _sessionService.GetFizzBuzzModel();

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

            var sessionModel = _sessionService.GetFizzBuzzModel();

            if (sessionModel != null && sessionModel.Count > 0)
            {
                _finalModel.AddRange(sessionModel);
            }

            var model = _fizzBuzzService.GenerateFizzBuzz(number);

            if (model != null && model.Count > 0)
            {
                _finalModel.AddRange(model);
            }

            _sessionService.SetFizzBuzzModel(_finalModel);
            return RedirectToAction("Index");
        }
    }
}
