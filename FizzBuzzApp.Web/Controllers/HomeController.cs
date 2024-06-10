using Microsoft.AspNetCore.Mvc;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;
using System;
using System.Collections.Generic;

namespace FizzBuzzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFizzBuzzService _fizzBuzzService;
        private List<FizzBuzzModel> _fizzBuzzModel;

        public HomeController(IFizzBuzzService fizzBuzzService)
        {
            Console.WriteLine("HomeController initialized");

            _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
            _fizzBuzzModel = new List<FizzBuzzModel>();
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20)
        {
            Console.WriteLine("Index GET method called");

            //Get saved fizze Buzz model
            _fizzBuzzModel = _fizzBuzzService.GetSavedSessionData() ?? new List<FizzBuzzModel>();

            var modelSubset = _fizzBuzzService.GetPagedFizzBuzzModel(_fizzBuzzModel, pageNumber, pageSize);

            ViewBag.TotalPages = _fizzBuzzService.GetTotalPages(_fizzBuzzModel, pageSize);
            ViewBag.PageNumber = pageNumber;

            return View(modelSubset);
        }

        [HttpPost]
        public IActionResult Index(int number)
        {
            Console.WriteLine("Index POST method called");

            if (number < 1 || number > 1000)
            {
                ModelState.AddModelError("Number", "Please enter a number between 1 and 1000.");
                return View();
            }

            _fizzBuzzModel = _fizzBuzzService.GetSavedSessionData() ?? new List<FizzBuzzModel>();

            var model = _fizzBuzzService.GenerateFizzBuzz(number);
            if (model != null && model.Count > 0)
            {
                _fizzBuzzModel.AddRange(model);
            }

            _fizzBuzzService.SaveModelData(_fizzBuzzModel);

            return RedirectToAction("Index");
        }
    }
}
