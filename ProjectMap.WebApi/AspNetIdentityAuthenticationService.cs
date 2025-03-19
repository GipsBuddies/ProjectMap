using System.Security.Claims;

namespace ProjectMap.WebApi
{
    public class AspNetIdentityAuthenticationService : Interfaces.IAuthenticationService
    {
        //Based on the example code provided by Microsoft

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetIdentityAuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public string? GetCurrentAuthenticatedUserId()
        {
            // Returns the aspnet_User.Id of the authenticated user
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
