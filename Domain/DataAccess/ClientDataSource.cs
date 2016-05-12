using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Domain.DTO;

namespace Domain.DataAccess
{
    public class ClientDataSource
    {

        public ClientDataSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        public List<Client> FindByReference(string reference)
        {
            using (var connection = new SqlConnection())
            {                
                connection.ConnectionString =
                    _connectionString;
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

        public Client FindByMemOrgId(string id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                                
                var clients = connection.Query<Client>(@"SELECT Id, Reference, 
                                                       CASE WHEN IsPerson =1 THEN PersonTitle + ' ' + PersonName +' ' + PersonSurname 
                                                       ELSE orgName
                                                       END AS Name
                                                       FROM uvw_ContactSummary WHERE Id = @Id",
                    new { Id = id});

                return clients.FirstOrDefault();
            }
        }
    }
}