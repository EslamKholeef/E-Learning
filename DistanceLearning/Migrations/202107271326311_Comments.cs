namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "StudentName", c => c.String());
            AddColumn("dbo.Comments", "StudentLogo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "StudentLogo");
            DropColumn("dbo.Comments", "StudentName");
        }
    }
}
