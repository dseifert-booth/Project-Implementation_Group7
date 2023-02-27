namespace PRJ666_G7_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationScheduleShiftList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataGroupField", c => c.String());
            AddColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataTextField", c => c.String());
            AddColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataValueField", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataValueField");
            DropColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataTextField");
            DropColumn("dbo.EmployeeBaseViewModels", "ShiftList_DataGroupField");
        }
    }
}
