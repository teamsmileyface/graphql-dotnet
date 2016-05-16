using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DataAccess;
using Domain.DTO;

namespace Domain
{
    public class ALBData
    {
        private readonly string _connectionString;

        public ALBData(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<Matter> GetMatterByReferenceAsync(string reference)
        {
            var dataSource = new MatterDataSource(_connectionString);
            return Task.FromResult(dataSource.FindByReference(reference).SingleOrDefault());
        }

        public Task<Client> GetClientByReferenceAsync(string id)
        {
            var dataSource = new ClientDataSource(_connectionString);
            return Task.FromResult(dataSource.FindByMemOrgId(id));
        }

        public Task<Debt> GetDebtForMatter(Matter source)
        {
            var dataSource = new DebtDataSource(_connectionString);
            return Task.FromResult(dataSource.FindDebtByMatterId(source.Id));
        }

        public Task<List<Matter>> GetMattersForClient(Client client, string matterReference)
        {
            var dataSource = new MatterDataSource(_connectionString);
            return Task.FromResult(dataSource.FindMatters(client.Id, matterReference));
        }

        // public delegate Task<TResult> Query<TParam, TResult>(TParam query);
    }
}
