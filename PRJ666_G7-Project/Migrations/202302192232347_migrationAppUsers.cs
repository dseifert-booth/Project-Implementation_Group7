namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationAppUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeBaseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeBaseViewModels");
        }
    }
}
