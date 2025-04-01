using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectMap.WebApi.Controllers;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;

namespace TestProjectApi;

[TestClass]
public class AppointmentIsPlannedAfterTodayTest
{
    [TestMethod]
    public virtual async Task AppointmentWithValidDate()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        Appointment appointment = new Appointment
        {
            UserId = userId,
            Date = DateTime.Now.AddMinutes(10),
        };

        var mockAppointmentRepository = new Mock<IAppointmentRepository>();
        mockAppointmentRepository.Setup(repo => repo.InsertAsync(appointment))
                    .ReturnsAsync(appointment);

        var logger = new Mock<ILogger<AppointmentController>>();

        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(auth => auth.GetCurrentAuthenticatedUserId())
                                 .Returns(userId.ToString);

        AppointmentController controller = new AppointmentController(mockAppointmentRepository.Object, logger.Object, mockAuthenticationService.Object);

        // Act
        var result = await controller.Add(appointment);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedResult));
    }

    [TestMethod]
    public virtual async Task AppointmentWithInvalidDate()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        Appointment appointment = new Appointment
        {
            UserId = userId,
            Date = DateTime.Now.AddMinutes(-10),
        };

        var mockAppointmentRepository = new Mock<IAppointmentRepository>();
        mockAppointmentRepository.Setup(repo => repo.InsertAsync(appointment))
                    .ReturnsAsync(appointment);

        var logger = new Mock<ILogger<AppointmentController>>();

        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(auth => auth.GetCurrentAuthenticatedUserId())
                    .Returns(userId.ToString);

        AppointmentController controller = new AppointmentController(mockAppointmentRepository.Object, logger.Object, mockAuthenticationService.Object);

        // Act
        var result = await controller.Add(appointment);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }
}
