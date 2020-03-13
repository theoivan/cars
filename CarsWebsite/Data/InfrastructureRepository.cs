namespace API.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;

    public class InfrastructureRepository : IInfrastructureRepository
    {
        private readonly IDbConnection db;

        public InfrastructureRepository(string connStrings)
        {
            this.db = new SqlConnection(connStrings);
        }

        public List<string> GetAllCountries()
        {
            return this.db.QueryAsync<string>("SELECT Country FROM Countries").Result.ToList();
        }

        public List<string> GetAllEmissionStandards()
        {
            return this.db.QueryAsync<string>("SELECT EmissionStandard FROM EmissionStandards").Result.ToList();
        }

        public List<string> GetAllFuelTypes()
        {
            return this.db.QueryAsync<string>("SELECT Fuel FROM FuelTypes").Result.ToList();
        }
    }
}
