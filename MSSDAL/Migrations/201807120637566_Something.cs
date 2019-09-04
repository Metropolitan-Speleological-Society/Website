namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Something : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.People", "CurrentStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "CurrentStatus", c => c.String());
            AlterColumn("dbo.People", "Surname", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
        }
    }
}
