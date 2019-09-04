namespace MSSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTripColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "GenericLocation", c => c.String());
            AddColumn("dbo.Trips", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "Grade");
            DropColumn("dbo.Trips", "GenericLocation");
        }
    }
}
