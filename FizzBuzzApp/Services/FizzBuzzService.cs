using System;
using System.Collections.Generic;
using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;

namespace FizzBuzzApp.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public List<FizzBuzzModel> GenerateFizzBuzz(int number)
        {
            var model = new List<FizzBuzzModel>();
            char dayInitial = DateTime.Now.DayOfWeek.ToString()[0]; // Get the first initial of the current day

            string output = number.ToString();
            string fizz = dayInitial + "izz";
            string buzz = dayInitial + "uzz";

            if (number % 3 == 0 && number % 5 == 0) output = fizz + " " + buzz;
            else if (number % 3 == 0) output = fizz;
            else if (number % 5 == 0) output = buzz;

            model.Add(new FizzBuzzModel { Number = number, Output = output });

            return model;
        }
    }
}

