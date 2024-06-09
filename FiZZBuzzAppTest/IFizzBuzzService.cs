using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;

using System.Collections.Generic;


namespace FizzBuzzAppTests.Controllers
{
    internal interface IFizzBuzzService
    {
    }
}





namespace FizzBuzzAppTests.Mocks
{
    public class MockSessionService : ISessionService
    {
        private List<FizzBuzzModel>? _fizzBuzzModel;

        public void SetFizzBuzzModel(List<FizzBuzzModel> model)
        {
            _fizzBuzzModel = model;
        }

        public List<FizzBuzzModel> GetFizzBuzzModel()
        {
            return _fizzBuzzModel ?? new List<FizzBuzzModel>();
        }
    }
}
