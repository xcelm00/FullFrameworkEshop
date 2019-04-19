namespace FullFrameworkEshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTypes", "ProductTypeName", c => c.String());
            DropColumn("dbo.ProductTypes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTypes", "Name", c => c.String());
            DropColumn("dbo.ProductTypes", "ProductTypeName");
        }
    }
}
