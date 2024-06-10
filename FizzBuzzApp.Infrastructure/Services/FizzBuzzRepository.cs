using System;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;
using FizzBuzzApp.Infrastructure.Services;

namespace FizzBuzzApp.Infrastructure.Services
{
	public class FizzBuzzRepository
	{
		public FizzBuzzRepository()
		{
		}
	}
}

//using FizzBuzzApp.Core.Interfaces;
//using FizzBuzzApp.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace FizzBuzzApp.Infrastructure.Data
//{
//    public class FizzBuzzRepository : IFizzBuzzRepository
//    {
//        private readonly IDateTimeProvider _dateTimeProvider;

//        public FizzBuzzRepository(IDateTimeProvider dateTimeProvider)
//        {
//            _dateTimeProvider = dateTimeProvider;
//        }

//        public List<FizzBuzzModel> GetFizzBuzz(int number, int pageNumber, int pageSize)
//        {
//            var model = new List<FizzBuzzModel>();

//            for (int i = 1; i <= number; i++)
//            {
//                string output = i.ToString();

//                if (i % 3 == 0 && i % 5 == 0) output = "fizz buzz";
//                else if (i % 3 == 0) output = "fizz";
//                else if (i % 5 == 0) output = "buzz";

//                if (_dateTimeProvider.Now.DayOfWeek == DayOfWeek.Wednesday)
//                {
//                    if (i % 3 == 0 && i % 5 == 0) output = "wizz wuzz";
//                    else if (i % 3 == 0) output = "wizz";
//                    else if (i % 5 == 0) output = "wuzz";
//                }

//                model.Add(new FizzBuzzModel { Number = i, Output = output });
//            }

//            // Apply pagination
//            return model.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
//        }
//    }
//}
