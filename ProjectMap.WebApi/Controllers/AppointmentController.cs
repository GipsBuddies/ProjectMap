using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("Appointments")]
    public class AppointmentController : Controller
    {
        
        private readonly AppointmentRepository _appointmentRepository;
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AppointmentController(AppointmentRepository appointmentRepository, ILogger<AppointmentController> logger, IAuthenticationService authenticationService)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost(Name = "CreateAppointment")]
        public async Task<ActionResult> Add(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid();
            appointment.UserId = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
            var createdAppointment = await _appointmentRepository.InsertAsync(appointment);
            return Created();
        }

        [HttpGet(Name = "ReadAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            Guid userId = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
            var appointments = await _appointmentRepository.ReadByUserIdAsync(userId);
            return Ok(appointments);
        }

        [HttpDelete("{appointmentId}", Name = "DeleteAppointment")]
        public async Task<IActionResult> DeleteAsync(Guid appointmentId)
        {
            await _appointmentRepository.DeleteAsync(appointmentId);
            return Ok();
        }
    }
}
