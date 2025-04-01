using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;

namespace TestProjectApi;

[TestClass]
public class MaxAgeIsTwelveTest
{
    [TestMethod]
    public virtual async Task DateValidationWithValidDateTest()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        ChoiceRouteModel choiceRoute = new ChoiceRouteModel
        {
            UserId = userId,
            BirthDate = DateTime.Today.AddYears(-12)
        };

        var mockChoiceRouteRepository = new Mock<IChoiceRouteRepository>();
        mockChoiceRouteRepository.Setup(repository => repository.InsertAsync(choiceRoute))
                    .ReturnsAsync(choiceRoute);

        var logger = new Mock<ILogger<ChoiceRouteController>>();

        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(authenticationService => authenticationService.GetCurrentAuthenticatedUserId())
                    .Returns(userId.ToString);

        ChoiceRouteController choiceRouteController = new ChoiceRouteController(mockChoiceRouteRepository.Object, logger.Object, mockAuthenticationService.Object);

        // Act
        var result = await choiceRouteController.Add(choiceRoute);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedResult));
    }



    [TestMethod]
    public virtual async Task DateValidationWithNotValidDateTest()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        ChoiceRouteModel choiceRoute = new ChoiceRouteModel
        {
            UserId = userId,
            BirthDate = DateTime.Today.AddYears(-12).AddDays(-1)
        };

        var mockChoiceRouteRepository = new Mock<IChoiceRouteRepository>();
        mockChoiceRouteRepository.Setup(repository => repository.InsertAsync(choiceRoute))
                    .ReturnsAsync(choiceRoute);

        var logger = new Mock<ILogger<ChoiceRouteController>>();

        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(authenticationService => authenticationService.GetCurrentAuthenticatedUserId())
                    .Returns(userId.ToString);

        ChoiceRouteController choiceRouteController = new ChoiceRouteController(mockChoiceRouteRepository.Object, logger.Object, mockAuthenticationService.Object);

        // Act
        var result = await choiceRouteController.Add(choiceRoute);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }
}
