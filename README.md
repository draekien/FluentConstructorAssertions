<p align="center">
  <img src="docs/wpei-logo.svg" alt="the logo of wpei.me">
</p>

<h1 align="center">Fluent Constructor Assertions</h1>

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Installing the package](#installing-the-package)
  - [Basic example](#basic-example)
  - [Advanced example](#advanced-example)

## Introduction

Fluent Constructor Assertions is a Fluent API for testing constructors in C#. The goal of this library is to make testing all those pesky `ArgumentNullExceptions` being thrown around when instantiating classes just a bit easier and less time intensive. It also provides a way to validate the happy path 😊

## Getting Started

### Installing the package

Install the `Fluent.ConstructorAssertions` package from nuget using Nuget Package Manager or via the console.

```ps
# Install the Fluent.ConstructorAssertions package to the project named MyProject
Install-Package Fluent.ConstructorAssertions -ProjectName MyProject
```

### Basic example

Lets say we want to test a class `MyClass` that takes `IMediator` and `ILogger<MyClass>` in it's constructor.

```csharp
public class MyClass
{
  private readonly IMediator _mediator;
  private readonly ILogger<MyClass> _logger;

  public MyClass(IMediator mediator, ILogger<MyClass> logger)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }
}
```

We can do the below to check that an `ArgumentNullException` is thrown when `IMediator` is `null` - aka the dependency was not registered correctly.

```csharp
[Fact]
public void GivenNullIMediator_WhenInstantiatingClass_ThenThrowArgumentNullException()
{
  ForConstructorOf<MyClass>
    .WithArgTypes(typeof(IMediator), typeof(ILogger<MyClass>))
    .Throws<ArgumentNullException>("Null mediator should throw exception")
    .ForArgs(null, Substitute.For<ILogger<MyClass>>())
    .Should.BeTrue();
}
```

### Advanced example

We can also use the call chaining functionality built into the API to do the below instead of writing a test for each parameter, and we can even check for the success scenario in the same chain!

```csharp
[Fact]
public void GivenNullArgs_WhenInstantiatingClass_ThenThrowArgumentNullException()
{
  IMediator mediator = Substitute.For<IMediator>();
  ILogger<MyClass> logger = Substitute.For<ILogger<MyClass>>();

  ForConstructorOf<MyClass>
    .WithArgTypes(typeof(IMediator), typeof(ILogger<MyClass>))
    .Throws<ArgumentNullException>("Null mediator should throw exception")
    .ForArgs(null, logger)
    .And.Throws<ArgumentNullException>("Null logger should throw exception")
    .ForArgs(mediator, null)
    .And.Succeeds("No exception should be thrown")
    .ForArgs(mediator, logger)
    .Should.BeTrue();
}
```
