namespace HR.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeResumes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        Link = c.String(maxLength: 2000),
                        EmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 200),
                        LastName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeResumes", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeResumes", new[] { "EmployeeId" });
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeResumes");
        }
    }
}
