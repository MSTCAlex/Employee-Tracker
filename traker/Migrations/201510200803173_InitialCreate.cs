namespace traker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employeeId = c.String(),
                        employerId = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        TotalHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.jobs");
        }
    }
}
