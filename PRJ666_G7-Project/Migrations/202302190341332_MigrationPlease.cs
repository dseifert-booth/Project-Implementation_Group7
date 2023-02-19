namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationPlease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "Manager", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
        }
    }
}
