namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationEmployeeRoleClaim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Shift_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Shift_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "Shift_Id" });
            DropColumn("dbo.Employees", "Shift_Id");
        }
    }
}
