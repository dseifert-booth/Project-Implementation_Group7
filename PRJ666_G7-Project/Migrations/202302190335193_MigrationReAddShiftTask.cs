namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationReAddShiftTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shifts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ShiftStart = c.DateTime(),
                    ShiftEnd = c.DateTime(),
                    ClockInTime = c.DateTime(nullable: false),
                    ClockOutTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            CreateTable(
               "dbo.Tasks",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Name = c.String(nullable: false),
                   Description = c.String(nullable: false),
                   Complete = c.Boolean(nullable: false)
               })
               .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
        }
    }
}
