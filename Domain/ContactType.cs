using GraphQL.Types;

namespace Domain
{
    public class ContactType : ObjectGraphType
    {
        public ContactType()
        {
            Name = "Contact";
            Description = "A contact - person or organisation";

            Field<NonNullGraphType<StringGraphType>>("reference", "The reference of the contact.");
            Field<StringGraphType>("name", "The name of a contact.");

            IsTypeOf = value => value is Contact;
        }
    }
}