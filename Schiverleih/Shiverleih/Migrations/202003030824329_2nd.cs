namespace Shiverleih.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Title", c => c.String(maxLength: 90));
            AlterColumn("dbo.Customers", "FName", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Customers", "LName", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Customers", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Products", "Title", c => c.String(nullable: false, maxLength: 90));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "LName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Title", c => c.String());
        }
    }
}
