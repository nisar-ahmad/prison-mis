namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecisionDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FIRs", "DecisionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FIRs", "DecisionDate");
        }
    }
}
