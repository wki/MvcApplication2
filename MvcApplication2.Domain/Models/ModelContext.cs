using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcApplication2.Domain.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ModelContext() : base("ModelContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
