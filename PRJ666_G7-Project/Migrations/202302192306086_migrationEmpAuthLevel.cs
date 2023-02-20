namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationEmpAuthLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "AuthLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "AuthLevel");
        }
    }
}
