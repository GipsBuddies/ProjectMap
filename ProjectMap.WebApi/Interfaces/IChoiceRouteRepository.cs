using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public interface IChoiceRouteRepository
    {
        Task<ChoiceRouteModel> InsertAsync(ChoiceRouteModel choiceRoute);
        Task<IEnumerable<ChoiceRouteModel>> ReadAsync(Guid userId);
        Task<ChoiceRouteModel> ReadByUserIdAsync(Guid userId);
        Task UpdateAsync(ChoiceRouteModel choiceRoute);
    }
}