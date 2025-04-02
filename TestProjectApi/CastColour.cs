using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Repositories;
using ProjectMap.WebApi.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;



namespace TestProjectApi;

[TestClass]
public class CastColour
{
    [TestMethod]
    public async Task TestMethod1()
    {
        // Arrange
        int castColor = 5;

        var mockChoiceRouteRepository = new Mock<IChoiceRouteRepository>();
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        var logger = new Mock<ILogger<ChoiceRouteController>>();

        var controller = new ChoiceRouteController(mockChoiceRouteRepository.Object, logger.Object, mockAuthenticationService.Object);
        ChoiceRouteModel choiceRoute = new ChoiceRouteModel {castColor = castColor};

        // Act
        var response = await controller.Add(choiceRoute);
        // Assert
        Assert.IsInstanceOfType(response, out BadRequestResult badrequestResult);
    }
}
