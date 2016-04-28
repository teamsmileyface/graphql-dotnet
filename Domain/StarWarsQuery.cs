using GraphQL.Types;

namespace Domain
{
    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery(StarWarsData data)
        {
            Name = "Query";

            //Field<CharacterInterface>("hero", resolve: context => data.GetDroidByIdAsync("3"));
            //Field<HumanType>(
            //    "human",
            //    arguments: new QueryArguments(
            //        new []
            //        {
            //            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
            //        }),
            //    resolve: context => data.GetHumanByIdAsync((string)context.Arguments["id"])
            //);
            //Field<DroidType>(
            //    "droid",
            //    arguments: new QueryArguments(
            //        new []
            //        {
            //            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the droid" }
            //        }),
            //    resolve: context => data.GetDroidByIdAsync((string)context.Arguments["id"])
            //);

            Field<MatterType>(
                "matter",
                arguments: new QueryArguments(
                    new[]
                    {
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "reference", Description = "reference of the matter" }
                    }),
                resolve: context => data.GetMatterByReferenceAsync((string)context.Arguments["reference"])
            );

            Field<ContactType>(
                "client",
                arguments: new QueryArguments(
                    new[]
                    {
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "reference", Description = "reference of the client" }
                    }),
                resolve: context => data.GetClientByReferenceAsync((string)context.Arguments["reference"])
            );

        }
    }
}
