namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteLogo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "Course_Id", c => c.Int());
            CreateIndex("dbo.Notes", "Course_Id");
            AddForeignKey("dbo.Notes", "Course_Id", "dbo.CourseModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "Course_Id", "dbo.CourseModels");
            DropIndex("dbo.Notes", new[] { "Course_Id" });
            DropColumn("dbo.Notes", "Course_Id");
        }
    }
}
