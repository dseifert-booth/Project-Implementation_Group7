namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationShiftDelete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShiftBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShiftStart = c.DateTime(nullable: false),
                        ShiftEnd = c.DateTime(nullable: false),
                        ClockInTime = c.DateTime(),
                        ClockOutTime = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShiftBaseViewModels");
        }
    }
}
