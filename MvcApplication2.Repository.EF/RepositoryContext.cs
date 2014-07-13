using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MvcApplication2.Repository.EF
{
    public class RepositoryContext : DbContext
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();

        public RepositoryContext()
            // :base(ConfigurationService.ConnectionString)
            : base("name=DefaultConnection")
        {
            Log.Debug("Initializing RepositoryContext");

      
            Database.SetInitializer(new RepositoryInitializer());
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<BusinessCardState> BusinessCards { get; set; }
        public DbSet<HistoryEntryState> HistoryEntries { get; set; }

        protected override void Dispose(bool disposing)
        {
            Log.Debug("Disposing RepositoryContext");
            base.Dispose(disposing);
        }
    }
}
