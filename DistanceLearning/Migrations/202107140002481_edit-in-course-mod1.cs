namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editincoursemod1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseModels", name: "Publisher_Id", newName: "User_Id");
            RenameIndex(table: "dbo.CourseModels", name: "IX_Publisher_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourseModels", name: "IX_User_Id", newName: "IX_Publisher_Id");
            RenameColumn(table: "dbo.CourseModels", name: "User_Id", newName: "Publisher_Id");
        }
    }
}
