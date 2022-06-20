using System;

namespace Domain
{
    // An Activity is an object with all of the listed properties. All properties for the class "Activity" will be columns
    // in a database table called "Activities."
    // We will use entity framework to connect the API Controller to our backend.
    // Entity Framework is an "object-relational mapper" which allows us to use C# to query our database. 
    // Entity Framework is a layer of abstraction, so we don't have to worry about what database language we're using.
    // To use Entity Framework, we need a database context, which we'll create in the persistence layer.
    public class Activity
    {
        public Guid Id { get; set; } // We use guid because Guid can be generated on both server and client. Because we use 'Id' EntityFramework will recognize as Primary Key.
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
    }
}