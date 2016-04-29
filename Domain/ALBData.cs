using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class ALBData
    {

        public Task<Matter> GetMatterByReferenceAsync(string reference)
        {
            var dataSource = new MatterDataSource();
            return Task.FromResult(dataSource.FindByReference(reference).SingleOrDefault());
        }

        public Task<Client> GetClientByReferenceAsync(string reference)
        {
            var dataSource = new ClientDataSource();
            return Task.FromResult(dataSource.FindByReference(reference).SingleOrDefault());
        }
    }
}
