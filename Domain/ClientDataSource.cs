using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Domain
{
    public class ClientDataSource
    {
        public List<Client> FindByReference(string reference)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString =
                    "Data Source=(local);Database=IrisLawBusiness;User Id=sa;Password=20Mountain08";
                connection.Open();
                
                var clients = connection.Query<Client>(@"SELECT Id, Reference, 
                                                       CASE WHEN IsPerson =1 THEN PersonTitle + ' ' + PersonName +' ' + PersonSurname 
                                                       ELSE orgName
                                                       END AS Name
                                                       FROM uvw_ContactSummary WHERE Reference = @Ref",
                    new { Ref = reference });

                return clients.ToList();
            }
        }
    }
}