namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationpina : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Shift_Id", "dbo.Shifts");
            DropIndex("dbo.Tasks", new[] { "Shift_Id" });
            CreateTable(
                "dbo.TaskShifts",
                c => new
                    {
                        Task_Id = c.Int(nullable: false),
                        Shift_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_Id, t.Shift_Id })
                .ForeignKey("dbo.Tasks", t => t.Task_Id, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.Shift_Id, cascadeDelete: true)
                .Index(t => t.Task_Id)
                .Index(t => t.Shift_Id);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Shift_Id", c => c.Int());
            DropForeignKey("dbo.TaskShifts", "Shift_Id", "dbo.Shifts");
            DropForeignKey("dbo.TaskShifts", "Task_Id", "dbo.Tasks");
            DropIndex("dbo.TaskShifts", new[] { "Shift_Id" });
            DropIndex("dbo.TaskShifts", new[] { "Task_Id" });
            DropTable("dbo.TaskShifts");
            CreateIndex("dbo.Tasks", "Shift_Id");
            AddForeignKey("dbo.Tasks", "Shift_Id", "dbo.Shifts", "Id");
        }
    }
}
