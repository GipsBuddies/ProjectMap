using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AppointmentController(AppointmentRepository appointmentRepository, ILogger<AppointmentController> logger)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
        }

        [HttpPost(Name = "CreateAppointment")]
        public async Task<ActionResult> Add(Appointment appointment)
        {
            appointment.Id = Guid.NewGuid();
            var createdAppointment = await _appointmentRepository.InsertAsync(appointment);
            return Created();
        }
    }
}
