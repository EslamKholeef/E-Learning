namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLikesandUnlikes6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseModels", "RateOfLikes", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseModels", "RateOfUnLikes", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseModels", "FinalRatingDegree", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseModels", "FinalRatingDegree", c => c.Double());
            AlterColumn("dbo.CourseModels", "RateOfUnLikes", c => c.Int());
            AlterColumn("dbo.CourseModels", "RateOfLikes", c => c.Int());
        }
    }
}
