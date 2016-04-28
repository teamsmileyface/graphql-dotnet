using GraphQL.Types;

namespace Domain
{
    public class ContactType : ObjectGraphType
    {
        public ContactType()
        {
            Name = "Contact";
            Description = "A contact - person or organisation";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the client.");
            Field<StringGraphType>("forename", "The forename of the client.");

            IsTypeOf = value => value is Contact;
        }
    }
}