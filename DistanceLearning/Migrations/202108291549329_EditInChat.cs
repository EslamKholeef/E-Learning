namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditInChat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chats", "First_Sender_Pic", c => c.String());
            AddColumn("dbo.Chats", "Second_Sender_Pic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chats", "Second_Sender_Pic");
            DropColumn("dbo.Chats", "First_Sender_Pic");
        }
    }
}
