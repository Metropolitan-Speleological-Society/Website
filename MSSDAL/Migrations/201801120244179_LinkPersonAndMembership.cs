namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkPersonAndMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonMemberships",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Memberships", "PersonMembership_Id", c => c.Guid());
            AddColumn("dbo.People", "PersonMembership_Id", c => c.Guid());
            CreateIndex("dbo.Memberships", "PersonMembership_Id");
            CreateIndex("dbo.People", "PersonMembership_Id");
            AddForeignKey("dbo.Memberships", "PersonMembership_Id", "dbo.PersonMemberships", "Id");
            AddForeignKey("dbo.People", "PersonMembership_Id", "dbo.PersonMemberships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "PersonMembership_Id", "dbo.PersonMemberships");
            DropForeignKey("dbo.Memberships", "PersonMembership_Id", "dbo.PersonMemberships");
            DropIndex("dbo.People", new[] { "PersonMembership_Id" });
            DropIndex("dbo.Memberships", new[] { "PersonMembership_Id" });
            DropColumn("dbo.People", "PersonMembership_Id");
            DropColumn("dbo.Memberships", "PersonMembership_Id");
            DropTable("dbo.PersonMemberships");
        }
    }
}
