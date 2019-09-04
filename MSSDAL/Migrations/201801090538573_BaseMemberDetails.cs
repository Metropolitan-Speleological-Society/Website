namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseMemberDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        Suburb = c.String(),
                        State = c.String(),
                        Postcode = c.String(),
                        Country = c.String(),
                        Person_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmailAddress = c.String(),
                        Person_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Phone = c.String(),
                        Person_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            AddColumn("dbo.People", "MiddleNames", c => c.String());
            AddColumn("dbo.People", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumbers", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Emails", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Addresses", "Person_Id", "dbo.People");
            DropIndex("dbo.PhoneNumbers", new[] { "Person_Id" });
            DropIndex("dbo.Emails", new[] { "Person_Id" });
            DropIndex("dbo.Addresses", new[] { "Person_Id" });
            DropColumn("dbo.People", "Surname");
            DropColumn("dbo.People", "MiddleNames");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.Emails");
            DropTable("dbo.Addresses");
        }
    }
}
