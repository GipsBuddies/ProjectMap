namespace ProjectMap.WebApi.Interfaces
{
    public interface IAuthenticationService
    {
        //Returns the user name of the authenticated user

        string? GetCurrentAuthenticatedUserId();
    }
}
