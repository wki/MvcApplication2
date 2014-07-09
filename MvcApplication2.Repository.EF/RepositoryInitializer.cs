using Common.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MvcApplication2.Repository.EF
{
    public class RepositoryInitializer : CreateDatabaseIfNotExists<RepositoryContext>
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();

        protected override void Seed(RepositoryContext context)
        {
            Log.Debug("Inside Seed Method");

            if (context.BusinessCards.Find(1) == null)
            {
                PopulateDatabase(context);
            }

            base.Seed(context);
        }

        private void PopulateDatabase(RepositoryContext context)
        {
            Log.Debug("Seeding Database");

            BusinessCardState[] cards = new [] {
                new BusinessCardState { Id = 1, Name = "howey",    EMail = "wki@cpan.org", Phone = "11833", EmployeeId = 1, Status = 2},
                new BusinessCardState { Id = 2, Name = "wolfgang", EMail = "wki@cpan.org", Phone = "11833", EmployeeId = 2, Status = 2}
            };

            foreach (var c in cards)
            {
                context.BusinessCards.Add(c);
            }

            context.SaveChanges();
        }
    }
}
