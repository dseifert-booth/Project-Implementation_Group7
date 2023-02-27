namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationUserAuthLevelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShiftBaseViewModels", "UserAuthLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShiftBaseViewModels", "UserAuthLevel");
        }
    }
}
