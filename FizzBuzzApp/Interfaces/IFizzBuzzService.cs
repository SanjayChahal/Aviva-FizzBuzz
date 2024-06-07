using System;
using FizzBuzzApp.Models;

namespace FizzBuzzApp.Interfaces
{
    public interface IFizzBuzzService
    {
        List<FizzBuzzModel> GenerateFizzBuzz(int number);
    }
}
