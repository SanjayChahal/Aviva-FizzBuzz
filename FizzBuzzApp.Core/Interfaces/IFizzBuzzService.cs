using System;
using FizzBuzzApp.Core.Models;

namespace FizzBuzzApp.Core.Interfaces
{
	public interface IFizzBuzzService
    {
        public List<FizzBuzzModel> GenerateFizzBuzz(int number);

    }
}

