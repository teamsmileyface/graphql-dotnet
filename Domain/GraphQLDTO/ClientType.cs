using Domain.DTO;
using GraphQL.Types;

namespace Domain.GraphQLDTO
{
    public class ClientType : ObjectGraphType
    {
        public ClientType(ALBData data)
        {            
            Name = "Client";
            Description = "A client - person or organisation";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the client.");
            Field<StringGraphType>("name", "The name of the client.");

            Field<ListGraphType<MatterType>>("matters", "The matters for the client",
             resolve: context => data.GetMattersForClient(context.Source as Client));

            IsTypeOf = value => value is Client;
        }
    }
}