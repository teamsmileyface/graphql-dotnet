using System.Linq;
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

            var arguments = new QueryArguments(
                new[]
                {
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "reference",
                        Description = "reference of the matter"
                    }
                });

            Field<ListGraphType<MatterType>>("matters", "The matters for the client", arguments, 
            resolve: context => data.GetMattersForClient(context.Source as Client, (string)context.Arguments["reference"]));
            
            IsTypeOf = value => value is Client;
        }
    }
}