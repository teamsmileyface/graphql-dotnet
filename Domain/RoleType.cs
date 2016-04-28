using GraphQL.Types;

namespace Domain
{
    public class RoleType : ObjectGraphType
    {
        public RoleType()
        {
            Name = "Role";
            Description = "A role description";

            Field<NonNullGraphType<IntGraphType>>("id", "The id of the role.");
            Field<StringGraphType>("description", "The description of the role.");

            IsTypeOf = value => value is Role;
        }
    }
}