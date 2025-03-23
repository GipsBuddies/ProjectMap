namespace ProjectMap.WebApi.Interfaces
{
    public interface IAuthenticationService
    {
        //Returns the user id of the authenticated user

        string? GetCurrentAuthenticatedUserId();
    }
}
