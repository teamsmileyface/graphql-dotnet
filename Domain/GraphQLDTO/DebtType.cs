using Domain.DTO;
using GraphQL.Types;

namespace Domain.GraphQLDTO
{
    public class DebtType : ObjectGraphType
    {
        public DebtType()
        {
            Name = "Debt";
            Description = "A debt";

            Field<DecimalGraphType>("originalDebt", "The original balance of the debt");
            Field<StringGraphType>("claimNumber", "The claim number");

            IsTypeOf = value => value is Debt;
        }
    }
}