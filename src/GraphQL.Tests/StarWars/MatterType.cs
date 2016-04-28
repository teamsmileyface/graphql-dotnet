using GraphQL.Types;

namespace GraphQL.Tests
{
    public class MatterType : ObjectGraphType
    {
        public MatterType(StarWarsData data)
        {
            Name = "Matter";
            Description = "A legal thingy";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the matter.");
            Field<StringGraphType>("description", "The description of the matter.");
            //Field<ListGraphType<CharacterInterface>>(
            //    "friends",
            //    resolve: context => data.GetFriends(context.Source as StarWarsCharacter)
            //);
            //Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");
            //Field<StringGraphType>("primaryFunction", "The primary function of the droid.");

            //Interface<CharacterInterface>();

            IsTypeOf = value => value is Matter;
        }
    }
}