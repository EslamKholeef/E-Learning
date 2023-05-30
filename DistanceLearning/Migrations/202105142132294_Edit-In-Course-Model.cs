namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditInCourseModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseModels", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.CourseModels", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourseModels", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.CourseModels", name: "CategoryId", newName: "Category_Id");
        }
    }
}
