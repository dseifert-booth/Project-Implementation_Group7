namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Deadline");
        }
    }
}
