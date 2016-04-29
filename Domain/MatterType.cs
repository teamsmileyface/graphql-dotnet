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
            IsTypeOf = value => value is Matter;
        }
    }
}