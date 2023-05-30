namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLikesandUnlikesshit1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseModels", "RateOfLikes", c => c.Double(nullable: false));
            AlterColumn("dbo.CourseModels", "RateOfUnLikes", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseModels", "RateOfUnLikes", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseModels", "RateOfLikes", c => c.Int(nullable: false));
        }
    }
}
