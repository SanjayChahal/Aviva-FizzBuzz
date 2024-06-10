using System;
using FizzBuzzApp.Core.Interfaces;

namespace FizzBuzzApp.Tests.Mocks
{
    public class MockDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _now;
        private readonly DayOfWeek _currentDayOfWeek;

        public MockDateTimeProvider(DateTime now, DayOfWeek currentDayOfWeek)
        {
            _now = now;
            _currentDayOfWeek = currentDayOfWeek;
        }

        public DateTime Now => _now;

        public DayOfWeek CurrentDayOfWeek => _currentDayOfWeek;
    }
}
