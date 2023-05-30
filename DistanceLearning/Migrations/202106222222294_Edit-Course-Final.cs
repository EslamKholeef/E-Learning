namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCourseFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "test_Id", "dbo.tests");
            DropIndex("dbo.Videos", new[] { "test_Id" });
            AddColumn("dbo.CourseModels", "DemoAboutTheCourse", c => c.String());
            DropColumn("dbo.Videos", "test_Id");
            DropTable("dbo.tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Videos", "test_Id", c => c.Int());
            DropColumn("dbo.CourseModels", "DemoAboutTheCourse");
            CreateIndex("dbo.Videos", "test_Id");
            AddForeignKey("dbo.Videos", "test_Id", "dbo.tests", "Id");
        }
    }
}
