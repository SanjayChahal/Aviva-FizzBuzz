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
        private List<FizzBuzzModel> _finalModel;

        public HomeController(IFizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
            _finalModel = new List<FizzBuzzModel>();
            Console.WriteLine("Hello, World!");
            ModelState.AddModelError("Number", "I got refreshed");
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            Console.WriteLine("I am on page number redirection");

            // Retrieve the session data here
            var sessionModel = HttpContext.Session.GetObject<List<FizzBuzzModel>>("FizzBuzzModel");

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
            Console.WriteLine("IAction index ");

            if (number < 1 || number > 1000)
            {
                ModelState.AddModelError("Number", "Please enter a number between 1 and 1000.");
                return View();
            }

            InitializeFinalModel();

            var sessionModel = HttpContext.Session.GetObject<List<FizzBuzzModel>>("FizzBuzzModel");

            if (sessionModel != null && sessionModel.Count > 0)
            {
                _finalModel.AddRange(sessionModel);
            }

            var model = _fizzBuzzService.GenerateFizzBuzz(number);

            if (model != null && model.Count > 0)
            {
                _finalModel.AddRange(model);
            }

            HttpContext.Session.SetObject("FizzBuzzModel", _finalModel);
            return RedirectToAction("Index");
        }

        private void InitializeFinalModel()
        {
            // Ensure _finalModel is initialized
            _finalModel ??= new List<FizzBuzzModel>();
        }
    }
}
