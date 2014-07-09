namespace MvcApplication2.Repository.EF.Migrations
{
    using Common.Logging;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcApplication2.Repository.EF.RepositoryContext>
    {
        public static ILog Log = LogManager.GetCurrentClassLogger();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcApplication2.Repository.EF.RepositoryContext context)
        {
            Log.Debug("Migration Seed");

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            BusinessCardState[] cards = new[] {
                new BusinessCardState { Id = 1, Name = "howey",    EMail = "wki@cpan.org", Phone = "11833", EmployeeId = 1, Status = 2},
                new BusinessCardState { Id = 2, Name = "wolfgang", EMail = "wki@cpan.org", Phone = "11833", EmployeeId = 2, Status = 2}
            };

            context.BusinessCards.AddOrUpdate(c => c.Name, cards);
        }
    }
}
