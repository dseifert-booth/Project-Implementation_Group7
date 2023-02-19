namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAAAAAAAAAAAAAAAGGGHHhhhhhhasdasdasd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Shift_Id", "dbo.Shifts");
            DropIndex("dbo.Employees", new[] { "Shift_Id" });
            CreateTable(
                "dbo.ShiftEmployees",
                c => new
                    {
                        Shift_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Shift_Id, t.Employee_Id })
                .ForeignKey("dbo.Shifts", t => t.Shift_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Shift_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Shift_Id", c => c.Int());
            DropForeignKey("dbo.ShiftEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.ShiftEmployees", "Shift_Id", "dbo.Shifts");
            DropIndex("dbo.ShiftEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.ShiftEmployees", new[] { "Shift_Id" });
            DropTable("dbo.ShiftEmployees");
            CreateIndex("dbo.Employees", "Shift_Id");
            AddForeignKey("dbo.Employees", "Shift_Id", "dbo.Shifts", "Id");
        }
    }
}
