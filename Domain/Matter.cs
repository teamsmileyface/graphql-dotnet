using System.Collections.Generic;

namespace Domain
{
    public class Matter
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        public List<Role> Roles { get; set; }
    }
}