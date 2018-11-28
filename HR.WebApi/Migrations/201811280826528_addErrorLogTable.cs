namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addErrorLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ControllerName = c.String(),
                        MethodName = c.String(),
                        ErrorMessage = c.String(),
                        ErrorType = c.String(),
                        VerbAttribute = c.String(),
                        UserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLogs");
        }
    }
}
