using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class ChoiceRouteRepository
    {
        private readonly string sqlConnectionString;

        public ChoiceRouteRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<ChoiceRouteModel> InsertAsync(ChoiceRouteModel choiceRoute) 
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var result = await sqlConnection.ExecuteAsync("INSERT INTO [ChoiceRoute] (Id, UserId, Path, Begining, Middel, Finish, NamePatient, BirthDate, NameDoctor) VALUES (@Id, @UserId, @Path, @Begining, @Middel, @Finish, @NamePatient, @BirthDate, @NameDoctor)", choiceRoute );
                return choiceRoute;
            }
        }
        public async Task<ChoiceRouteModel> ReadByUserIdAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "SELECT * FROM [ChoiceRoute] WHERE Userid = @Userid";

                return await sqlConnection.QueryFirstOrDefaultAsync<ChoiceRouteModel>(query, new { userId });

            }
        }

        public async Task UpdateAsync(ChoiceRouteModel choiceRoute)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [ChoiceRoute] SET " +
                                                    "Path = @Path, " +
                                                    "Begining = @Begining, " +
                                                    "Middel = @Middel, " +
                                                    "Finish = @Finish, " +
                                                    "NamePatient = @NamePatient, " +
                                                    "BirthDate = @BirthDate, " +
                                                    "NameDoctor = @NameDoctor"
                                                    , choiceRoute);

            }
        }

        public async Task<IEnumerable<ChoiceRouteModel>> ReadAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            { 
                return await sqlConnection.QueryAsync<ChoiceRouteModel>("SELECT * FROM [ChoiceRoute] WHERE Userid = @Userid", new { userId});
            }
        }
    }
}
