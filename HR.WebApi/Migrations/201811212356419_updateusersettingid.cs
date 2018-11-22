namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusersettingid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserSettings", new[] { "Id" });
            DropPrimaryKey("dbo.UserSettings");
            AlterColumn("dbo.UserSettings", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserSettings", "Id");
            CreateIndex("dbo.UserSettings", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserSettings", new[] { "Id" });
            DropPrimaryKey("dbo.UserSettings");
            AlterColumn("dbo.UserSettings", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserSettings", "Id");
            CreateIndex("dbo.UserSettings", "Id");
        }
    }
}
