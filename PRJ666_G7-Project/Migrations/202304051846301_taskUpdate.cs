namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskShifts", "Task_Id", "dbo.Tasks");
            DropForeignKey("dbo.TaskShifts", "Shift_Id", "dbo.Shifts");
            DropIndex("dbo.TaskShifts", new[] { "Task_Id" });
            DropIndex("dbo.TaskShifts", new[] { "Shift_Id" });
            AddColumn("dbo.Tasks", "Employee_Id", c => c.Int());
            AddColumn("dbo.Tasks", "Shift_Id", c => c.Int());
            CreateIndex("dbo.Tasks", "Employee_Id");
            CreateIndex("dbo.Tasks", "Shift_Id");
            AddForeignKey("dbo.Tasks", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Tasks", "Shift_Id", "dbo.Shifts", "Id");
            DropTable("dbo.TaskShifts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskShifts",
                c => new
                    {
                        Task_Id = c.Int(nullable: false),
                        Shift_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_Id, t.Shift_Id });
            
            DropForeignKey("dbo.Tasks", "Shift_Id", "dbo.Shifts");
            DropForeignKey("dbo.Tasks", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Tasks", new[] { "Shift_Id" });
            DropIndex("dbo.Tasks", new[] { "Employee_Id" });
            DropColumn("dbo.Tasks", "Shift_Id");
            DropColumn("dbo.Tasks", "Employee_Id");
            CreateIndex("dbo.TaskShifts", "Shift_Id");
            CreateIndex("dbo.TaskShifts", "Task_Id");
            AddForeignKey("dbo.TaskShifts", "Shift_Id", "dbo.Shifts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TaskShifts", "Task_Id", "dbo.Tasks", "Id", cascadeDelete: true);
        }
    }
}
