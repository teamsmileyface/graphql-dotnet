using System;

namespace Domain.DTO
{
    public class Matter
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public Debt Debt { get; set; }
    }
}