namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatephonenumberfield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Contacts", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "MobileNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
