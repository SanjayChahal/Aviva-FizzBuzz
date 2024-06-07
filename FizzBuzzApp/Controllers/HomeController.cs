using Microsoft.AspNetCore.Mvc;
using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;
using System;
using System.Collections.Generic;

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
            ModelState.AddModelError("Number", "I got refreshed");
        }

        public IActionResult Index()
        {
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
            return View(_finalModel);
        }

        private void InitializeFinalModel()
        {
            // Check if _finalModel is null and initialize it if needed
            if (_finalModel == null)
            {
                _finalModel = new List<FizzBuzzModel>();
            }
        }
    }
}
