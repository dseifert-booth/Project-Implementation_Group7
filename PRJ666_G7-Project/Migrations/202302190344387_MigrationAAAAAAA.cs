namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Names", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Tasks", "Descriptions", c => c.String());
            DropColumn("dbo.Tasks", "Name");
            DropColumn("dbo.Tasks", "Description");
        }
        
        public override void Down()
        {
        }
    }
}
