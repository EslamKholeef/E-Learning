namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCourse : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tests", newName: "Courses");
            RenameColumn(table: "dbo.Videos", name: "test_Id", newName: "Course_Id");
            RenameIndex(table: "dbo.Videos", name: "IX_test_Id", newName: "IX_Course_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Videos", name: "IX_Course_Id", newName: "IX_test_Id");
            RenameColumn(table: "dbo.Videos", name: "Course_Id", newName: "test_Id");
            RenameTable(name: "dbo.Courses", newName: "tests");
        }
    }
}
