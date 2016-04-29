using GraphQL.Types;

namespace Domain
{
    public class RoleType : ObjectGraphType
    {
        public RoleType(ALBData data)
        {
            Name = "Role";
            Description = "A role description";

            Field<NonNullGraphType<IntGraphType>>("id", "The id of the role.");
            Field<StringGraphType>("description", "The description of the role.");

            Field<ListGraphType<ContactType>>("contacts",
                resolve: context => data.GetContacts(context.Source as Role));

            IsTypeOf = value => value is Role;
        }
    }
}