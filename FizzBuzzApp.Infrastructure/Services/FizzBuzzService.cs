using System.Drawing;
using FizzBuzzApp.Core.Common;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;

namespace FizzBuzzApp.Infrastructure.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public FizzBuzzService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public List<FizzBuzzModel> GenerateFizzBuzz(int number)
        {
            var model = new List<FizzBuzzModel>();
            var dayInitial = _dateTimeProvider.CurrentDayOfWeek.ToString()[0];

            string output = number.ToString();
            ColorEnum color = ColorEnum.Black;  // Default color
            string fizz = dayInitial + "izz";
            string buzz = dayInitial + "uzz";

            if (number % 3 == 0 && number % 5 == 0)
            {
                output = fizz + " " + buzz;
                color = ColorEnum.Red;
            }

            else if (number % 3 == 0)
            {
                output = fizz;
                color = ColorEnum.Green;
            }
            else if (number % 5 == 0)
            {
                output = buzz;
                color = ColorEnum.Blue;
            }

            model.Add(new FizzBuzzModel { Number = number, Output = output, Color = color });

            return model;
        }

       
    }
}
