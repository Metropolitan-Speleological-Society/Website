namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        MembershipType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.People", "CurrentStatus", c => c.String());
            AddColumn("dbo.People", "ShowOnSite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "ShowOnSite");
            DropColumn("dbo.People", "CurrentStatus");
            DropTable("dbo.Memberships");
        }
    }
}
