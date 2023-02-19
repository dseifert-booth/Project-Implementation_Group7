namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAAAAA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Tasks", "Description", c => c.String());
            DropColumn("dbo.Tasks", "Names");
            DropColumn("dbo.Tasks", "Descriptions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Descriptions", c => c.String());
            AddColumn("dbo.Tasks", "Names", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Tasks", "Description");
            DropColumn("dbo.Tasks", "Name");
        }
    }
}
