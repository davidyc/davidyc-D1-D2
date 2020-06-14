namespace Task_2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task_2.NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NorthwindContext context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Super Good" },
                new Category { CategoryName = "New Category" },
                new Category { CategoryName = "Software" });

            context.Region.AddOrUpdate(r => r.RegionID,
               new Region { RegionDescription = "Kazakhstan", RegionID = 17 });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory { TerritoryID = "100000000", TerritoryDescription = "Qaraganda", RegionID = 17 },
                new Territory { TerritoryID = "200000000", TerritoryDescription = "Almaty", RegionID = 17 },
                new Territory { TerritoryID = "300000000", TerritoryDescription = "Astana", RegionID = 17 });
        }
    }
}
