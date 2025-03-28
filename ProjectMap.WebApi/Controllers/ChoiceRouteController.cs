using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;
using ProjectMap.WebApi.Repositories;

namespace ProjectMap.WebApi.Controllers
{
    [ApiController]
    [Route("ChoiceRoute")]
    public class ChoiceRouteController : Controller
    {
        private readonly ChoiceRouteRepository _choiceRouteRepository;
        private readonly ILogger<ChoiceRouteController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public ChoiceRouteController(ChoiceRouteRepository choiceRouteRepository, ILogger<ChoiceRouteController> logger, IAuthenticationService authenticationService)
        {
            _choiceRouteRepository = choiceRouteRepository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost(Name = "CreateChoiceRoute")]
        [Authorize]
        public async Task<ActionResult> Add(ChoiceRouteModel choiceRoute)
        {
            choiceRoute.Id = Guid.NewGuid();
            choiceRoute.UserId = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
            var createdAppointment = await _choiceRouteRepository.InsertAsync(choiceRoute );
            return Created();
        }

        [HttpGet(Name = "ReadChoiceRoute")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Appointment>>> Get()
        {
            Guid userId = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
            var readChoiceRoute = await _choiceRouteRepository.ReadByUserIdAsync(userId);
            return Ok(readChoiceRoute);
        }

        [HttpPut(Name = "UpdateChoiceRoute")]
        [Authorize] 
        public async Task<ActionResult> Update(Guid choiceRouteId, ChoiceRouteModel newChoiceRoute)
        {

            var existingChoiceRoute= await _choiceRouteRepository.ReadAsync(choiceRouteId);

            if (existingChoiceRoute == null)
                return NotFound();

            newChoiceRoute.UserId  = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());

            await _choiceRouteRepository.UpdateAsync(newChoiceRoute);

            return Ok(newChoiceRoute);
        }
    }
}
