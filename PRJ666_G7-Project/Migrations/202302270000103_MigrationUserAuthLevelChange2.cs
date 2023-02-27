namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationUserAuthLevelChange2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShiftBaseViewModels", "UserAuthLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShiftBaseViewModels", "UserAuthLevel", c => c.Int(nullable: false));
        }
    }
}
