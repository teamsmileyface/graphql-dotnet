using System;
using System.Collections.Generic;

namespace Domain
{
    public class Matter
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public Debt Debt { get; set; }
    }
}