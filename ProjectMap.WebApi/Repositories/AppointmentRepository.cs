using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class AppointmentRepository
    {
        private readonly string sqlConnectionString;

        public AppointmentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Appointment> InsertAsync(Appointment appointment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Appointments] (Id, UserId, Date, Reason) VALUES (@Id, @UserId, @Date, @Reason)", appointment);
                return appointment;
            }
        }
    }
}
