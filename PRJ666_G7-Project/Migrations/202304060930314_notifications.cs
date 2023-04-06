namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Notifications", new[] { "Employee_Id" });
            DropTable("dbo.Notifications");


        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IssueDateTime = c.DateTime(nullable: false),
                    Description = c.String(nullable: false),
                    Employee_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
        }
    }
}
