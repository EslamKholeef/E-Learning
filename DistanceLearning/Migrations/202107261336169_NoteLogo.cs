namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteLogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "NoteLogo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "NoteLogo");
        }
    }
}
