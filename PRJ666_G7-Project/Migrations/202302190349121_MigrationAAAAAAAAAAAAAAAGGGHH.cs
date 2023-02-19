namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAAAAAAAAAAGGGHH : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "Shift_Id" });
            DropColumn("dbo.Employees", "Shift_Id");
        }
        
        public override void Down()
        {
        }
    }
}
