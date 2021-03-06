using System;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.GraphQLDTO;
using GraphQL.Types;

namespace Domain
{
    public class ALBQuery : ObjectGraphType
    {
        public ALBQuery(ALBData data)
        {
            Name = "Query";

            Field<MatterType>(
                "matter",
                arguments: new QueryArguments(
                    new[]
                    {
                        new QueryArgument<NonNullGraphType<StringGraphType>>
                        {
                            Name = "reference",
                            Description = "reference of the matter"
                        }
                    }),
                resolve: context => data.GetMatterByReferenceAsync((string) context.Arguments["reference"])
                );

            Field<ClientType>(
                "client",
                arguments: new QueryArguments(
                    new[]
                    {
                        new QueryArgument<StringGraphType> {Name = "reference", Description = "reference of the client"},
                        new QueryArgument<StringGraphType> {Name = "id", Description = "guid of the client"}
                    }),
                resolve: context => GetClientAsync(data, context)
                );



 }

        private static Task<Client> GetClientAsync(ALBData data, ResolveFieldContext context)
        {
            if (context.Arguments["id"] != null)
                return data.GetClientByReferenceAsync((string) context.Arguments["id"]);
            
            return null;
        }
    }
}