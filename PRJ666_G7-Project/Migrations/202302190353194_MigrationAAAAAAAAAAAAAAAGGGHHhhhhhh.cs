namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAAAAAAAAAAGGGHHhhhhhh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            DropColumn("dbo.AspNetUsers", "FullNames");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FullNames", c => c.String());
            DropColumn("dbo.AspNetUsers", "FullName");
        }
    }
}
