namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserandAccounttables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.UserAccounts",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AccountId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "Id", "dbo.Users");
            DropForeignKey("dbo.UserAccounts", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.UserAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.UserAccounts", new[] { "AccountId" });
            DropIndex("dbo.UserAccounts", new[] { "UserId" });
            DropIndex("dbo.UserSettings", new[] { "Id" });
            DropTable("dbo.UserAccounts");
            DropTable("dbo.UserSettings");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
