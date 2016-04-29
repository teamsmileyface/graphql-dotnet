using GraphQL.Types;

namespace Domain
{
    public class ClientType : ObjectGraphType
    {
        public ClientType()
        {            
            Name = "Client";
            Description = "A client - person or organisation";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the client.");
            Field<StringGraphType>("name", "The name of the client.");

            IsTypeOf = value => value is Client;
        }
    }
}