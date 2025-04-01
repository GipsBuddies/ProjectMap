using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Interfaces;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class ChoiceRouteRepository : IChoiceRouteRepository
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
                var result = await sqlConnection.ExecuteAsync("INSERT INTO [ChoiceRoute] (UserId, Path, Begining, Middel, Finish, NamePatient, BirthDate, NameDoctor, characterType, castColor, hasCastOnLeftArm, hasCastOnRightArm, hasCastOnLeftLeg, hasCastOnRightLeg, skinTone, hairStyle, hairColor, shirtColor, pantsColor, shoeColor) VALUES (@UserId, @Path, @Begining, @Middel, @Finish, @NamePatient, @BirthDate, @NameDoctor, @characterType, @castColor, @hasCastOnLeftArm, @hasCastOnRightArm, @hasCastOnLeftLeg, @hasCastOnRightLeg, @skinTone, @hairStyle, @hairColor, @shirtColor, @pantsColor, @shoeColor)", choiceRoute);
                return choiceRoute;
            }
        }
        public async Task<ChoiceRouteModel> ReadByUserIdAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var query = "SELECT * FROM [ChoiceRoute] WHERE UserId = @UserId";

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
                                                    "NameDoctor = @NameDoctor, " +
                                                    "characterType = @characterType, " +
                                                    "castColor = @castColor, " +
                                                    "hasCastOnLeftArm = @hasCastOnLeftArm, " +
                                                    "hasCastOnRightArm = @hasCastOnRightArm, " +
                                                    "hasCastOnLeftLeg = @hasCastOnLeftLeg, " +
                                                    "hasCastOnRightLeg = @hasCastOnRightLeg, " +
                                                    "skinTone = @skinTone, " +
                                                    "hairStyle = @hairStyle, " +
                                                    "hairColor = @hairColor, " +
                                                    "shirtColor = @shirtColor, " +
                                                    "pantsColor = @pantsColor, " +
                                                    "shoeColor = shoeColor " +
                                                    "WHERE UserId = @UserId"
                                                    , choiceRoute);

            }
        }

        public async Task<IEnumerable<ChoiceRouteModel>> ReadAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<ChoiceRouteModel>("SELECT * FROM [ChoiceRoute] WHERE Userid = @Userid", new { userId });
            }
        }
    }
}
