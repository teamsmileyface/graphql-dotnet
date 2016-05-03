using System;

namespace Domain
{
    public class DebtDataSource
    {
        public Debt FindDebtByMatterId(Guid id)
        {
            return new Debt {ClaimNumber = id.ToString(), OriginalDebt = 123.4m};
        }
    }
}