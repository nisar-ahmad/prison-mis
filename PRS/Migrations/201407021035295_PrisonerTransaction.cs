namespace PRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrisonerTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrisonerTransactions",
                c => new
                    {
                        PrisonerTransactionId = c.Int(nullable: false, identity: true),
                        PrisonerId = c.Int(nullable: false),
                        DateOfTransaction = c.DateTime(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Reference = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PrisonerTransactionId)
                .ForeignKey("dbo.Prisoners", t => t.PrisonerId)
                .Index(t => t.PrisonerId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PrisonerTransactions", new[] { "PrisonerId" });
            DropForeignKey("dbo.PrisonerTransactions", "PrisonerId", "dbo.Prisoners");
            DropTable("dbo.PrisonerTransactions");
        }
    }
}
