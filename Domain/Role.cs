using System.Collections.Generic;

namespace Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Contact> Contacts { get; set; }

        //public IEnumerable<Contact> Contacts { get; set; }
    }
}