namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactschemaadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Link = c.String(maxLength: 2000),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(),
                        Title = c.String(maxLength: 2000),
                        Bio = c.String(maxLength: 2000),
                        Name = c.String(maxLength: 200),
                        PhoneNumber = c.Int(nullable: false),
                        MobileNumber = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        QuickFacts = c.String(maxLength: 200),
                        Website = c.String(maxLength: 200),
                        NoteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Notes", t => t.NoteId)
                .Index(t => t.DepartmentId)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Contacts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ContactDocs", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Contacts", new[] { "NoteId" });
            DropIndex("dbo.Contacts", new[] { "DepartmentId" });
            DropIndex("dbo.ContactDocs", new[] { "ContactId" });
            DropTable("dbo.Notes");
            DropTable("dbo.Departments");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactDocs");
        }
    }
}
