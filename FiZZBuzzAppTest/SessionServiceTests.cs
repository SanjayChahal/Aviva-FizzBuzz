using FizzBuzzApp;
using FizzBuzzApp.Models;
using FizzBuzzApp.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FizzBuzzAppTests
{
    public class SessionServiceTests
    {
        [Fact]
        public void SetFizzBuzzModel_Should_Set_Model_In_Session()
        {
            // Arrange
            var model = new List<FizzBuzzModel>
            {
                new FizzBuzzModel { Number = 1, Output = "1" },
                new FizzBuzzModel { Number = 2, Output = "2" },
                new FizzBuzzModel { Number = 3, Output = "Fizz" }
            };

            var session = new Mock<ISession>();
            session.Setup(s => s.SetObject("FizzBuzzModel", model));

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(h => h.HttpContext.Session).Returns(session.Object);

            var sessionService = new SessionService(httpContextAccessor.Object);

            // Act
            sessionService.SetFizzBuzzModel(model);

            // Assert
            session.Verify(s => s.SetObject("FizzBuzzModel", model), Times.Once);
        }

        [Fact]
        public void GetFizzBuzzModel_Should_Return_Model_From_Session()
        {
            // Arrange
            var model = new List<FizzBuzzModel>
            {
                new FizzBuzzModel { Number = 1, Output = "1" },
                new FizzBuzzModel { Number = 2, Output = "2" },
                new FizzBuzzModel { Number = 3, Output = "Fizz" }
            };

            var session = new Mock<ISession>();
            session.Setup(s => s.GetObject<List<FizzBuzzModel>>("FizzBuzzModel")).Returns(model);

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(h => h.HttpContext.Session).Returns(session.Object);

            var sessionService = new SessionService(httpContextAccessor.Object);

            // Act
            var result = sessionService.GetFizzBuzzModel();

            // Assert
            Assert.Equal(model, result);
        }
    }
}
