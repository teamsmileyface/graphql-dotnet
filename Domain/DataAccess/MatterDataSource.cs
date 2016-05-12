using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Dapper;
using Domain.DTO;
using Xunit;

namespace Domain.DataAccess
{
    public class MatterDataSource
    {
        private readonly string _connectionString;

        public MatterDataSource(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<Matter> FindByReference(string reference)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                var matters = connection.Query<Matter>("SELECT ProjectId AS Id, matRef AS Reference, matDescription AS Description FROM Matter WHERE matRef = @Ref",
                    new {Ref = reference});

                return matters.ToList();
            }
        }

        public Matter FindByFqn()
        {
            var url ="http://localhost:55696/";
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "barry", "Passw0rd1"))));

                var response =
                    client.GetAsync("fields/matter/4F2D950A-95A6-4431-BDF1-6DC4B3995DAD?fqns=Matter.Description,Matter.Reference").Result;

                var jsonMatter = response.Content.ReadAsStringAsync().Result;

                var fields = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadFqnResult>(jsonMatter);
                                
                return new Matter()
                {
                    Description = (string) fields.Values.First(f => f.Key== "Matter.Description").Value,
                    Reference= (string)fields.Values.First(f => f.Key == "Matter.Reference").Value
                };
            }
        }

        public List<Matter> FindMattersByClientId(Guid id)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;

                connection.Open();

                var matters = connection.Query<Matter>(@"
                    SELECT m.ProjectId as Id, m.matRef as Reference, m.matDescription as Description 
                        FROM Matter m INNER JOIN dbo.ProjectAssociations pa ON m.ProjectId = pa.ProjectID
                            WHERE (pa.OrgID = @MemOrgID OR pa.MemberID = @MemOrgID) AND pa.AssociationRoleID = 1",
                    new { MemOrgID = id });

                return matters.ToList();
            }
        }
    }

    public class ReadFqnResult
    {
        public Dictionary<string, object> Values { get; set; }

        public ReadFqnResult(Dictionary<string, object> values)
        {
            this.Values = values;
        }
    }


    public class MatterTest
    {
        private readonly string _connectionString;

        public MatterTest()
        {
            _connectionString = "Data Source=(local);Database=IrisLawBusiness;User Id=sa;Password=20Mountain08";
        }

        [Fact(Skip = "database test")]
        public void TestMatterWorks()
        {
            var dataSource = new MatterDataSource(_connectionString);

            var matters = dataSource.FindByReference("M000010001");
            Assert.Equal(1, matters.Count);
            Assert.Equal("Matter for Multicheck", matters.Single().Description);
        }

        [Fact]
        public void TestFieldsApiWorks()
        {
            var dataSource = new MatterDataSource(_connectionString);

            var matter = dataSource.FindByFqn();
            /*Assert.Equal(1, matters.Count); */
                Assert.Equal("Matter for Multicheck", matter.Description);
            Assert.Equal("M000010001", matter.Reference);
        }
    }


    public class ClientTest
    {
        private readonly string _connectionString;

        public ClientTest()
        {
            _connectionString = "Data Source=(local);Database=IrisLawBusiness;User Id=sa;Password=20Mountain08";
        }

        [Fact(Skip = "database test")]
        public void TestClientWorks()
        {
            var dataSource = new ClientDataSource(_connectionString);

            var clients = dataSource.FindByReference("M00001");
            Assert.Equal(1, clients.Count);
            Assert.Equal("Mr M Multirole", clients.Single().Name);
        }
    }
}
