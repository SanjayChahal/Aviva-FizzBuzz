## Design Document for FizzBuzzApp

### Project Overview
FizzBuzzApp is a web application developed using ASP.NET Core MVC that allows users to generate a FizzBuzz sequence based on an input number. The application adheres to modern software design principles, including the Onion Architecture, SOLID principles, and dependency injection. This document details the architectural design, layer responsibilities, and testing strategy employed in the project.

### Architectural Design

#### Onion Architecture
The Onion Architecture is a layered architecture that emphasizes the separation of concerns, making the system more maintainable, testable, and scalable. The architecture consists of several layers:

1. **Core Layer (Domain Layer)**: Contains the business logic and domain entities.
2. **Service Layer**: Contains service interfaces and their implementations, handling the core application logic.
3. **Infrastructure Layer**: Handles data access, external service integrations, and other infrastructure-related concerns.
4. **Presentation Layer**: Contains the UI logic, controllers, and views.

The dependencies in Onion Architecture flow inward, ensuring that the core logic is isolated from external concerns.

### Layers and Responsibilities

#### Core Layer
- **Entities**: Represents the domain entities, such as `FizzBuzzModel`.
- **Interfaces**: Defines interfaces for the services and data access. For example, `IFizzBuzzService` and `IDateTimeProvider`.

#### Service Layer
- **Services**: Contains the implementation of service interfaces. For example, `FizzBuzzService` implements the `IFizzBuzzService` interface and contains the main business logic for generating FizzBuzz sequences.

#### Infrastructure Layer
- **Data Services**: Implements data access and external service interaction logic. For example, `HttpContextDataService` provides session management.

#### Presentation Layer
- **Controllers**: Manages HTTP requests and responses. For example, `HomeController` handles the main application logic for user interaction.
- **Views**: Defines the UI components, like Razor views, that present data to the user.

### Dependency Injection
Dependency Injection (DI) is used to enhance the testability and modularity of the application by decoupling the implementation from the interface. This is achieved by injecting dependencies through constructors, allowing for easy swapping of implementations, especially during testing.

**Example**:
```csharp
public class HomeController : Controller
{
    private readonly IFizzBuzzService _fizzBuzzService;
    private readonly IHttpContextDataService _dataService;

    public HomeController(IFizzBuzzService fizzBuzzService, IHttpContextDataService sessionService)
    {
        _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
        _dataService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
    }
}
```

### SOLID Principles
The project adheres to the SOLID principles to ensure a robust and maintainable codebase:

1. **Single Responsibility Principle (SRP)**: Each class has a single responsibility. For example, `FizzBuzzService` handles the business logic, while `HomeController` manages HTTP requests.
2. **Open/Closed Principle (OCP)**: Classes are open for extension but closed for modification. The use of interfaces and abstractions allows for extending functionality without modifying existing code.
3. **Liskov Substitution Principle (LSP)**: Derived classes can be substituted for their base classes. Interfaces like `IFizzBuzzService` ensure that any implementation can be used interchangeably.
4. **Interface Segregation Principle (ISP)**: Clients should not be forced to depend on methods they do not use. Interfaces are designed to be specific to the functionality they represent.
5. **Dependency Inversion Principle (DIP)**: High-level modules depend on abstractions, not on concrete implementations. Dependency injection is used to inject the required dependencies.

### Unit Testing
Unit testing is crucial to verify the correctness of the business logic. The `FizzBuzzService` class, containing most of the business logic, is thoroughly tested using the Moq framework to mock dependencies.

**Example Test**:
```csharp
public class FizzBuzzServiceTests
{
    [Fact]
    public void GenerateFizzBuzz_NumberDivisibleBy3_ReturnsFizz()
    {
        var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        dateTimeProviderMock.Setup(m => m.CurrentDayOfWeek).Returns(DayOfWeek.Monday);

        var contextServiceMock = new Mock<IHttpContextDataService>();
        var service = new FizzBuzzService(dateTimeProviderMock.Object, contextServiceMock.Object);

        var result = service.GenerateFizzBuzz(3);

        Assert.Single(result);
        Assert.Equal("Mizz", result[0].Output);
        Assert.Equal(ColorEnum.Green, result[0].Color);
    }
}
```
In the above test:
- Moq is used to mock the `IDateTimeProvider` and `IHttpContextDataService`.
- The `FizzBuzzService` is tested to ensure it returns the correct FizzBuzz value for a number divisible by 3.

### Conclusion
The FizzBuzzApp project is designed with a focus on modularity, testability, and maintainability. By using Onion Architecture, SOLID principles, and dependency injection, the application is well-structured, easy to extend, and robust against changes. The comprehensive unit tests ensure that the business logic is correct and reliable.
