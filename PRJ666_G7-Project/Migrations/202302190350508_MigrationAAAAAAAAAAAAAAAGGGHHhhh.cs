namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAAAAAAAAAAGGGHHhhh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullNames", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
        }
    }
}
