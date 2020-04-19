namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecisionStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prisoners", "Age", c => c.String());
            AddColumn("dbo.FIRs", "DecisionStatus", c => c.Int(nullable: false));
            AddColumn("dbo.FIRs", "DecisionAuthority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FIRs", "DecisionAuthority");
            DropColumn("dbo.FIRs", "DecisionStatus");
            DropColumn("dbo.Prisoners", "Age");
        }
    }
}
