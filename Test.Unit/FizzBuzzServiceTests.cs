using System;
using System.Collections.Generic;
using FizzBuzzApp.Core.Common;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;
using FizzBuzzApp.Infrastructure.Services;
using Moq;
using Xunit;

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

            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

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

            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

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

            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

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

            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

            // Act
            var result = service.GenerateFizzBuzz(7);

            // Assert
            Assert.Single(result);
            Assert.Equal("7", result[0].Output);
            Assert.Equal(ColorEnum.Black, result[0].Color);
        }

        [Fact]
        public void GetPagedFizzBuzzModel_ReturnsCorrectSubset()
        {
            // Arrange
            var allModels = new List<FizzBuzzModel>();
            for (int i = 1; i <= 100; i++)
            {
                allModels.Add(new FizzBuzzModel { Number = i, Output = i.ToString(), Color = ColorEnum.Black });
            }

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

            // Act
            var result = service.GetPagedFizzBuzzModel(allModels, 2, 20);

            // Assert
            Assert.Equal(20, result.Count);
            Assert.Equal("21", result[0].Output);
            Assert.Equal("40", result[19].Output);
        }

        [Fact]
        public void GetTotalPages_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var allModels = new List<FizzBuzzModel>();
            for (int i = 1; i <= 100; i++)
            {
                allModels.Add(new FizzBuzzModel { Number = i, Output = i.ToString(), Color = ColorEnum.Black });
            }

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var contextServiceMock = new Mock<IHttpContextDataService>();
            var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

            // Act
            var totalPages = service.GetTotalPages(allModels, 20);

            // Assert
            Assert.Equal(5, totalPages);
        }
    }
}
