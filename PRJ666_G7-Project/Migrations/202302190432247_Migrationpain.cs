namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationpain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shifts", "ClockInTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Shifts", "ClockOutTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shifts", "ClockOutTime", c => c.DateTime());
            AlterColumn("dbo.Shifts", "ClockInTime", c => c.DateTime());
        }
    }
}
