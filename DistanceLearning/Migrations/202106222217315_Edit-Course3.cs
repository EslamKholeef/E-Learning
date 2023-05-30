namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCourse3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseModels", "Code", c => c.String());
            AddColumn("dbo.Videos", "CourseModel_Id", c => c.Int());
            CreateIndex("dbo.Videos", "CourseModel_Id");
            AddForeignKey("dbo.Videos", "CourseModel_Id", "dbo.CourseModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "CourseModel_Id", "dbo.CourseModels");
            DropIndex("dbo.Videos", new[] { "CourseModel_Id" });
            DropColumn("dbo.Videos", "CourseModel_Id");
            DropColumn("dbo.CourseModels", "Code");
        }
    }
}
