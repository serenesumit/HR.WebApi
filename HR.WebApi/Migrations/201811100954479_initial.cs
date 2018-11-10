namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
            
            CreateTable(
                "dbo.EmployeeDocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Link = c.String(maxLength: 2000),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventDocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Link = c.String(maxLength: 2000),
                        EventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTypeId = c.Int(),
                        Title = c.String(maxLength: 2000),
                        Location = c.String(maxLength: 2000),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 200),
                        Zip = c.String(maxLength: 200),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Website = c.String(maxLength: 200),
                        Schedule = c.String(maxLength: 200),
                        Agenda = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventDocs", "EventId", "dbo.Events");
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.EmployeeDocs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Contacts", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Contacts", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ContactDocs", "ContactId", "dbo.Contacts");
            DropIndex("dbo.EventDocs", new[] { "EventId" });
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropIndex("dbo.EmployeeDocs", new[] { "EmployeeId" });
            DropIndex("dbo.Contacts", new[] { "NoteId" });
            DropIndex("dbo.Contacts", new[] { "DepartmentId" });
            DropIndex("dbo.ContactDocs", new[] { "ContactId" });
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
            DropTable("dbo.EventDocs");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeDocs");
            DropTable("dbo.Notes");
            DropTable("dbo.Departments");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactDocs");
        }
    }
}
