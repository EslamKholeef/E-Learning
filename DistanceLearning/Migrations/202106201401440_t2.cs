namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tests", "VideoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tests", "VideoId", c => c.Int(nullable: false));
        }
    }
}
