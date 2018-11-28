namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAuditTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audits", "CreatedBy", c => c.String());
            AddColumn("dbo.Audits", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Audits", "RecordID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Audits", "RecordID");
            DropColumn("dbo.Audits", "CreatedDate");
            DropColumn("dbo.Audits", "CreatedBy");
        }
    }
}
