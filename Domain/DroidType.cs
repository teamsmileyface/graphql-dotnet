using GraphQL.Types;

namespace Domain
{
    public class DroidType : ObjectGraphType
    {
        public DroidType(StarWarsData data)
        {
            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe.";

            Field<NonNullGraphType<StringGraphType>>("id", "The id of the droid.");
            Field<StringGraphType>("name", "The name of the droid.");
            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => data.GetFriends(context.Source as StarWarsCharacter)
            );
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");
            Field<StringGraphType>("primaryFunction", "The primary function of the droid.");

            Interface<CharacterInterface>();

            IsTypeOf = value => value is Droid;
        }
    }
}
