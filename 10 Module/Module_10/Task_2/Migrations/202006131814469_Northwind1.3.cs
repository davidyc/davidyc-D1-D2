namespace Task_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Northwind13 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Region", newName: "Regions");
            AddColumn("dbo.Customers", "FoundationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "FoundationDate");
            RenameTable(name: "dbo.Regions", newName: "Region");
        }
    }
}
