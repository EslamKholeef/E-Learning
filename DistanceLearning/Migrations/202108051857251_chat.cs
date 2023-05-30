namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatsAndUsers", "Chat_Id1", "dbo.Chats");
            DropForeignKey("dbo.ChatsAndUsers", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.ChatsAndUsers", new[] { "Chat_Id1" });
            DropIndex("dbo.ChatsAndUsers", new[] { "User_Id1" });
            AddColumn("dbo.Chats", "FirstUserId", c => c.String());
            AddColumn("dbo.Chats", "SecondUserId", c => c.String());
            DropTable("dbo.ChatsAndUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChatsAndUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(),
                        Chat_Id = c.Int(nullable: false),
                        Chat_Id1 = c.Int(),
                        User_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Chats", "SecondUserId");
            DropColumn("dbo.Chats", "FirstUserId");
            CreateIndex("dbo.ChatsAndUsers", "User_Id1");
            CreateIndex("dbo.ChatsAndUsers", "Chat_Id1");
            AddForeignKey("dbo.ChatsAndUsers", "User_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ChatsAndUsers", "Chat_Id1", "dbo.Chats", "Id");
        }
    }
}
