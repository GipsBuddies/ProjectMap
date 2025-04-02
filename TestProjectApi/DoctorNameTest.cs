using Microsoft.Testing.Platform.Logging;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Repositories;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestProjectApi;

[TestClass]
public class DoctorNameTest
{
    [TestMethod]
    public async Task AddingDoctorNameWithActualNameShouldReturnCreatedResult()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        var MockChoiceRouteRepository = new Mock<IChoiceRouteRepository>();
        
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(authenticationService => authenticationService.GetCurrentAuthenticatedUserId())
                    .Returns(userId.ToString);

        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ChoiceRouteController>>();
        var ChoiceRouteController = new ChoiceRouteController(MockChoiceRouteRepository.Object, logger.Object, mockAuthenticationService.Object);
        
        ChoiceRouteModel choiceRoute = new ChoiceRouteModel
        {
            UserId = userId,
            NameDoctor = "Dr. Smith",
            BirthDate = new DateTime(2025, 4, 1),
            castColor = 2
        };

        // Act
        var response = await ChoiceRouteController.Add(choiceRoute);
        // Assert
        Assert.IsInstanceOfType(response, out CreatedResult createdObjectResult);
    }



    [TestMethod]
    public async Task AddingDoctorNameWithEmptyStringShouldReturnBadRequest()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        var MockChoiceRouteRepository = new Mock<IChoiceRouteRepository>();

        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(authenticationService => authenticationService.GetCurrentAuthenticatedUserId())
                    .Returns(userId.ToString);

        var logger = new Mock<Microsoft.Extensions.Logging.ILogger<ChoiceRouteController>>();
        var ChoiceRouteController = new ChoiceRouteController(MockChoiceRouteRepository.Object, logger.Object, mockAuthenticationService.Object);

        ChoiceRouteModel choiceRoute = new ChoiceRouteModel
        {
            UserId = userId,
            NameDoctor = "",
            BirthDate = new DateTime(2025, 4, 1)
        };

        // Act
        var response = await ChoiceRouteController.Add(choiceRoute);
        // Assert
        Assert.IsInstanceOfType(response, out BadRequestResult BadRequestResult);
    }
}
