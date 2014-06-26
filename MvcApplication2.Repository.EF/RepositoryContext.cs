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
        public RepositoryContext()
            // :base(ConfigurationService.ConnectionString)
            : base("name=DefaultConnection")
        {
        }

        
        public DbSet<BusinessCardState> BusinessCards { get; set; }
        public DbSet<HistoryEntryState> HistoryEntries { get; set; }

    }
}
