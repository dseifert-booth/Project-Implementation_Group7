namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class popup : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ShiftEmployees", newName: "EmployeeShifts");
            DropPrimaryKey("dbo.EmployeeShifts");
            CreateTable(
                "dbo.EmployeeShiftsWeeklies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShiftsDate = c.DateTime(nullable: false),
                        EmployeeScheduleViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeBaseViewModels", t => t.EmployeeScheduleViewModel_Id)
                .Index(t => t.EmployeeScheduleViewModel_Id);
            
            AddColumn("dbo.Shifts", "EmployeeShiftsWeekly_Id", c => c.Int());
            AddPrimaryKey("dbo.EmployeeShifts", new[] { "Employee_Id", "Shift_Id" });
            CreateIndex("dbo.Shifts", "EmployeeShiftsWeekly_Id");
            AddForeignKey("dbo.Shifts", "EmployeeShiftsWeekly_Id", "dbo.EmployeeShiftsWeeklies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeShiftsWeeklies", "EmployeeScheduleViewModel_Id", "dbo.EmployeeBaseViewModels");
            DropForeignKey("dbo.Shifts", "EmployeeShiftsWeekly_Id", "dbo.EmployeeShiftsWeeklies");
            DropIndex("dbo.Shifts", new[] { "EmployeeShiftsWeekly_Id" });
            DropIndex("dbo.EmployeeShiftsWeeklies", new[] { "EmployeeScheduleViewModel_Id" });
            DropPrimaryKey("dbo.EmployeeShifts");
            DropColumn("dbo.Shifts", "EmployeeShiftsWeekly_Id");
            DropTable("dbo.EmployeeShiftsWeeklies");
            AddPrimaryKey("dbo.EmployeeShifts", new[] { "Shift_Id", "Employee_Id" });
            RenameTable(name: "dbo.EmployeeShifts", newName: "ShiftEmployees");
        }
    }
}
