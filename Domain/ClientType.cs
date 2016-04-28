using GraphQL.Types;

namespace GraphQL.Tests
{
    public class ClientType : ObjectGraphType
    {
        public ClientType()
        {
            Name = "Client";
            Description = "A client - person or organisation";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the client.");
            Field<StringGraphType>("forename", "The forename of the client.");

            IsTypeOf = value => value is Client;
        }
    }
}