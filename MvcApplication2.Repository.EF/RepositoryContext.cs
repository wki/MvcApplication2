using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MvcApplication2.Repository.EF
{
    public class CardRecord
    {
        private int p1;
        private string p2;
        private string p3;

        public CardRecord(int p1, string p2, string p3)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        [Key]
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class RepositoryContext : DbContext
    {
        public RepositoryContext()
            // :base(ConfigurationService.ConnectionString)
            : base("name=DefaultConnection")
        {
        }

        public DbSet<CardRecord> Cards { get; set; }
    }
}
