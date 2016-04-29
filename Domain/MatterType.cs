using GraphQL.Types;

namespace Domain
{
    public class MatterType : ObjectGraphType
    {
        public MatterType(ALBData data)
        {
            Name = "Matter";
            Description = "A legal thingy";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the matter.");
            Field<StringGraphType>("description", "The description of the matter.");
            Field<RoleType>(
                "role",
                resolve: context => data.GetRoleByIdAsync((int) context.Arguments["id"], context.Source as Matter),
                description: "roles for a matter",
                arguments: new QueryArguments(
                    new[]
                    {
                        new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "id", Description = "id of the role"}
                    })
                );
            Field<ListGraphType<RoleType>>(
                "roles",
                resolve: context => data.GetRoles(context.Source as Matter)
                );

            IsTypeOf = value => value is Matter;
        }
    }
}