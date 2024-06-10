
namespace FizzBuzzApp.Core.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DayOfWeek CurrentDayOfWeek { get; }
    }
}
