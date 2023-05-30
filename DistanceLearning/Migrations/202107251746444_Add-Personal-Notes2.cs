namespace DistanceLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonalNotes2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notes", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Notes", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notes", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Notes", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
