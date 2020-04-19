namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PrisonerTransactions", "Reference", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrisonerTransactions", "Reference", c => c.String());
        }
    }
}
