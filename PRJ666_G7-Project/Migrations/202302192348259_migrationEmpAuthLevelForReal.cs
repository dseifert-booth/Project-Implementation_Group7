namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationEmpAuthLevelForReal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeBaseViewModels", "AuthLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeBaseViewModels", "AuthLevel");
        }
    }
}
