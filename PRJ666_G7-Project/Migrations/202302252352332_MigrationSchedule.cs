namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationSchedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeBaseViewModels", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeBaseViewModels", "Discriminator");
        }
    }
}
