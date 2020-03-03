namespace Shiverleih.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3rd : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerProducts");
            AddColumn("dbo.CustomerProducts", "CustomerProductID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CustomerProducts", "CustomerProductID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CustomerProducts");
            DropColumn("dbo.CustomerProducts", "CustomerProductID");
            AddPrimaryKey("dbo.CustomerProducts", new[] { "CustomerID", "ProductID" });
        }
    }
}
