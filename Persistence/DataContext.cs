using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    // We have one entity that we have created so far, called "Activity"
    public class DataContext : DbContext // We want to be able to inject our DataContext as a service for other places in application.
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet is a property for the class, DataContext. It takes a "Type" parameter, so we are adding "Activity".
        // "What type of property is DbSet? It is the type, activity.
        // This property reflects the name of the database table, activities.
        public DbSet<Activity> Activities { get; set; }
    }
}