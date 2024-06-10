using FizzBuzzApp.Core.Common;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Infrastructure.Services;
using Moq;


namespace FizzBuzzApp.Tests
{
    public class FizzBuzzServiceTests
    {
        [Fact]
        public void GenerateFizzBuzz_NumberDivisibleBy3_ReturnsFizz()
        {
            // Arrange
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(DayOfWeek.Monday);

            var service = new FizzBuzzService(dateTimeProviderMock.Object);

            // Act
            var result = service.GenerateFizzBuzz(3);

            // Assert
            Assert.Single(result);
            Assert.Equal("Mizz", result[0].Output);
            Assert.Equal(ColorEnum.Green, result[0].Color);
        }

        [Fact]
        public void GenerateFizzBuzz_NumberDivisibleBy5_ReturnsBuzz()
        {
            // Arrange
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(DayOfWeek.Tuesday);

            var service = new FizzBuzzService(dateTimeProviderMock.Object);

            // Act
            var result = service.GenerateFizzBuzz(5);

            // Assert
            Assert.Single(result);
            Assert.Equal("Tuzz", result[0].Output);
            Assert.Equal(ColorEnum.Blue, result[0].Color);
        }

        [Fact]
        public void GenerateFizzBuzz_NumberDivisibleBy3And5_ReturnsFizzBuzz()
        {
            // Arrange
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(DayOfWeek.Wednesday);

            var service = new FizzBuzzService(dateTimeProviderMock.Object);

            // Act
            var result = service.GenerateFizzBuzz(15);

            // Assert
            Assert.Single(result);
            Assert.Equal("Wizz Wuzz", result[0].Output);
            Assert.Equal(ColorEnum.Red, result[0].Color);
        }



        [Fact]
        public void GenerateFizzBuzz_NumberNotDivisibleBy3Or5_ReturnsNumber()
        {
            // Arrange
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(DayOfWeek.Thursday);

            var service = new FizzBuzzService(dateTimeProviderMock.Object);

            // Act
            var result = service.GenerateFizzBuzz(7);

            // Assert
            Assert.Single(result);
            Assert.Equal("7", result[0].Output);
            Assert.Equal(ColorEnum.Black, result[0].Color);
        }
    }
}
