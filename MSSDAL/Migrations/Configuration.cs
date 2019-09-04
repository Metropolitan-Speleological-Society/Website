namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
	using System.Reflection;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MSS.DAL.MSSContext>
    {
        public Configuration()
        {
			AutomaticMigrationsEnabled = false;
			ContextKey = "MSS.DAL.MSSContext";
        }

        protected override void Seed(MSS.DAL.MSSContext context)
        {
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
        }
    }
}
