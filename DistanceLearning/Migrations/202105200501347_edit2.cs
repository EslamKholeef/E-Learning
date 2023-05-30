namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseModels", "CategoryName", c => c.String());
            DropColumn("dbo.CourseModels", "CourseLogo2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseModels", "CourseLogo2", c => c.String());
            DropColumn("dbo.CourseModels", "CategoryName");
        }
    }
}
