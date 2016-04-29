using System;

namespace Domain
{

    public class Client : Contact
    {
        public string Reference { get; set; }

    }

    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // public Person Person { get; set; }
        // public Organisation Organisation { get; set; }
    }

    //public class Person 
    //{
    //    public Guid Id { get; set; }
    //    public string Title { get; set; }
    //    public string Forename { get; set; }
    //    public string Surname { get; set; }
    //}

    //public class Organisation
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //}
}