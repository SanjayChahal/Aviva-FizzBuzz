using System;
using System.Collections.Generic;
using FizzBuzzApp.Core;
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
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
        private readonly Mock<IHttpContextDataService> _contextServiceMock;
        private readonly FizzBuzzService _service;

        public FizzBuzzServiceTests()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _contextServiceMock = new Mock<IHttpContextDataService>();
            _service = new FizzBuzzService(_dateTimeProviderMock.Object, _contextServiceMock.Object);
        }

        [Theory]
        [InlineData(3, "Mizz", ColorEnum.Green, DayOfWeek.Monday)]
        [InlineData(5, "Tuzz", ColorEnum.Blue, DayOfWeek.Tuesday)]
        [InlineData(15, "Wizz Wuzz", ColorEnum.Red, DayOfWeek.Wednesday)]
        [InlineData(7, "7", ColorEnum.Black, DayOfWeek.Thursday)]
        public void GenerateFizzBuzz_VariousNumbers_ReturnsExpectedResult(int number, string expectedOutput, ColorEnum expectedColor, DayOfWeek dayOfWeek)
        {
            // Arrange
            _dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(dayOfWeek);

            // Act
            var result = _service.GenerateFizzBuzz(number);

            // Assert
            Assert.Single(result);
            Assert.Equal(expectedOutput, result[0].Output);
            Assert.Equal(expectedColor, result[0].Color);
        }

        [Fact]
        public void GetSavedSessionData_ReturnsCorrectData()
        {
            // Arrange
            var expectedData = new List<FizzBuzzModel>
            {
                new FizzBuzzModel { Number = 1, Output = "1", Color = ColorEnum.Black }
            };
            _contextServiceMock.Setup(m => m.GetData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey))
                               .Returns(expectedData);

            // Act
            var result = _service.GetSavedSessionData();

            // Assert
            Assert.Equal(expectedData, result);
        }

        [Fact]
        public void SaveModelData_ValidData_CallsSaveData()
        {
            // Arrange
            var dataToSave = new List<FizzBuzzModel>
            {
                new FizzBuzzModel { Number = 1, Output = "1", Color = ColorEnum.Black }
            };

            // Act
            _service.SaveModelData(dataToSave);

            // Assert
            _contextServiceMock.Verify(m => m.SaveData(Constants.SessionKeys.FizzBuzzModelKey, dataToSave), Times.Once);
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

            // Act
            var result = _service.GetPagedFizzBuzzModel(allModels, 2, 20);

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

            // Act
            var totalPages = _service.GetTotalPages(allModels, 20);

            // Assert
            Assert.Equal(5, totalPages);
        }

        [Fact]
        public void GetTotalPages_EmptyList_ReturnsZero()
        {
            // Arrange
            var allModels = new List<FizzBuzzModel>();

            // Act
            var totalPages = _service.GetTotalPages(allModels, 20);

            // Assert
            Assert.Equal(0, totalPages);
        }

        [Fact]
        public void GetTotalPages_NullList_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.GetTotalPages(null, 20));
        }
    }
}
