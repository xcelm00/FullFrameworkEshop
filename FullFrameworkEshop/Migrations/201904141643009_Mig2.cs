namespace FullFrameworkEshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Customer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Customer");
        }
    }
}
