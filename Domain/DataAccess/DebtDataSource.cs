using System;
using Domain.DTO;

namespace Domain.DataAccess
{
    public class DebtDataSource
    {
        private readonly string _connectionString;

        public DebtDataSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Debt FindDebtByMatterId(Guid id)
        {
            return new Debt {ClaimNumber = id.ToString(), OriginalDebt = 123.4m};
        }     
    }
}