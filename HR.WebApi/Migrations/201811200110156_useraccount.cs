namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useraccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccounts1",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AccountId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts1");
        }
    }
}
