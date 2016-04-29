using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;

namespace Domain
{
    public class ALBData
    {
        public IEnumerable<Role> GetRoles(Matter matter)
        {
            if (matter == null)
            {
                return null;
            }

            return matter.Roles; 
                     
        }

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

        public Task<Role> GetRoleByIdAsync(int id, Matter matter)
        {
            return Task.FromResult(GetRoles(matter).FirstOrDefault(r => r.Id == id));
        }

        public Task<List<Contact>> GetContacts(Role role)
        {
            return Task.FromResult(role.Contacts);
        }
    }
}
