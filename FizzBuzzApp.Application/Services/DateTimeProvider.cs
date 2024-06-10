using FizzBuzzApp.Core.Interfaces;

namespace FizzBuzzApp.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
        public DayOfWeek CurrentDayOfWeek => DateTime.Now.DayOfWeek;
    }
}
