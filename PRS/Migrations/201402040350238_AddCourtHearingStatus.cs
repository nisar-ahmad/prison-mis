namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourtHearingStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtHearings", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtHearings", "Status");
        }
    }
}
