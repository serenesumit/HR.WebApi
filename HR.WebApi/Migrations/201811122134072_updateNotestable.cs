namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNotestable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "NoteId", "dbo.Notes");
            DropIndex("dbo.Contacts", new[] { "NoteId" });
            AddColumn("dbo.Notes", "ContactId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "ContactId");
            AddForeignKey("dbo.Notes", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
            DropColumn("dbo.Contacts", "NoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "NoteId", c => c.Int());
            DropForeignKey("dbo.Notes", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Notes", new[] { "ContactId" });
            DropColumn("dbo.Notes", "ContactId");
            CreateIndex("dbo.Contacts", "NoteId");
            AddForeignKey("dbo.Contacts", "NoteId", "dbo.Notes", "Id");
        }
    }
}
