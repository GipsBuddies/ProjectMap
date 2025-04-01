using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Interfaces
{
    public interface IAppointmentRepository
    {
        Task DeleteAsync(Guid id);
        Task<Appointment> InsertAsync(Appointment appointment);
        Task<List<Appointment>> ReadByUserIdAsync(Guid userId);
    }
}