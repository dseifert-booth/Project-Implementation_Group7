namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationEmployeeRoleClaim2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "UserName", c => c.String());
            AlterColumn("dbo.Employees", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "UserName", c => c.String(nullable: false));
        }
    }
}
