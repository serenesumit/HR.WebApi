namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAccountandusrtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.Int(nullable: false),
                        AccName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.AccountNumber);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UID = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.UID);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Settings = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserAccounts1",
                c => new
                    {
                        UID = c.Int(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UID, t.AccountNumber });
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UID = c.Int(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UID, t.AccountNumber })
                .ForeignKey("dbo.Users", t => t.UID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.AccountNumber, cascadeDelete: true)
                .Index(t => t.UID)
                .Index(t => t.AccountNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "Id", "dbo.Users");
            DropForeignKey("dbo.UserAccounts", "AccountNumber", "dbo.Accounts");
            DropForeignKey("dbo.UserAccounts", "UID", "dbo.Users");
            DropIndex("dbo.UserAccounts", new[] { "AccountNumber" });
            DropIndex("dbo.UserAccounts", new[] { "UID" });
            DropIndex("dbo.UserSettings", new[] { "Id" });
            DropTable("dbo.UserAccounts");
            DropTable("dbo.UserAccounts1");
            DropTable("dbo.UserSettings");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
