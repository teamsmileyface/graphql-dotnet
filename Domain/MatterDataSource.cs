using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Xunit;

namespace Domain
{
    public class MatterDataSource
    {
        public List<Matter> FindByReference(string reference)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString =
                    "Data Source=(local);Database=IrisLawBusiness;User Id=sa;Password=20Mountain08";
                connection.Open();

                var matters = connection.Query<Matter>("SELECT matRef AS Reference, matDescription AS Description FROM Matter WHERE matRef = @Ref",
                    new {Ref = reference});

                return matters.ToList();
            }
        }
    }

    public class ClientDataSource
    {
        public List<Client> FindByReference(string reference)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString =
                    "Data Source=(local);Database=IrisLawBusiness;User Id=sa;Password=20Mountain08";
                connection.Open();

                /*
                SELECT TOP 1000 [Id]
      ,[PersonSurname]
      ,[PersonTitle]
      ,[PersonName]
      ,[orgName]
      ,[IsPerson]
      ,[IsClient]
      ,[Reference]
  FROM [IrisLawBusiness].[dbo].[uvw_ContactSummary]
  */

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


    //public class MatterTest
    //{
    //    [Fact]
    //    public void TestMatterWorks()
    //    {
    //        var dataSource = new MatterDataSource();

    //        var matters = dataSource.FindByReference("M000010001");
    //        Assert.Equal(1, matters.Count);
    //        Assert.Equal("Matter for Multicheck", matters.Single().Description);
    //    }
    //}


    public class ClientTest
    {
        [Fact]
        public void TestClientWorks()
        {
            var dataSource = new ClientDataSource();

            var clients = dataSource.FindByReference("M00001");
            Assert.Equal(1, clients.Count);
            Assert.Equal("Mr M Multirole", clients.Single().Name);
        }
    }
}
